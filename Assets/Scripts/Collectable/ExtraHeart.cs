public class ExtraHeart : Bubble
{
    private const int ExtraHeartsCount = 1;

    protected override void Collect(Player player)
    {
        player.Health.IncreaseMaxHealth(ExtraHeartsCount);
    }
}