using System.Collections.Generic;
using UnityEngine;


public class MaterialController : MonoBehaviour
{
    [SerializeField] private List<MaterialControlProperty> settings;
    [SerializeField, Range(0, 1)] private float value;

    public void SetValue(float val)
    {
        this.value = val;
    }
    void Update()
    {
        foreach (var s in settings)
        {
            s.material.SetFloat(s.property, value);
        }
    }
}