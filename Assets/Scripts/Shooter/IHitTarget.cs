namespace Shooter
{
    public interface IHitTarget
    {
        int ReceiveHit(IDamageGiver damageGiver);
    }
}