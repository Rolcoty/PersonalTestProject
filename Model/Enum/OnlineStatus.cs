﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Enum
{
    public enum OnlineStatus
    {
        Unknow = -1,
        Offline = 0,
        Online = 1,
        //Hiding = 5,   //局域网你隐个P身
        Leaving = 10,
        Busy = 20
    }
}
