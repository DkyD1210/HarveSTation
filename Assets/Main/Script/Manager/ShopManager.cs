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
    private Transform m_TrsShopText;

    [SerializeField]
    private List<UIShopSlot> m_SlotList = new List<UIShopSlot>();

    [SerializeField]
    private List<TextMeshProUGUI> m_SlotTextList = new List<TextMeshProUGUI>();

    private int Index = 0;

    private int MaxIndex = 0;

    private GameManager gameManager;

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
        gameManager = GameManager.Instance;
        m_SlotTextList.AddRange(m_TrsShopText.GetComponentsInChildren<TextMeshProUGUI>());
        m_BeforeDay = TimeManager.Instance.CheckDay();
        ShopUpdate();
    }

    void Update()
    {
        CheckUpdate();
        SelectSlot();
    }

    private void CheckUpdate()
    {
        int day = TimeManager.Instance.CheckDay();
        if (m_BeforeDay != day)
        {
            m_BeforeDay = day;
            ShopUpdate();
        }
    }

    private void ShopUpdate()
    {
        m_SlotList.Clear();
        m_SlotList.AddRange(m_TrsShopSlot.GetComponentsInChildren<UIShopSlot>());
        MaxIndex = (m_SlotList.Count - 1);

        int count = m_SlotList.Count;
        for (int i = 0; i < count; i++)
        {            
            int rand = Random.Range(0, gameManager.GetItemCount());
            GameObject obj = gameManager.m_ItemList[rand];
            SpriteRenderer spr = obj.GetComponent<SpriteRenderer>();
            Item item = obj.GetComponent<Item>();

            m_SlotList[i].SetItemSlot(spr.sprite, item.ReturnName(), 0, 0);

        }
        ShowSlotText(m_SlotList[0]);
    }

    private void SelectSlot()
    {
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            if (Index > 0)
            {
                Index--;
            }
            ShowSlotText(m_SlotList[Index]);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            if (Index < MaxIndex)
            {
                Index++;
            }
            ShowSlotText(m_SlotList[Index]);
        }
    }

    private void ShowSlotText(UIShopSlot slot)
    {
        (string nametext, int counttext, int prisetext, bool issell) = slot.GetItemSlot();
        m_SlotTextList[0].text = nametext;
        m_SlotTextList[1].text = $"{counttext}a";
        if (issell == true)
        {
            m_SlotTextList[2].text = $"b : {prisetext}";
        }
        else
        {
            m_SlotTextList[2].text = $"c : {prisetext}";
        }
    }
}
