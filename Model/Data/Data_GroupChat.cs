﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    [Serializable]
    public class Data_GroupChat
    {
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>群所有者IP</remarks>
        public long OwnerID { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public byte[] HeadImage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>DateTimeTicks</remarks>
        public long ID { get; set; }

        public List<Data_User> Members { get; set; }

        public string Name { get; set; }
    }
}
