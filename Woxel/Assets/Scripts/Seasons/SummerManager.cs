using System;
using System.Collections.Generic;
using UnityEngine;

public class SummerManager : BaseIndividualSeasonManager
{
    [SerializeField] private MaterialController mat;
    [SerializeField] private GameObject cloud;
    [SerializeField] private GameObject cloud1;
    [SerializeField] private GameObject cloud2;
    [SerializeField] private GameObject cloud3;
    [SerializeField] private GameObject plane;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private AnimationCurve matCurve;
    [SerializeField] private AnimationCurve emitCurve;
    [SerializeField] private AnimationCurve cloudCurve;
    [SerializeField] private AnimationCurve cloud1Curve;
    [SerializeField] private AnimationCurve cloud2Curve;
    [SerializeField] private AnimationCurve cloud3Curve;
    [SerializeField] private AnimationCurve planeCurve;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Light sun;
    [SerializeField] private Light lightning;
    [SerializeField] private List<float> flashTime;
    
    private void Update()
    {
        mat.SetValue(matCurve.Evaluate(ratio));
        var module = particle.emission;
        module.rateOverTime = emitCurve.Evaluate(ratio) * 500f;
        cloud.transform.position 
            = new Vector3(Mathf.Lerp(-5, 15, cloudCurve.Evaluate(ratio)), 
                cloud.transform.position.y, cloud.transform.position.z);
        
        cloud1.transform.position 
            = new Vector3(Mathf.Lerp(-5, 15, cloud1Curve.Evaluate(ratio)), 
                cloud1.transform.position.y, cloud1.transform.position.z);
        
        cloud2.transform.position 
            = new Vector3(Mathf.Lerp(-5, 15, cloud2Curve.Evaluate(ratio)), 
                cloud2.transform.position.y, cloud2.transform.position.z);
        
        cloud3.transform.position 
            = new Vector3(Mathf.Lerp(-5, 15, cloud3Curve.Evaluate(ratio)), 
                cloud3.transform.position.y, cloud3.transform.position.z);
        
        plane.transform.position 
            = new Vector3(Mathf.Lerp(15, -5, planeCurve.Evaluate(ratio)), 
                plane.transform.position.y, plane.transform.position.z);
        
        sun.color = gradient.Evaluate(ratio);
        lightning.gameObject.SetActive(false);
        foreach (var f in flashTime)
        {
            if (f < ratio && ratio < f + 0.0005f)
            {
                lightning.gameObject.SetActive(true);
                break;
            }
        }
    }
}