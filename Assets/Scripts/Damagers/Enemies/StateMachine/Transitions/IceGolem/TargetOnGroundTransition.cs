using UnityEngine;

public class TargetOnGroundTransition : EnemyTransition
{
    [SerializeField] private float _minHeight;

    private void Update()
    {
        if ((Target.GetWorldCenter().y - transform.position.y) < _minHeight)
            NeedTransit = true;
    }
}
