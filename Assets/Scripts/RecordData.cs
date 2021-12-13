using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RecordData 
{
    public int _maxScore;

    public RecordData (GameManager gM)
    {
        _maxScore = gM._maxScore;
    }

}
