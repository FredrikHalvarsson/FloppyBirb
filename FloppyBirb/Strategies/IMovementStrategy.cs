namespace FloppyBirb.Strategies
{
    // Strategy Pattern: see line 6 in Bird.cs for explanation
    public interface IMovementStrategy
    {
        float Flap();
        void Update(ref float dy);
    }
}