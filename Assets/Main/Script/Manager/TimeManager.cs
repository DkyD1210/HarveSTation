using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TimeManager : MonoBehaviour
{

    public static TimeManager Instance;

    [Header("�ð��� ��¥")]
    //��
    private float m_MainSecond;
    private int m_MaxSecond = 60;
    //��
    public int m_MainMinute;
    private int m_MaxMinute = 60;

    //��
    public int m_MainHour;
    private int m_MaxHour = 24;

    //��
    public int m_MainDay = 1;

    [Tooltip("���� 1�ʴ� �ΰ��� �� ��")]
    [SerializeField]
    private int m_GameTime = 42;

    [Header("�ð���")]
    public m_eGameDay m_GameDay;

    [SerializeField]
    private bool TimeStop = false;

    
    [Header("����")]
    public m_eWeather m_WeaTher;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
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
        //Debug.Log($"{m_MainDay} : {m_MainHour} : {m_MainMinute} : {m_MainSecond}");
    }

    private void SetTime()
    {
        if(TimeStop == true)
        {
            return;
        }

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
            SetWeather();
        }

        switch (m_MainHour)
        {
            case (6):
                m_GameDay = m_eGameDay.Morning;
                break;
            case (12):
                m_GameDay = m_eGameDay.Day;
                break;
            case (18):
                m_GameDay = m_eGameDay.Night;
                break;
        }
    }

    public int CheckDay()
    {
        int _day = m_MainDay;
        return _day;
    }

    private void SetWeather()
    {
        int weather = Random.Range(0, 3);
        m_WeaTher = (m_eWeather)weather;
    }
}
