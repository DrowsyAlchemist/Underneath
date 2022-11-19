using UnityEngine;

public class TargetLeftKillZoneTransition : EnemyTransition
{
    [SerializeField] private float _killZoneRadius;

    private void Update()
    {
        if (Vector2.Distance(Target.GetWorldCenter(), transform.position) > _killZoneRadius)
            NeedTransit = true;
    }
}
