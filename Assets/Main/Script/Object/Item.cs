using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    private SpriteRenderer m_spr;

    [SerializeField]
    private m_eItemName m_ItemName;


    private void Start()
    {
        m_spr = GetComponent<SpriteRenderer>();
    }

    public void GetItem(int _slotnum, int count = 1)
    {
        m_spr = GetComponent<SpriteRenderer>();
        InventoryManager.Instance.GetItem(_slotnum, m_spr.sprite, m_ItemName.ToString(), count);
    }

    public string ReturnName()
    {
        return m_ItemName.ToString();
    }


}
