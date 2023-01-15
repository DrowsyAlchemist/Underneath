using UnityEngine;

public class ÑrossedWithPlayerTransotion : EnemyTransition
{
    [SerializeField] private float _distanse;

    private void Update()
    {
        if (Vector2.Distance(transform.position, Target.GetPosition()) < _distanse)
            NeedTransit = true;
    }
}
