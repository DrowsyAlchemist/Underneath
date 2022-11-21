using UnityEngine;

public class AdventureGirlAnimation : PlayerAnimation
{
    private const string MeleeAnimation = "Melee";
    private const string ShootAnimation = "Shoot";

    public void PlayMelee()
    {
        Animator.Play(MeleeAnimation);
    }

    public void PlayShoot()
    {
        Animator.Play(ShootAnimation);
    }
}
