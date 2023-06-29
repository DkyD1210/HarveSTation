using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookManager : MonoBehaviour
{
    public static CookManager Instance;

    private GameManager gameManager;

    private InventoryManager inventoryManager;

    [Header("아이템 조합법")]
    public List<Lecipe> m_Lecipes = new List<Lecipe>();

    [Space]

    [SerializeField]
    private Button m_Button;



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
        inventoryManager = InventoryManager.Instance;
        m_Button.onClick.AddListener(SetNeedItem);
    }

    private void SetNeedItem()
    {
        CookItem(m_Lecipes[0]);
    }


    private void CookItem(Lecipe _lecipe)
    {
        foreach (var _Materialitem in _lecipe.Materials)
        {
            string _MaterialName = _Materialitem.Name.ToString();
            int _Materialcount = _Materialitem.Count;

            int MaterialSlotNum = inventoryManager.CheckEmptyInventory(_MaterialName);
            int MaterialCount = inventoryManager.FindItemCount(MaterialSlotNum);
            if (MaterialCount - _Materialcount >= 0)
            {
                foreach (var _ResualtItem in _lecipe.Resualts)
                {
                    string _ResualtItemName = _ResualtItem.Name.ToString();
                    int _ResualtItemCount = _ResualtItem.Count;

                    int ItemSlotNum = inventoryManager.CheckEmptyInventory(_ResualtItemName);
                    if(ItemSlotNum == -1)
                    {
                        Debug.Log("아이템 공간 부족");
                    }
                    else
                    {
                        Item ResualtItem = gameManager.FindItemIndex(_ResualtItemName);
                        ResualtItem.GetItem(ItemSlotNum);

                        inventoryManager.RemoveItem(MaterialSlotNum, _Materialcount);
                    }
                }
            }
            else
            {
                Debug.Log("재료부족");
            }
        }
    }


}
