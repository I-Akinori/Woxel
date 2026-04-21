using System;
using UnityEngine;

[Serializable]
public class MaterialControlProperty
{
    public Material material;
    public AnimationCurve curve;
    public string property;
    [Range(0, 1)] public float value;
}
