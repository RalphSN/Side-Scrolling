public interface IAttacker
{
    int AttackPower { get; }
    void Attack(int facingDirection);
}
