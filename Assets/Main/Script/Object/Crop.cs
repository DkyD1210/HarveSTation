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

    [Header("작물 성장")]

    [Tooltip("경과 날짜")]
    [SerializeField]
    private int m_GrowDay;

    [SerializeField]
    private int m_GrowHour;

    [SerializeField]
    private int m_GrowMinute;

    [SerializeField]
    private int GrowTime; //총 자라는 시간

    [SerializeField]
    private int m_Time = 0; //시간경과;

    private int beforeTime;

    private bool m_Grow = false; //성장여부

    public bool m_CanHarvest = false; //수확 가능 여부

    [Header("작물 수확 조건")]
    [Tooltip("작물 수확 가능 시간")]
    [SerializeField]
    private m_eTimeType m_TimeType;

    [Tooltip("작물 수확 가능 날씨")]
    [SerializeField]
    private m_eWeatherType m_WeatherType;

    [Header("작물 이름")]
    [SerializeField]
    private m_eCropName m_CropName;

    private TimeManager timeManager;
    void Start()
    {
        m_spr = GetComponent<SpriteRenderer>();
        SetGrowTime();

        timeManager = TimeManager.Instance;
        beforeTime = timeManager.CheckTime();
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

        if (TimeManager.Instance.CheckTime() != beforeTime)
        {
            m_Time++;
            beforeTime = timeManager.CheckTime();
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
            m_eWeather EWeather = timeManager.m_WeaTher;
            m_eGameDay EDay = timeManager.m_GameDay;
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

    private void SetGrowTime()
    {
        m_GrowHour += m_GrowDay * 24;
        m_GrowMinute += m_GrowHour * 60;
        GrowTime = m_GrowMinute;
    }

}
