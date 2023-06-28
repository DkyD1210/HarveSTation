using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;

    private TimeManager timeManager;

    [SerializeField]
    private TextMeshProUGUI m_DayText; //날짜 텍스트

    [SerializeField]
    private TextMeshProUGUI m_TimeText; //시간 텍스트

    [SerializeField]
    private TextMeshProUGUI m_TimeTypeText; //시간대 텍스트

    [SerializeField]
    private TextMeshProUGUI m_WeatherText; //날씨 텍스트

    [SerializeField]
    private TextMeshProUGUI m_MoneyText; //돈 텍스트;

    [SerializeField]
    private GameObject m_CookUI;//요리창

    [SerializeField]
    private GameObject m_ShopUI;//상점창

    [SerializeField]
    private GameObject m_InventoryUI;//인벤토리



    public bool m_IsUIOpen
    {
        get
        {
            return m_CookUI.activeSelf || m_ShopUI.activeSelf || m_InventoryUI.activeSelf;
        }
    }

    private void Awake()
    {
        if (Instance == null)
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
        timeManager = TimeManager.Instance;
    }


    void Update()
    {
        PrintWeather();
    }

    private void PrintWeather()
    {
        m_eWeather weather = timeManager.m_WeaTher;

        string weathertext = "Weather : " + weather;
        m_WeatherText.text = weathertext;
    }

    public void SetTimeText(int minute, int hour, int day)
    {

        m_DayText.text = $"{day} Day";

        string timetext = string.Format("{0:D2}", hour) + ":" + string.Format("{0:D2}", minute);
        m_TimeText.text = timetext;

        m_eGameDay TimeType = timeManager.m_GameDay;
        m_TimeTypeText.text = TimeType.ToString();

    }



    public void SetCookUI()
    {
        m_CookUI.SetActive(!m_CookUI.activeSelf);
    }

    public void SetShopUI()
    {
        m_ShopUI.SetActive(!m_ShopUI.activeSelf);
    }

    public void SetInvetoryUI()
    {
        m_InventoryUI.SetActive(!m_InventoryUI.activeSelf);
    }

    public void SetMoneyText(int money)
    {
        m_MoneyText.text = $"{money}$";
    }
}
