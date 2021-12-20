using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _SFXSource;
    [SerializeField] private AudioClip[] _sfx;
    [SerializeField] private AudioClip _ost;

    private void Start()
    {
        _SFXSource.PlayOneShot(_ost, 0.15f);
    }

    public void PlayerThrust()
    {
        _SFXSource.PlayOneShot(_sfx[0], 0.2f);
    }
    public void PlayerShot()
    {
        _SFXSource.PlayOneShot(_sfx[1], 0.1f);
    }
    public void PlayerExplosion()
    {
        _SFXSource.PlayOneShot(_sfx[2], 0.1f);
    }
    public void AsteroidExplosion()
    {
        _SFXSource.PlayOneShot(_sfx[3], 0.1f);
    }
    public void EnemyShot()
    {
        _SFXSource.PlayOneShot(_sfx[4], 0.1f);
    }
    public void MenuSelect()
    {
        _SFXSource.PlayOneShot(_sfx[5], 0.2f);
    }
    public void MenuClick()
    {
        _SFXSource.PlayOneShot(_sfx[6], 0.3f);
    }
    public void MenuBack()
    {
        _SFXSource.PlayOneShot(_sfx[7], 0.2f);
    }
    public void PowerUp()
    {
        _SFXSource.PlayOneShot(_sfx[8], 0.2f);
    }
    public void OneUp()
    {
        _SFXSource.PlayOneShot(_sfx[9], 0.15f);
    }
    public void PiercingShot()
    {
        _SFXSource.PlayOneShot(_sfx[10], 0.2f);
    }
}
