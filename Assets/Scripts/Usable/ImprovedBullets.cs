using UnityEngine;

public class ImprovedBullets : PermanentEffectItem
{
    [SerializeField] private Bullet _improvedBullet;

    public override void Use(AccessPoint game)
    {
        if (game.Player.TryGetComponent(out GirlPlayer girlPlayer))
            girlPlayer.SetBullet(_improvedBullet);
    }
}
