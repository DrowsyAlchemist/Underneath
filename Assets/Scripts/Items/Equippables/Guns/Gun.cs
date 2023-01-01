using UnityEngine;

public class Gun : EquippableItem
{
    [SerializeField] private float _secondsBetweenShoots;
    [SerializeField] private Bullet _bullet;

    private float _timeAfterShot;

    public bool CanShoot => (_timeAfterShot > _secondsBetweenShoots);

    private void Update()
    {
        _timeAfterShot += Time.deltaTime;
    }

    public void Shoot(Vector2 shootPoint, Vector2 direction)
    {
        if (CanShoot)
        {
            _timeAfterShot = 0;
            Bullet bullet = Instantiate(_bullet, shootPoint, Quaternion.identity);
            bullet.Launch(direction.normalized);
        }
    }

    public override void Affect(Player player)
    {
        player.Inventory.SetGun(this);
    }

    public override void StopAffecting(Player player)
    {
        player.Inventory.TakeOffGun();
    }

    public void ModifyTimeBetweenShots(float modifier)
    {
        _secondsBetweenShoots *= modifier;
    }
}
