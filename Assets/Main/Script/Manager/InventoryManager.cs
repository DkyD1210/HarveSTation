using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    private UIManager uIManager;

    private GameManager gameManager;

    [SerializeField]
    private Transform TrsSlot;

    [SerializeField]
    private List<UIInventorySlot> m_SlotList = new List<UIInventorySlot>();

    [SerializeField]
    private Transform TrsText;

    [SerializeField]
    private List<TextMeshProUGUI> m_TextList = new List<TextMeshProUGUI>();


    [SerializeField]
    private int m_SlotIndex;
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
        uIManager = UIManager.Instance;
        gameManager = GameManager.Instance;
    }

    void Update()
    {
        SelectSlot();
    }

    private void InitManager()
    {
        m_SlotList.AddRange(TrsSlot.GetComponentsInChildren<UIInventorySlot>());
        m_TextList.AddRange(TrsText.GetComponentsInChildren<TextMeshProUGUI>());
        MaxIndex = (m_SlotList.Count - 1);
        SetItemText(0);
    }

    private void SelectSlot()
    {
        if (uIManager.m_IsUIOpen == false)
        {
            m_SlotIndex = 0;
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (m_SlotIndex > 0)
            {
                m_SlotIndex--;
            }
            SetItemText(m_SlotIndex);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (m_SlotIndex + 1 < MaxIndex)
            {
                m_SlotIndex++;
            }
            SetItemText(m_SlotIndex);
        }
    }


    private void SetItemText(int index)
    {
        UIInventorySlot item = m_SlotList[index];
        m_TextList[0].text = item.GetItemName();
        if (item.GetItemCount() == 0)
        {
            m_TextList[1].text = "None";
        }
        else
        {
            m_TextList[1].text = item.GetItemCount().ToString();
        }
    }


    public int CheckEmptyInventory(string _name)
    {
        int count = m_SlotList.Count;
        for (int i = 0; i < count; i++)
        {
            if (m_SlotList[i].GetItemName() == _name)
            {
                return i;
            }
        }
        for (int i = 0; i < count; i++)
        {
            if (m_SlotList[i].GetItemName() == string.Empty)
            {

                return i;
            }
        }
        return -1;
    }

    public int FindItemSlotIndex(string _name)
    {
        int count = m_SlotList.Count;
        for (int i = 0; i < count; i++)
        {
            if (m_SlotList[i].GetItemName() == _name)
            {
                return i;
            }
        }
        return 0;
    }

    public int FindItemCount(string _name)
    {
        int count = m_SlotList.Count;
        for (int i = 0; i < count; i++)
        {
            if (m_SlotList[i].GetItemName() == _name)
            {
                int _count = m_SlotList[i].GetItemCount();
                return _count;
            }
        }
        return 0;
    }

    public int FindItemCount(int slotNum)
    {
        if (slotNum == -1)
        {
            return 0;
        }
        return m_SlotList[slotNum].GetItemCount();
    }

    public void GetItem(int slotNum, Sprite spr, string name, int count = 1)
    {
        m_SlotList[slotNum].SetItem(name, spr, count);
    }

    public void RemoveItem(int _slotNum, int decreaseCount)
    {
        m_SlotList[_slotNum].RemoveCount(decreaseCount);
    }

}
