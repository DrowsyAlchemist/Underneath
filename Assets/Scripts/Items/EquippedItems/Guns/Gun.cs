using UnityEngine;

public class Gun : EquippableItem
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private float _secondsBetweenShoots;

    private float _timeAfterShot;

    private void Update()
    {
        _timeAfterShot += Time.deltaTime;
    }

    public virtual void Shoot(Vector2 shootPoint, Vector2 direction)
    {
        if (_timeAfterShot > _secondsBetweenShoots)
        {
            _timeAfterShot = 0;
            Bullet bullet = Instantiate(_bullet, shootPoint, Quaternion.identity);
            bullet.Launch(direction.normalized);
        }
    }

    public override void Affect(Player player)
    {
        throw new System.NotImplementedException();
    }

    public override void StopAffecting(Player player)
    {
        throw new System.NotImplementedException();
    }
}
