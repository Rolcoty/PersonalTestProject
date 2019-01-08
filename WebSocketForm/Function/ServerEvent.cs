﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebSocketForm.Enum;
using WebSocketForm.Model;
using WebSocketForm.Model.Enum;

namespace WebSocketForm.Function
{
    class ServerEvent
    {
        public static void LocalServer_LoginReceived(PostInfo data, IPAddress ip)
        {
            var userData = (User)data.Data;

            Setting.AddUser(userData);
        }

        public static void LocalServer_LogoutReceived(PostInfo data, IPAddress ip)
        {
            var userData = (User)data.Data;

            Setting.AddUser(userData);
        }
    }
}
