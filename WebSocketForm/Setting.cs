﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using Model.File;

namespace WebSocketForm
{
    public static class Setting
    {

        #region Setting

        /// <summary>
        /// 通讯端口
        /// </summary>
        public const int DATA_PORT = 8009;

        /// <summary>
        /// 广播端口
        /// </summary>
        public const int BROADCAST_PORT = 8010;
        
        /// <summary>
        /// 单包大小
        /// </summary>
        public const int BUFFER_SIZE = 1024;

        /// <summary>
        /// 用户设定
        /// </summary>
        public static File_User UserConfig;

        #endregion

    }
}
