using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookManager : MonoBehaviour
{
    public static CookManager Instance;


    [SerializeField]
    private Button m_Button;

    private List<GameObject> m_ItemList = new List<GameObject>();

    private InventoryManager inventoryManager;


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
        m_Button.onClick.AddListener(SetNeedItem);
        m_ItemList = GameManager.Instance.m_ItemList;
        inventoryManager = InventoryManager.Instance;
    }

    private void SetNeedItem()
    {
        UseItem(m_eItemName.Potato.ToString(), 8, m_eItemName.FridePotato.ToString());
    }

    /// <summary>
    /// ��ắȯ
    /// </summary>
    /// <param name="_name">�ʿ��� ��� ������ �̸�</param>
    /// <param name="_needCount">�ʿ��� ��� ������ ����</param>
    /// <param name="_returnItemName">��ȯ ������ �̸�</param>
    private void UseItem(string _name, int _needCount, string _returnItemName)
    {
        int CropslotNum = inventoryManager.CheckEmptyInventory(_name);
        int CropCount = inventoryManager.FindItemCount(_name);
        

        if ((CropCount - _needCount) >= 0)
        {
            
            int ItemSlotNum = inventoryManager.CheckEmptyInventory(_returnItemName);
            if (ItemSlotNum == -1)
            {
                Debug.Log("�κ��丮 ���� ����");
            }
            else
            {
                inventoryManager.RemoveItem(CropslotNum, _needCount);
                GameObject returnItemobj = m_ItemList[FindItemIndex(_returnItemName)];
                Item returnItem = returnItemobj.GetComponent<Item>();

                returnItem.GetItem(ItemSlotNum);
                
                Debug.Log("��������");
            }
        }
        else
        {
            Debug.Log("������");
        }
    }

    private int FindItemIndex(string _name)
    {
        int count = m_ItemList.Count;
        for (int Index = 0; Index < count; Index++)
        {
            Item item =  m_ItemList[Index].GetComponent<Item>();
            if(item.ReturnName() == _name)
            {
                return Index;
            }
        }
        return -1;
    }
}
