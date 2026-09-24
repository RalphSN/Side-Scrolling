public interface IDamageable
{
    int MaxHp { get; }
    int CurrentHp { get; }
    bool IsDead { get; }

    void TakeDamage(int amount);
}
