using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Crop : MonoBehaviour
{

    //수확 가능 조건들
    public enum m_eTimeType //시간타입
    {
        Anything,
        Morning,
        Day,
        Night,
    }

    public enum m_eWeatherType //날씨타입
    {
        Anythnig,
        Sun,
        Wind,
        Rain,
        heat,
    }

    private Transform m_trs;

    private SpriteRenderer m_spr;

    [Header("작물 시간")]
    [SerializeField]
    private int GrowTime = 5;


    [SerializeField]
    private int m_Time = 0; //시간경과

    private int beforeTime;

    private bool m_Grow = false; //성장여부

    public bool m_CanHarvest = false; //수확 가능 여부

    [SerializeField]
    private m_eTimeType m_TimeType;

    [SerializeField]
    private m_eWeatherType m_WeatherType;

    void Start()
    {
        m_spr = GetComponent<SpriteRenderer>();

        beforeTime = TimeManager.Instance.CheckDay();
    }


    void Update()
    {
        Grow();
        CheckHarvest();
    }

    private void Grow() //성장
    {
        if (m_Grow == true)
        {
            return;
        }

        if (TimeManager.Instance.CheckDay() != beforeTime)
        {
            m_Time++;
            beforeTime = TimeManager.Instance.CheckDay();
        }
        if (m_Time >= GrowTime)
        {
            m_Grow = true;
            m_spr.color = Color.red; // 색 바뀌는건 임시 스프라이트 바꿀때 삭제
        }
    }

    private void CheckHarvest() //수확가능 조건확인
    {
        if (m_Grow == false)
        {
            return;
        }

        if (m_TimeType == m_eTimeType.Anything && m_WeatherType == m_eWeatherType.Anythnig)
        {
            m_CanHarvest = true;
        }
        else
        {
            m_eWeather EWeather = TimeManager.Instance.m_WeaTher;
            m_eGameDay EDay = TimeManager.Instance.m_GameDay;
            if (m_TimeType.ToString() == EDay.ToString() && m_WeatherType == m_eWeatherType.Anythnig) 
            {
                m_CanHarvest = true;
            }
            else if(m_TimeType == m_eTimeType.Anything && m_WeatherType.ToString() == EWeather.ToString())
            {
                m_CanHarvest = true;
            }
            else if(m_TimeType.ToString() == EDay.ToString() && m_WeatherType.ToString() == EWeather.ToString())
            {
                m_CanHarvest = true;
            }
            else
            {
                m_CanHarvest = false;
            }
        }
    }

}
