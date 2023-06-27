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


    [SerializeField]
    private GameObject m_CookUI;//�丮â

    [SerializeField]
    private GameObject m_ShopUI;//����â

    [SerializeField]
    private GameObject m_Inventory;//�κ��丮

    private TimeManager timeManager;

    public bool m_IsUIOpen
    {
        get
        {
            return m_CookUI.activeSelf || m_ShopUI.activeSelf || m_Inventory.activeSelf;
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

    public void SetTimeText(int minute, int hour, int day)
    {

        m_DayText.text = $"{day} Day";

        string timetext = string.Format("{0:D2}", hour) + ":" + string.Format("{0:D2}", minute);
        m_TimeText.text = timetext;

        m_eGameDay TimeType = timeManager.m_GameDay;
        m_TimeTypeText.text = TimeType.ToString();

    }

    private void PrintWeather()
    {
        m_eWeather weather = timeManager.m_WeaTher;

        string weathertext = "Weather : " + weather;
        m_WeatherText.text = weathertext;
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
        m_Inventory.SetActive(!m_Inventory.activeSelf);
    }
}
