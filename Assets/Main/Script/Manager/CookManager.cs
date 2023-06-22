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

    [SerializeField]
    private List<GameObject> m_ItemList;
    
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
        UseItem("Potato", 8, m_eItemName.FridePotato.ToString());
    }

    private void UseItem(string _name, int _needCount, string _returnItemName)
    {
        int CropslotNum = InventoryManager.Instance.CheckEmptyInventory(_name);
        int CropCount = InventoryManager.Instance.FindItemCount(_name);
        int ItemSlotNum = InventoryManager.Instance.CheckEmptyInventory(_returnItemName);

        if ((CropCount - _needCount) >= 0)
        {
            if (ItemSlotNum == -1)
            {
                Debug.Log("인벤토리 공간 부족");
            }
            else
            {
                InventoryManager.Instance.RemoveItem(CropslotNum, _needCount);
                //Item returnItem =
                
                Debug.Log("조리시작");
            }
        }
        else
        {
            Debug.Log("재료부족");
        }
    }
}
