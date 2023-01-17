public class Apple : Bubble
{
    private const int RestoringHealth = 1;

    protected override void Collect(Player player)
    {
        player.Health.RestoreHealth(RestoringHealth);
    }
}