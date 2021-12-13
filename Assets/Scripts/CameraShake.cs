using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 _origPos = transform.position;

        float elapsed = 0.0f;

        while(elapsed < duration)
        {
            float x = Random.Range(-1, 1) * magnitude;      //El desplazamiento de la camara en x
            float y = Random.Range(-1, 1) * magnitude;      //Lo mismo pero en la y

            transform.localPosition = new Vector3(x, y, _origPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = _origPos;

    }
}
