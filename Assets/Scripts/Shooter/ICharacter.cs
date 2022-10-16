namespace Shooter
{
    public interface ICharacter
    {
        int Id { get; }
        float Health { get; }
        void ReceiveDamage(float amount);
    }
}