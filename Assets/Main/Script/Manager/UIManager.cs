using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;

    [SerializeField]
    private TextMeshProUGUI m_DayText; //��¥ �ؽ�Ʈ

    [SerializeField]
    private TextMeshProUGUI m_TimeText; //�ð� �ؽ�Ʈ

    [SerializeField]
    private TextMeshProUGUI m_TimeTypeText; //�ð��� �ؽ�Ʈ

    [SerializeField]
    private TextMeshProUGUI m_WeatherText; //���� �ؽ�Ʈ

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
        PrintTime();
        PrintWeather();
    }

    private void PrintTime()
    {
        int day = TimeManager.Instance.m_MainDay;

        m_DayText.text = $"{day} Day";

        int hour = TimeManager.Instance.m_MainHour;

        int min = TimeManager.Instance.m_MainMinute;

        string timetext = string.Format("{0:D2}",hour) + ":" + string.Format("{0:D2}",min);
        m_TimeText.text = timetext;

        m_eGameDay TimeType = TimeManager.Instance.m_GameDay;
        m_TimeTypeText.text = TimeType.ToString();

    }

    private void PrintWeather()
    {
        m_eWeather weather = TimeManager.Instance.m_WeaTher;

        string weathertext = "Weather : " + weather;
        m_WeatherText.text = weathertext;
    }
}
