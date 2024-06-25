using FloppyBirb.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloppyBirb.Strategies
{
    // Alternate Movement type, example for expanding the code with strategies
    internal class SlowMovement : IMovementStrategy
    {
        public float Flap()
        {
            return -1;
        }

        public void Update(ref float dy)
        {
            dy += GameConfig.Gravity;
        }
    }
}
