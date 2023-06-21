using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [SerializeField]
    private GameObject Inventory;

    [SerializeField]
    private Transform TrsInventory;

    [SerializeField]
    private List<UIInventorySlot> m_SlotList = new List<UIInventorySlot>();

    [SerializeField]
    private List<Transform> m_TrsSlotList = new List<Transform>();

    public bool m_InventoryOpen
    {
        get => Inventory.activeSelf;
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
        m_SlotList.AddRange(TrsInventory.GetComponentsInChildren<UIInventorySlot>());
        GetSlotTranform();


    }

    // Update is called once per frame
    void Update()
    {
        ShowInvetory();
    }

    private void ShowInvetory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Inventory.SetActive(!Inventory.activeSelf);
        }
    }

    private void GetSlotTranform()
    {
        int count = m_SlotList.Count;
        for (int i = 0; i < count; i++)
        {
            m_TrsSlotList.Add(m_SlotList[i].transform);
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
        m_SlotList[SlotNum].SetItem(_name, _spr);
    }


}
