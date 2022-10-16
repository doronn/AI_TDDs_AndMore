namespace Shooter
{
    public interface IHitTarget
    {
        bool ReceiveHit(IDamageGiver damageGiver);
    }
}