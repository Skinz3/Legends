using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World
{
    public interface IUpdatable
    {
        void Update(long deltaTime);
    }
}
