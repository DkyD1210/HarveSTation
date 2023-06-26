using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;


    [SerializeField]
    private Transform TrsInventory;

    [SerializeField]
    private List<UIInventorySlot> m_SlotList = new List<UIInventorySlot>();


    private List<string> m_ItemNameList = new List<string>();

    private List<int> m_ItemCountList = new List<int>();


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
        m_SlotList.AddRange(TrsInventory.GetComponentsInChildren<UIInventorySlot>());
        GetSlotTranform();

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void GetSlotTranform()
    {
        int count = m_SlotList.Count;
        for (int i = 0; i < count; i++)
        {
            m_ItemNameList.Add(string.Empty);
            m_ItemCountList.Add(0);
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
}
