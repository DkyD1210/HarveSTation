using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookManager : MonoBehaviour
{
    public static CookManager Instance;

    [SerializeField]
    private GameObject m_CookUI;

    [SerializeField]
    private Button m_Button;

    private List<GameObject> m_ItemList = new List<GameObject>();


    public bool m_IsCook
    {
        get { return m_CookUI.activeSelf; }
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
        m_Button.onClick.AddListener(SetNeedItem);
        m_ItemList = GameManager.Instance.m_ItemList;
    }

    void Update()
    {
        Cooking();
    }

    private void Cooking()
    {
        if (m_IsCook == false)
        {
            return;
        }
    }

    public void SetCookUI()
    {
        m_CookUI.SetActive(!m_CookUI.activeSelf);
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
        int CropslotNum = InventoryManager.Instance.CheckEmptyInventory(_name);
        int CropCount = InventoryManager.Instance.FindItemCount(_name);
        

        if ((CropCount - _needCount) >= 0)
        {
            
            int ItemSlotNum = InventoryManager.Instance.CheckEmptyInventory(_returnItemName);
            if (ItemSlotNum == -1)
            {
                Debug.Log("�κ��丮 ���� ����");
            }
            else
            {
                InventoryManager.Instance.RemoveItem(CropslotNum, _needCount);
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
        for (int i = 0; i < count; i++)
        {
            Item item =  m_ItemList[i].GetComponent<Item>();
            if(item.m_ItemName.ToString() == _name)
            {
                return i;
            }
        }
        return -1;
    }
}
