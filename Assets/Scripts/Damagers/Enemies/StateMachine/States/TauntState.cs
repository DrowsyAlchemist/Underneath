public class TauntState : EnemyState
{
    private void OnEnable()
    {
        EnemyAnimation.PlayTaunt();
    }
}
