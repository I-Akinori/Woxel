using System;
using UnityEngine;

public class WinterManager : BaseIndividualSeasonManager
{
    [SerializeField] private MaterialController snowMat;
    [SerializeField] private MaterialController lightMat;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private AnimationCurve snowMatCurve;
    [SerializeField] private AnimationCurve lightMatCurve;
    [SerializeField] private AnimationCurve emitCurve;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Light sun;
    private void Update()
    {
        ratio += Time.deltaTime * 0.01f;
        if (ratio > 1)
        {
            ratio = 0;
        }
        snowMat.SetValue(snowMatCurve.Evaluate(ratio));
        lightMat.SetValue(lightMatCurve.Evaluate(ratio));
        var module = particle.emission;
        module.rateOverTime = emitCurve.Evaluate(ratio) * 50f;
        sun.color = gradient.Evaluate(ratio);
    }
}