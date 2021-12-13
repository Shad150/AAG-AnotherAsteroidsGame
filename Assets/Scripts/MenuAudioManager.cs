using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _SFXSource;
    [SerializeField] private AudioClip[] _sfx;
    [SerializeField] private AudioClip _ost;

    private void Start()
    {
        _SFXSource.PlayOneShot(_ost, 0.15f);
    }

    public void MenuSelect()
    {
        _SFXSource.PlayOneShot(_sfx[0], 0.2f);
    }
    public void MenuClick()
    {
        _SFXSource.PlayOneShot(_sfx[1], 0.3f);
    }
    public void MenuBack()
    {
        _SFXSource.PlayOneShot(_sfx[2], 0.2f);
    }

}
