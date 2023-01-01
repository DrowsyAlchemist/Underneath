public class TauntState : EnemyState
{
    private void OnEnable()
    {
        EnemyAnimator.PlayTaunt();
    }
}
