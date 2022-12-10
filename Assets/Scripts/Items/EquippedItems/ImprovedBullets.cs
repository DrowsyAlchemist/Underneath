using UnityEngine;

public class ImprovedBullets : Item
{
    [SerializeField] private Bullet _improvedBullet;

    public void Use(AccessPoint game)
    {
        if (game.Player.TryGetComponent(out GirlPlayer girlPlayer))
            girlPlayer.SetBullet(_improvedBullet);
    }
}
