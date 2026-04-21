using System;
using UnityEngine;

public class BaseIndividualSeasonManager : MonoBehaviour
{
    [SerializeField, Range(0, 1)] protected float ratio = 0;
    
    public void SetRatio(float val)
    {
        this.ratio = val;
    }
}