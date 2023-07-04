using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CookManager : MonoBehaviour
{
    public static CookManager Instance;

    private GameManager gameManager;

    private InventoryManager inventoryManager;

    private UIManager uiManager;

    [Header("아이템 조합법")]
    public List<Lecipe> m_Lecipes = new List<Lecipe>();

    [Space]

    [SerializeField]
    private Transform TrsSlot;

    [SerializeField]
    private GameObject m_Slot;

    [SerializeField]
    List<UICookSlot> m_SlotList = new List<UICookSlot>();

    [Space]
    [SerializeField]
    List<UIMaterialSlot> m_InfoSlotList = new List<UIMaterialSlot>();

    [SerializeField]
    private TextMeshProUGUI ItemText;

    [SerializeField]
    private int m_Index;
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
        gameManager = GameManager.Instance;
        inventoryManager = InventoryManager.Instance;
        uiManager = UIManager.Instance;
        MaxIndex = m_Lecipes.Count;
        SetListSlot();
    }

    private void Update()
    {
        SelctLecipe();
    }

    private void SelctLecipe()
    {
        if (uiManager.m_CookUI.activeSelf == false)
        {
            m_Index = 0;
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (m_Index > 0)
            {
                m_Index--;
            }
            SetInfo(m_Lecipes[m_Index]);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (m_Index + 1 < MaxIndex)
            {
                m_Index++;
            }
            SetInfo(m_Lecipes[m_Index]);
        }

        if(Input.GetKeyDown(KeyCode.J))
        {
            CookItem(m_Lecipes[m_Index]);
        }
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
        SetInfo(m_Lecipes[0]);
    }

    private void SetInfo(Lecipe lec)
    {
        int slotcount = m_InfoSlotList.Count;
        for (int i = 0; i<slotcount; i++)
        {
            GameObject obj = m_InfoSlotList[i].gameObject;
            obj.SetActive(false);
        }
        
        ItemText.text = lec.Resualts[0].Name.ToString();
        List<Lecipe.Material> mat = lec.Materials;
        int count = mat.Count;
        for(int i = 0; i < count; i++)
        {
            GameObject obj = m_InfoSlotList[i].gameObject;
            obj.SetActive(true);
            Sprite spr = gameManager.FindItemIndex(mat[i].Name.ToString()).ReturnSprite();
            int matcount = mat[i].Count;
            m_InfoSlotList[i].SetMaterial(matcount, spr);
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
