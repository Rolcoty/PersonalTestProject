﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WebSocketForm.Enum;
using WebSocketForm.Function;
using WebSocketForm.Helper;
using WebSocketForm.Model;
using WebSocketForm.Model.Data;
using WebSocketForm.Model.View;

namespace WebSocketForm.View
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 持续在线广播线程
        /// </summary>
        public static Thread StillOnlineBroadcastingThread;
        /// <summary>
        /// 刷新在线状态线程
        /// </summary>
        public static Thread UserStatusRefreshThread;

        public MainWindow()
        {
            InitializeComponent();
            Init();

            if (Setting.UserConfig == null)
            {
                var us = new UserSetting();
                us.ShowDialog();
            }

            StartOnlineThreads();
            LoadedInit();
        }

        #region 过程方法

        private void Init()
        {
            //读取设定
            AppData.Load();

            //清空测试用列表项目
            OnlineUserList.Items.Clear();

            //重载控件
            OnlineUserList.ItemsSource = AppData.GetMenuList();

            //绑定服务器事件
            LocalServer.GetLoginDataReceived += LocalServer_GetLoginData;

            LocalServer.LoginReceived += Other_LoginReceived;
            LocalServer.LogoutReceived += Other_LogoutReceived;

            LocalServer.OpenLocalServer();
        }

        private void LoadedInit()
        {
            UserHeadImage.Background = new ImageBrush(ImageHelper.BytesToBitmapImage(Setting.UserConfig.HeadImage));
        }

        private void StartOnlineThreads()
        {
            //上线广播
            new Thread(BroadcastFunction.OnlineBroadcasting) { IsBackground = true }.Start();
            //持续在线广播线程
            new Thread(BroadcastFunction.StillOnlineBroadcasting) { IsBackground = true }.Start();
            //刷新在线状态线程
            new Thread(AppData.UserStatusRefresh) { IsBackground = true }.Start();
        }

        #endregion

        #region 接口

        public void RefreshMenu()
        {
            OnlineUserList.ItemsSource = AppData.GetMenuList();
            OnlineUserList.Items.Refresh();
        }

        #endregion

        #region 服务器事件

        private static void LocalServer_GetLoginData(BroadcastInfo data, IPAddress ip)
        {
            NetHelper.Send_TCP(ip, new PostInfo()
            {
                Action = PostActionType.login,
                Data = ModelHelper.FileUserToDataUser(Setting.UserConfig),
                IP = Setting.UserConfig.IP
            });
        }

        private void Other_LoginReceived(PostInfo data, IPAddress ip)
        {
            var userData = (Data_User)data.Data;

            AppData.AddUser(ModelHelper.DataUserToViewUser(userData));

            OnlineUserList.ItemsSource = AppData.GetMenuList();
            OnlineUserList.Items.Refresh();
        }

        private void Other_LogoutReceived(PostInfo data, IPAddress ip)
        {
            var userData = (Data_User)data.Data;

            AppData.AddUser(ModelHelper.DataUserToViewUser(userData));

            OnlineUserList.ItemsSource = AppData.GetMenuList();
            OnlineUserList.Items.Refresh();
        }

        #endregion

        #region 窗体基础事件
        private void Window_Drag(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement c = (FrameworkElement)sender;
            if (c.Name != "ControlBox")
            {
                DragMove();
            }
        }

        private void HideApplication(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void FullSizeOrMinApplication(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
            else
            {
                WindowState = WindowState.Maximized;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            new Thread(() =>
            {
                BroadcastFunction.OfflineBroadcasting();
            }).Start();
        }

        private void MenuButton_Enter(object sender, MouseEventArgs e)
        {
            var label = (Label)sender;

            if (label.Opacity != 1)
            {
                var storyBord = new Storyboard();
                var opacity = new DoubleAnimation(0.8, new TimeSpan(0, 0, 0, 0, 50));
                Storyboard.SetTarget(opacity, label);
                Storyboard.SetTargetProperty(opacity, new PropertyPath(OpacityProperty));
                storyBord.Children.Add(opacity);

                storyBord.Begin();
            }
        }

        private void MenuButton_Leave(object sender, MouseEventArgs e)
        {
            var label = (Label)sender;

            if (label.Opacity != 1)
            {
                var storyBord = new Storyboard();
                var opacity = new DoubleAnimation(0.6, new TimeSpan(0, 0, 0, 0, 50));
                Storyboard.SetTarget(opacity, label);
                Storyboard.SetTargetProperty(opacity, new PropertyPath(OpacityProperty));
                storyBord.Children.Add(opacity);

                storyBord.Begin();
            }
        }
        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new Thread(() =>
            {
                var uc = new UdpClient();
                var ipep = new IPEndPoint(IPAddress.Broadcast, 8009);

                while (true)
                {
                    uc.SendAsync(new byte[264130], 264130, ipep);
                    Thread.Sleep(10);
                }
            })
            {
                IsBackground = true
            }.Start();
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            Random rd = new Random();

            var ip = "192.168.4." + rd.Next(255);

            AppData.AddUser(new User()
            {
                IP = IPAddress.Parse(ip),
                IsTop = rd.Next(100) % 2 == 0 ? true : false,
                NickName = "测试"
            });

            RefreshMenu();
        }

        Thread t;

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            //if (t == null)
            //{
            //    t = new Thread(() =>
            //    {
            //        var i = 1;
            //        while (true)
            //        {
            //            var data = new byte[i++];
            //            NetHelper.UDP_Send(IPAddress.Broadcast, data);
            //            Thread.Sleep(10);
            //        }
            //    })
            //    {
            //        IsBackground = true
            //    };

            //    t.Start();
            //}
            NetHelper.Send_TCP(new IPAddress(Setting.UserConfig.IP), new PostInfo()
            {
                Action = PostActionType.stillOnline,
                Data = new byte[999999999],
                IP = Setting.UserConfig.IP
            });
        }
    }
}
