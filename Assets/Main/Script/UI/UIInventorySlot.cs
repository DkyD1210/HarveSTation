using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIInventorySlot : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI ItemCountText;

    [SerializeField]
    private Transform UIItem;

    [SerializeField]
    private int m_ItemCount;

    [SerializeField]
    private string ItemName;

    [SerializeField]
    private Image ItemImage;

    void Start()
    {
        ItemCountText = GetComponentInChildren<TextMeshProUGUI>();
        UIItem = transform.Find("Item");
    }

    

    void Update()
    {
        SetItemCountText();
    }

    private void SetItemCountText()
    {
        if (ItemName == null)
        {
            return;
        }


        if (m_ItemCount == 0)
        {
            ItemCountText.text = string.Empty;
        }
        else
        {
            ItemCountText.text = m_ItemCount.ToString();
        }
    }


    public int SetItem(string _name, Sprite sprite)
    {
        ItemImage = UIItem.GetComponent<Image>();
        ItemImage.sprite = sprite;

        ItemName = _name;
        m_ItemCount++;

        return m_ItemCount;
    }

    public string GetItemName()
    {
        return ItemName;
    }

    public bool RemoveCount(int count)
    {
        if ((m_ItemCount - count) <= 0)
        {
            ItemImage = UIItem.GetComponent<Image>();
            ItemImage.sprite = null;

            m_ItemCount = 0;
            return true;
        }
        else
        {
            m_ItemCount -= count;
            return false;
        }
    }

}
