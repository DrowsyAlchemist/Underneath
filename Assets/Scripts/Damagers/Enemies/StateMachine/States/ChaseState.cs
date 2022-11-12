using UnityEngine;

public class ChaseState : EnemyState
{
    [SerializeField] private float _speed;

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Target.position, _speed * Time.deltaTime);
    }
}
