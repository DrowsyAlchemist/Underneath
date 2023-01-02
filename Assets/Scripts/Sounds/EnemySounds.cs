using UnityEngine;

public class EnemySounds : MonoBehaviour
{
    [SerializeField] private AudioSource _hurtSound;
    [SerializeField] private AudioSource _deathSound;

    public void PlayHurt()
    {
        _hurtSound.Play();
    }

    public void PlayDeath()
    {
        _deathSound.Play();
    }
}
