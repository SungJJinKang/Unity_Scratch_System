using System;
using System.Collections;
using UnityEngine;

public class WorldSystem : MonoBehaviour
{
    public static WorldSystem instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartUpdateWorldDateTimeCoroutine();
    }

    private void OnWorldCreated()
    {
        InitWorldDateTime();
    }

    /// <summary>
    /// The init world year.
    /// Google Deepmind Challenge match : 2016/3/9
    /// Alphago Beat Lee sedol through Go(Baduk)
    /// After 10 years, Finally AI is tring Destroying Humanity
    /// </summary>
    private const int initWorldYear = 2026;
    private const int initWorldMonth = 3;
    private const int initWorldDay = 9;
    //


    /// <summary>
    /// DateTime In Game World
    /// This is added with deltaTime(Unscaled Time) !!!!!!!!!!!!!!!, not RealTime(Scaled Time)
    /// </summary>
    private DateTime WorldDateTime;
    private void InitWorldDateTime()
    {
        this.WorldDateTime = new DateTime(initWorldYear, initWorldMonth, initWorldDay);
    }

    private const float ScaledTimeForADay = 5.0f;

    private void StartUpdateWorldDateTimeCoroutine()
    {
        StopUpdateWorldDateTimeCoroutine();

        UpdateWorldDateTimeCoroutine = StartCoroutine(UpdateWorldDateTimeIEnumerator());
    }

    private void StopUpdateWorldDateTimeCoroutine()
    {
        if (UpdateWorldDateTimeCoroutine != null)
        {
            StopCoroutine(UpdateWorldDateTimeCoroutine);
            UpdateWorldDateTimeCoroutine = null;
        }
    }

    private Coroutine UpdateWorldDateTimeCoroutine;
    IEnumerator UpdateWorldDateTimeIEnumerator()
    {
        while (true)
        {
            yield return new WaitForSeconds(ScaledTimeForADay);

            if (WorldDateTime == null)
                this.InitWorldDateTime();

            WorldDateTime.AddDays(1);
        }

    }


}
