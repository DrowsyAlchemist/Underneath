public class ExtraHeart : Collectable
{
    private const int ExtraHeartsCount = 1;

    protected override void CollectByPlayer(Player player)
    {
        player.PlayerHealth.IncreaseMaxHealth(ExtraHeartsCount);
    }
}
