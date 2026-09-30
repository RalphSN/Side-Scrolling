public interface IHazard 
{
    int Damage { get; }
    void OnPlayerContact(PlayerHealth player);
}
