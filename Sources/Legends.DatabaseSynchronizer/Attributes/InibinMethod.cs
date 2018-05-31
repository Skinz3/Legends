using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.DatabaseSynchronizer.Attributes
{
    public class InibinMethod : Attribute
    {
        public Type recordType;

        public InibinMethod(Type recordType)
        {
            this.recordType = recordType;
        }
    }
}
