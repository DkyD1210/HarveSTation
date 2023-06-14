using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeManager : MonoBehaviour
{

    public static TimeManager instance;

    [Header("시간과 날짜")]
    //초
    private float m_MainSecond;
    private int m_MaxSecond = 60;
    //분
    [SerializeField]
    private int m_MainMinute;
    private int m_MaxMinute = 60;

    //시
    [SerializeField]
    private int m_MainHour;
    private int m_MaxHour = 24;

    //날
    [SerializeField]
    private int m_MainDay;

    [Tooltip("현실 1초당 인게임 초 값")]
    [SerializeField]
    private int m_GameTime = 42;

    [Header("시간대")]
    public m_eGameDay GameDay;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {

    }

    void Update()
    {
        SetTime();
        Debug.Log($"{m_MainDay} : {m_MainHour} : {m_MainMinute} : {m_MainSecond}");
    }

    private void SetTime()
    {
        m_MainSecond += Time.deltaTime * m_GameTime;
        if (m_MainSecond >= m_MaxSecond)
        {
            m_MainSecond = 0;
            m_MainMinute++;
        }
        if (m_MainMinute >= m_MaxMinute)
        {
            m_MainMinute = 0;
            m_MainHour++;
        }
        if (m_MainHour >= m_MaxHour)
        {
            m_MainHour = 0;
            m_MainDay++;
        }

        switch (m_MainHour)
        {
            case (0):
                GameDay = m_eGameDay.Mornig;
                break;
            case (6):
                GameDay = m_eGameDay.Day;
                break;
            case (12):
                GameDay = m_eGameDay.Noon;
                break;
            case (18):
                GameDay = m_eGameDay.Night;
                break;
        }
    }

    public int CheckDay()
    {
        int _day = m_MainDay;
        return _day;
    }
}
