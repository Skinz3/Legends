﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Configurations
{
    public class Configuration
    {
        public string ServerIp
        {
            get;
            set;
        }
        public ushort ServerPort
        {
            get;
            set;
        }
        public bool StartClient
        {
            get;
            set;
        }
        public string LeaguePath
        {
            get;
            set;
        }
        public string MySQLHost
        {
            get;
            set;
        }
        public string MySQLUser
        {
            get;
            set;
        }
        public string MySQLPassword
        {
            get;
            set;
        }
        public string DatabaseName
        {
            get;
            set;
        }

        public List<PlayerData> Players
        {
            get;
            set;
        }
        
    }
}
