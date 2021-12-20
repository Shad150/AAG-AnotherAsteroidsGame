using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPU : MonoBehaviour
{
    [SerializeField] private GameManager _GM;
    [SerializeField] private PlayerController _pC;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("1Up"))
        {
            _GM._aM.OneUp();
            _GM._pLives++;
            _GM.OneUPTextUpdate();
        }

        if (collision.collider.CompareTag("Piercing"))
        {
            _GM._aM.PowerUp();
            _pC.StartCoroutine(_pC.PiercingAmmo());
        }
        if (collision.collider.CompareTag("ShieldPU"))
        {
            _GM._aM.PowerUp();
            _pC.StartCoroutine(_pC.Shield());
        }
        if (collision.collider.CompareTag("TripleShot"))
        {
            _GM._aM.PowerUp();
            _pC.StartCoroutine(_pC.TripleShot());
        }
    }
}
