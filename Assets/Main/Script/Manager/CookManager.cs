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
    private Transform TrsSlot;

    [SerializeField]
    private GameObject m_Slot;

    [SerializeField]
    List<UICookSlot> m_SlotList = new List<UICookSlot>();


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
        SetListSlot();
    }

    private void SetListSlot()
    {
        int count = m_Lecipes.Count;
        for (int i = 0; i < count; i++)
        {
            GameObject slotObj = Instantiate(m_Slot, TrsSlot);
            UICookSlot slot = slotObj.GetComponent<UICookSlot>();
            
            int id = m_Lecipes[i].Id;
            Lecipe.Resualt resualt = m_Lecipes[i].Resualts[0];
            slot.SetSlot(id, resualt.Name.ToString());

            m_SlotList.Add(slot);
        }
    }

    private Lecipe FindLecipeIndex(int id)
    {
        int count = m_Lecipes.Count;
        for (int i = 0; i < count; i++)
        {
            if (m_Lecipes[i].Id == id)
            {
                return m_Lecipes[i];
            }
        }
        return m_Lecipes[0];
    }

    private void CookItem(Lecipe _lecipe)
    {
        bool noitem = true;
        List<(int, int)> _removeItemList = new List<(int, int)>();

        //재료 확인
        foreach (Lecipe.Material _Materialitem in _lecipe.Materials)
        {
            string _MaterialName = _Materialitem.Name.ToString();
            int _Materialcount = _Materialitem.Count;

            int MaterialSlotNum = inventoryManager.CheckEmptyInventory(_MaterialName);
            int MaterialCount = inventoryManager.FindItemCount(MaterialSlotNum);
            if (MaterialCount - _Materialcount >= 0)
            {
                _removeItemList.Add((MaterialSlotNum, _Materialcount));
            }
            else
            {
                Debug.Log("재료부족");
                noitem = false;
            }
        }

        //재료 전부 충족
        if (noitem == true)
        {
            //빈 슬롯 확인
            foreach (Lecipe.Resualt _ResualtItem in _lecipe.Resualts)
            {
                string _ResualtItemName = _ResualtItem.Name.ToString();
                int _ResualtItemCount = _ResualtItem.Count;

                int ItemSlotNum = inventoryManager.CheckEmptyInventory(_ResualtItemName);
                if (ItemSlotNum == -1)
                {
                    Debug.Log("아이템 공간 부족");
                }
                else
                {
                    //아이템 생성
                    Item ResualtItem = gameManager.FindItemIndex(_ResualtItemName);
                    ResualtItem.GetItem(ItemSlotNum, _ResualtItemCount);
                }
            }

            //재료 삭제
            int count = _removeItemList.Count;
            for (int i = 0; i < count; i++)
            {
                int MaterialSlotNum = _removeItemList[i].Item1;
                int MaterialCount = _removeItemList[i].Item2;

                inventoryManager.RemoveItem(MaterialSlotNum, MaterialCount);
            }
        }
        _removeItemList.Clear();
    }
}
