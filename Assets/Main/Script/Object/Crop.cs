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
    [SerializeField]
    private int GrowTime = 5; //hour ���� 1�� 24

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

    void Start()
    {
        m_spr = GetComponent<SpriteRenderer>();

        beforeTime = TimeManager.Instance.CheckTime();
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
            beforeTime = TimeManager.Instance.CheckTime();
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
