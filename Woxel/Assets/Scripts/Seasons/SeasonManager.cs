using System;
using UnityEngine;

public enum Season
{
    Spring,
    Summer,
    Autumn,
    Winter
}
public class SeasonManager : MonoBehaviour
{
    [SerializeField, Range(0, 1)] protected float ratio = 0;
    [SerializeField] private Season season;
    [SerializeField] private SpringManager spring;
    [SerializeField] private SummerManager summer;
    [SerializeField] private AutumnManager autumn;
    [SerializeField] private WinterManager winter;
    [SerializeField] private BaseIndividualSeasonManager currentManager;
    [SerializeField] private Light light;

    void Start()
    {
        this.season = Season.Spring;
        this.currentManager = spring;
        this.currentManager.gameObject.SetActive(true);
    }

    private void GoToNextSeason()
    {
        this.currentManager.SetRatio(0);
        this.currentManager.gameObject.SetActive(false);
        switch (season)
        {
            case Season.Spring:
                season = Season.Summer;
                this.currentManager = summer;
                break;
            case Season.Summer:
                season = Season.Autumn;
                this.currentManager = autumn;
                break;
            case Season.Autumn:
                season = Season.Winter;
                this.currentManager = winter;
                break;
            case Season.Winter:
                season = Season.Spring;
                this.currentManager = spring;
                break;
            default:
                break;
        }
        this.currentManager.gameObject.SetActive(true);
        this.currentManager.SetRatio(0);
    }
    private void Update()
    {
        this.ratio += Time.deltaTime * 0.015f;
        if (ratio > 1)
        {
            ratio = 0;
            this.GoToNextSeason();
        }
        else
        {
            this.currentManager.SetRatio(ratio);
        }

        var angle = -90f + 60f * (int)season;
        if (season == Season.Winter)
        {
            angle += ratio * 60 * 3;
        }
        else
        {
            angle += ratio * 60;
        }
        light.transform.rotation = Quaternion.Euler(50, angle, 0);
    }
}