using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    private GameManager gameManager;

    private UIManager uiManager;

    private InventoryManager inventoryManager;

    private int m_BeforeDay;

    [SerializeField]
    private Transform m_TrsShopSlot;

    [SerializeField]
    private Transform m_TrsShopText;

    [SerializeField]
    private List<UIShopSlot> m_SlotList = new List<UIShopSlot>();

    [SerializeField]
    private List<TextMeshProUGUI> m_SlotTextList = new List<TextMeshProUGUI>();

    [SerializeField]
    private int m_SlotIndex = 0;

    [SerializeField]
    private int MaxIndex = 0;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitManager();
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        gameManager = GameManager.Instance;
        uiManager = UIManager.Instance;
        inventoryManager = InventoryManager.Instance;

        m_BeforeDay = TimeManager.Instance.CheckDay();
        ShopUpdate();
    }

    void Update()
    {
        CheckUpdate();
        SelectSlot();
    }

    private void InitManager()
    {
        m_SlotTextList.AddRange(m_TrsShopText.GetComponentsInChildren<TextMeshProUGUI>());
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


        int count = m_SlotList.Count;
        for (int i = 0; i < count; i++)
        {
            int rand = Random.Range(0, gameManager.GetItemCount());
            GameObject obj = gameManager.m_ItemList[rand];
            SpriteRenderer spr = obj.GetComponent<SpriteRenderer>();
            Item item = obj.GetComponent<Item>();

            int disCount = Random.Range(1, 6);
            int prise = Random.Range(20, 101);

            m_SlotList[i].SetItemSlot(spr.sprite, item.ReturnName(), disCount, prise);
            MaxIndex++;
        }
        SetSlotText(m_SlotList[0]);
    }


    private void SelectSlot()
    {
        if (uiManager.m_IsUIOpen == false)
        {
            m_SlotIndex = 0;
            return;
        }


        MaxIndex = m_SlotList.Count;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (m_SlotIndex > 0)
            {
                m_SlotIndex--;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (m_SlotIndex + 1 < MaxIndex)
            {
                m_SlotIndex++;
            }
        }
        SetSlotText(m_SlotList[m_SlotIndex]);


        if (Input.GetKeyDown(KeyCode.K))
        {
            ActItem(m_SlotList[m_SlotIndex]);
        }
    }


    private void SetSlotText(UIShopSlot slot)
    {
        (string nametext, int counttext, int prisetext, bool issell) = slot.GetItemSlot();
        m_SlotTextList[0].text = nametext;
        m_SlotTextList[1].text = $"{counttext}a"; // $"{counttext}개"
        if (issell == true)
        {
            m_SlotTextList[2].text = $"b : {prisetext}"; // $"판매가 : {prisetext}"
        }
        else
        {
            m_SlotTextList[2].text = $"c : {prisetext}"; // $"구매가 : {prisetext}"
        }
    }


    private void ActItem(UIShopSlot slot)
    {
        (string name, int count, int prise, bool isSell) = slot.GetItemSlot();
        if (isSell == true)
        {
            int itemSlotNum = inventoryManager.FindItemSlotIndex(name);
            int itemCount = inventoryManager.FindItemCount(itemSlotNum);
            if ((itemCount - count) < 0)
            {
                Debug.Log("아이템 부족");
            }
            else
            {
                gameManager.Money += prise;
                inventoryManager.RemoveItem(itemSlotNum, count);
                m_SlotList.Remove(slot);
            }
        }
        else
        {
            if ((gameManager.Money - prise) < 0)
            {
                Debug.Log("돈 부족");
            }
            else
            {
                int slotNum = inventoryManager.CheckEmptyInventory(name);
                if (slotNum == -1)
                {
                    Debug.Log("인벤토리 공간 부족");
                }
                else
                {
                    Sprite spr = slot.GetitmeSprite();
                    inventoryManager.GetItem(slotNum, spr, name, count);
                    gameManager.Money -= prise;
                }

            }
        }

    }
}
