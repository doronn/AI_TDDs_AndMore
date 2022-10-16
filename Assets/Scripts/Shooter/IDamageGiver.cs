namespace Shooter
{
    public interface IDamageGiver
    {
        float Damage { get; }
        public int OwnerId { get; }
    }
}