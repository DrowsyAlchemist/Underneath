using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private AudioSource _runnindSound;
    [SerializeField] private AudioSource _jumpFromGroundSound;
    [SerializeField] private AudioSource _jumpInAirSound;
    [SerializeField] private AudioSource _landingSound;
    [SerializeField] private AudioSource _idleSound;
    [SerializeField] private List<AudioSource> _hurtSounds;

    private static PlayerSounds _instance;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

    public static void PlayIdle()
    {
        if (_instance._idleSound.isPlaying == false)
        {
            _instance.ResetSounds();
            _instance._idleSound.Play();
        }
    }

    public static void PlayRun()
    {
        if (_instance._runnindSound.isPlaying == false)
        {
            _instance.ResetSounds();
            _instance._runnindSound.Play();
        }
    }

    public static void PlayJumpFromGround()
    {
        _instance.ResetSounds();
        _instance._jumpFromGroundSound.Play();
    }

    public static void PlayJumpInAir()
    {
        _instance.ResetSounds();
        _instance._jumpInAirSound.Play();
    }

    public static void PlayJumpLoop()
    {
        _instance.ResetSounds();
    }
     
    public static void PlayLanding()
    {
        _instance.ResetSounds();
        _instance._landingSound.Play();
    }

    public static void PlayHurt()
    {
        _instance.ResetSounds();
        int soundNumber = Random.Range(0, _instance._hurtSounds.Count);
        _instance._hurtSounds[soundNumber].Play();
    }

    private void ResetSounds()
    {
        if (_idleSound.isPlaying)
            _idleSound.Stop();

        if(_runnindSound.isPlaying)
            _runnindSound.Stop();
    }
}
