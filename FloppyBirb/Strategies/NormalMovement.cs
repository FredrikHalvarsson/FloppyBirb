using FloppyBirb.Strategies;
using FloppyBirb.Helpers;

namespace FloppyBirb.Strategies
{
    // Implementation of the Movement Strategy
    public class NormalMovement : IMovementStrategy
    {

        public float Flap()
        {
            return -2;
        }

        public void Update(ref float dy)
        {
            dy += GameConfig.Gravity;
        }
    }
}