using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    private int m_BeforeDay;

    [SerializeField]
    private Transform m_TrsShopSlot;

    [SerializeField]
    private List<UIShopSlot> m_SlotList = new List<UIShopSlot>();

    private TextMeshProUGUI m_ItemNameText;

    private TextMeshProUGUI m_ItemDiscountText;

    private TextMeshProUGUI m_ItemPriceText;

    private int Index;


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
        m_SlotList.AddRange(m_TrsShopSlot.GetComponentsInChildren<UIShopSlot>());
        m_BeforeDay = TimeManager.Instance.CheckDay();
    }

    void Update()
    {
        CheckUpdate();
        SelectSlot();
    }

    private void CheckUpdate()
    {
        int day = TimeManager.Instance.CheckDay();
        if(m_BeforeDay != day)
        {
            m_BeforeDay = day;
            ShopUpdate();
        }
    }
    private void ShopUpdate()
    {

    }

    private void SelectSlot()
    {
        if(Input.GetKeyUp(KeyCode.LeftArrow))
        {
            Index++;
            UIShopSlot slot = m_SlotList[Index];
            ShowSlotText(slot);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            Index--;
            UIShopSlot slot = m_SlotList[Index];
            ShowSlotText(slot);
        }

    }

    private void ShowSlotText(UIShopSlot slot)
    {
        m_ItemNameText.text = "1";
        m_ItemDiscountText.text = "2";
        m_ItemPriceText.text = "3";
    }
}
