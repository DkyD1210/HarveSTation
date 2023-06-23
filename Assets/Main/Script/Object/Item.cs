using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    private SpriteRenderer m_spr;


    public m_eItemName m_ItemName;

    private void Start()
    {
       

    }

    public void GetItem(int _slotnum)
    {
        m_spr = GetComponent<SpriteRenderer>();
        InventoryManager.Instance.GetItem(_slotnum, m_ItemName.ToString(), m_spr.sprite);
    }

    public string ReturnName()
    {
        return m_ItemName.ToString();
    }


}
