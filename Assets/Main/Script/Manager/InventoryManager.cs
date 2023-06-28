using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    private UIManager uIManager;

    [SerializeField]
    private Transform TrsSlot;

    [SerializeField]
    private List<UIInventorySlot> m_SlotList = new List<UIInventorySlot>();

    [SerializeField]
    private Transform TrsText;

    [SerializeField]
    private List<TextMeshProUGUI> m_TextList = new List<TextMeshProUGUI>();

    private List<string> m_ItemNameList = new List<string>();

    private List<int> m_ItemCountList = new List<int>();

    [SerializeField]
    private int SlotIndex;
    [SerializeField]
    private int MaxIndex = 0;

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
        uIManager = UIManager.Instance;
        m_SlotList.AddRange(TrsSlot.GetComponentsInChildren<UIInventorySlot>());
        m_TextList.AddRange(TrsText.GetComponentsInChildren<TextMeshProUGUI>());
        GetSlotTranform();

    }

    void Update()
    {
        SelectSlot();
    }

    private void GetSlotTranform()
    {
        int count = m_SlotList.Count;
        for (int i = 0; i < count; i++)
        {
            m_ItemNameList.Add(string.Empty);
            m_ItemCountList.Add(0);
            MaxIndex++;
        }
        SetItemText(0);
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

    public void GetItem(int SlotNum, string _name, Sprite _spr)
    {
        m_ItemNameList[SlotNum] = _name;
        m_ItemCountList[SlotNum] = m_SlotList[SlotNum].SetItem(_name, _spr);
    }

    public int FindItemCount(string _name)
    {
        int count = m_ItemNameList.Count;
        for (int i = 0; i < count; i++)
        {
            if (m_ItemNameList[i] == _name)
            {
                return m_ItemCountList[i];
            }
        }
        return 0;
    }

    public void RemoveItem(int _slotNum, int decreaseCount)
    {
        m_ItemCountList[_slotNum] -= decreaseCount;
        bool IsRemove = m_SlotList[_slotNum].RemoveCount(decreaseCount);
        if (IsRemove)
        {
            m_ItemNameList.RemoveAt(_slotNum);
            m_ItemCountList.RemoveAt(_slotNum);
        }
    }

    private void SetItemText(int index)
    {
        m_TextList[0].text = m_ItemNameList[index].ToString();
        m_TextList[1].text = m_ItemCountList[index].ToString();
    }

    private void SelectSlot()
    {
        if(uIManager.m_IsUIOpen == false)
        {
            SlotIndex = 0;
            return;
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            if (SlotIndex > 0)
            {
                SlotIndex--;
            }
            SetItemText(SlotIndex);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            if (SlotIndex +1 < MaxIndex)
            {
                SlotIndex++;
            }
            SetItemText(SlotIndex);
        }
    }
}
