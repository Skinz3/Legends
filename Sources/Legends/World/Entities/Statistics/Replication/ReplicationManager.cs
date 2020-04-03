﻿using Legends.Protocol.GameClient.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Entities.Statistics.Replication
{
    /// <summary>
    /// Special Thanks to Jesus (Moonshadow) and Furkan_S ^^
    /// </summary>
    public class ReplicationManager
    {
        public ReplicateStat[,] Values { get; private set; } = new ReplicateStat[6, 32];
        public bool Changed { get; set; } = true;

        private void DoUpdate(uint value, int primary, int secondary, bool isFloat)
        {
            if (Values[primary, secondary] == null)
            {
                Values[primary, secondary] = new ReplicateStat
                {
                    Value = value,
                    IsFloat = isFloat,
                    Changed = true
                };
                Changed = true;
            }
            else if (Values[primary, secondary].Value != value)
            {
                Values[primary, secondary].IsFloat = isFloat;
                Values[primary, secondary].Value = value;
                Values[primary, secondary].Changed = true;
                Changed = true;
            }
        }

        public void UpdateUInt(uint value, int primary, int secondary)
        {
            DoUpdate(value, primary, secondary, false);
        }

        public void UpdateInt(int value, int primary, int secondary)
        {
            DoUpdate((uint)value, primary, secondary, false);
        }

        public void UpdateBool(bool value, int primary, int secondary)
        {
            DoUpdate(value ? 1u : 0u, primary, secondary, false);
        }

        public void UpdateFloat(float value, int primary, int secondary)
        {
            DoUpdate(BitConverter.ToUInt32(BitConverter.GetBytes(value), 0), primary, secondary, true);
        }
    }
}
