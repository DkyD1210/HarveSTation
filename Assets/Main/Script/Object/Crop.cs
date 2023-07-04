using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Crop : MonoBehaviour
{

    //��Ȯ ���� ���ǵ�
    public enum m_eTimeType //�ð�Ÿ��
    {
        Anything,
        Morning,
        Day,
        Night,
    }

    public enum m_eWeatherType //����Ÿ��
    {
        Anythnig,
        Sun,
        Wind,
        Rain,
        heat,
    }

    private Transform m_trs;

    private SpriteRenderer m_spr;

    [Header("�۹� ����")]

    [Tooltip("��� ��¥")]
    [SerializeField]
    private int m_GrowDay;

    [SerializeField]
    private int m_GrowHour;

    [SerializeField]
    private int m_GrowMinute;

    [SerializeField]
    private int GrowTime; //�� �ڶ�� �ð�

    [SerializeField]
    private int m_Time = 0; //�ð����;

    private int beforeTime;

    private bool m_Grow = false; //���忩��

    public bool m_CanHarvest = false; //��Ȯ ���� ����

    [Header("�۹� ��Ȯ ����")]
    [Tooltip("�۹� ��Ȯ ���� �ð�")]
    [SerializeField]
    private m_eTimeType m_TimeType;

    [Tooltip("�۹� ��Ȯ ���� ����")]
    [SerializeField]
    private m_eWeatherType m_WeatherType;

    [Header("�۹� �̸�")]
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

    private void Grow() //����
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
            m_spr.color = Color.red; // �� �ٲ�°� �ӽ� ��������Ʈ �ٲܶ� ����
        }
    }

    private void CheckHarvest() //��Ȯ���� ����Ȯ��
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
