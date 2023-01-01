public class Apple : Collectable
{
    private const int RestoringHealth = 1;

    protected override void CollectByPlayer(Player player)
    {
        player.PlayerHealth.RestoreHealth(RestoringHealth);
    }
}
