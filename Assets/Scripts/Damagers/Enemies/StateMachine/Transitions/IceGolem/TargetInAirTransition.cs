using UnityEngine;

public class TargetInAirTransition : EnemyTransition
{
    [SerializeField] private float _maxHeight;

    private void Update()
    {
        if ((Target.GetWorldCenter().y - transform.position.y) > _maxHeight)
            NeedTransit = true;
    }
}
