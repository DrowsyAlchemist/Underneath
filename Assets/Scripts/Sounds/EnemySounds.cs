using UnityEngine;

public class EnemySounds : MonoBehaviour
{
    [SerializeField] private AudioSource _hurtSound;
    [SerializeField] private AudioSource _deathSound;
    [SerializeField] private AudioSource _meleeSound;

    public void PlayHurt()
    {
        _hurtSound.Play();
    }

    public void PlayDeath()
    {
        _deathSound.Play();
    }

    public void MeleeSound()
    {
        _meleeSound.Play();
    }
}