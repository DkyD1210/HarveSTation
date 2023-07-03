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
    private TextMeshProUGUI m_DayText; //��¥ �ؽ�Ʈ

    [SerializeField]
    private TextMeshProUGUI m_TimeText; //�ð� �ؽ�Ʈ

    [SerializeField]
    private TextMeshProUGUI m_TimeTypeText; //�ð��� �ؽ�Ʈ

    [SerializeField]
    private TextMeshProUGUI m_WeatherText; //���� �ؽ�Ʈ

    [SerializeField]
    private TextMeshProUGUI m_MoneyText; //�� �ؽ�Ʈ;

    [SerializeField]
    private GameObject m_CookUI;//�丮â

    [SerializeField]
    private GameObject m_ShopUI;//����â

    [SerializeField]
    private GameObject m_InventoryUI;//�κ��丮



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
        AwakeUI();
    }


    void Update()
    {
        PrintWeather();
        OffUI();
    }

    private void PrintWeather()
    {
        m_eWeather weather = timeManager.m_WeaTher;

        string weathertext = "Weather : " + weather;
        m_WeatherText.text = weathertext;
    }

    private void OffUI()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (m_IsUIOpen == true)
            {
                if(m_CookUI.activeSelf == true)
                {
                    m_CookUI.SetActive(false);
                }
                if (m_InventoryUI.activeSelf == true)
                {
                    m_InventoryUI.SetActive(false);
                }
                if (m_ShopUI.activeSelf == true)
                {
                    m_ShopUI.SetActive(false);
                }
            }
        }
    }

    public void SetTimeText(int minute, int hour, int day)
    {

        m_DayText.text = $"{day} Day";

        string timetext = string.Format("{0:D2}", hour) + ":" + string.Format("{0:D2}", minute);
        m_TimeText.text = timetext;

        m_eGameDay TimeType = timeManager.m_GameDay;
        m_TimeTypeText.text = TimeType.ToString();

    }

    private void AwakeUI()
    {
        m_CookUI.SetActive(true);
        m_ShopUI.SetActive(true);
        m_InventoryUI.SetActive(true);
        m_CookUI.SetActive(false);
        m_ShopUI.SetActive(false);
        m_InventoryUI.SetActive(false);
    }


    public void SetCookUI()
    {
        m_CookUI.SetActive(true);
    }

    public void SetShopUI()
    {
        m_ShopUI.SetActive(true);
    }

    public void SetInvetoryUI()
    {
        m_InventoryUI.SetActive(true);
    }

    public void SetMoneyText(int money)
    {
        m_MoneyText.text = $"{money}$";
    }
}
