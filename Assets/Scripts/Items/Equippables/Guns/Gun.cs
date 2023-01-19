using UnityEngine;

public class Gun : AffectingItem
{
    [SerializeField] private float _secondsBetweenShots;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private AudioSource _shotSound;

    private float _timeAfterShot;

    public bool CanShoot => (_timeAfterShot > _secondsBetweenShots);

    private void OnEnable()
    {
        _timeAfterShot = _secondsBetweenShots;
    }

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
            _shotSound.Play();
        }
    }

    public void ModifyTimeBetweenShots(float modifier)
    {
        _secondsBetweenShots *= modifier;
    }

    protected override void StartAffecting(Player player)
    {
        transform.SetParent(player.transform);
        player.Inventory.SetGun(this);
    }

    protected override void StopAffecting(Player player)
    {
        player.Inventory.TakeOffGun();
    }
}