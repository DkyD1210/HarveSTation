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
    private string m_ItemName;

    [SerializeField]
    private Image ItemImage;

    void Start()
    {
        ItemCountText = GetComponentInChildren<TextMeshProUGUI>();
        UIItem = transform.Find("Item");
    }

    private void SetItemCountText()
    {
        if (m_ItemCount == 0)
        {
            ItemCountText.text = string.Empty;
        }
        else
        {
            ItemCountText.text = m_ItemCount.ToString();
        }
    }


    public int SetItem(string _name, Sprite sprite, int addItemCount = 1)
    {
        ItemImage = UIItem.GetComponent<Image>();
        ItemImage.sprite = sprite;

        m_ItemName = _name;
        m_ItemCount += addItemCount;

        SetItemCountText();

        return m_ItemCount;
    }

    public string GetItemName()
    {
        return m_ItemName;
    }

    public bool RemoveCount(int count)
    {
        if ((m_ItemCount - count) <= 0)
        {
            ItemImage = UIItem.GetComponent<Image>();
            ItemImage.sprite = null;

            m_ItemCount = 0;
            SetItemCountText();
            return true;
        }
        else
        {
            m_ItemCount -= count;
            SetItemCountText();
            return false;
        }
        
    }

}
