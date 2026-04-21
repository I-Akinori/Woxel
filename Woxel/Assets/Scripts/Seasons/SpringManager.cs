using System;
using UnityEngine;

public class SpringManager : BaseIndividualSeasonManager
{
    [SerializeField] private MaterialController mat;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private AnimationCurve matCurve;
    [SerializeField] private AnimationCurve emitCurve;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Light sun;
    private void Update()
    {
        mat.SetValue(matCurve.Evaluate(ratio));
        var module = particle.emission;
        module.rateOverTime = emitCurve.Evaluate(ratio) * 20f;
        sun.color = gradient.Evaluate(ratio);
    }
}