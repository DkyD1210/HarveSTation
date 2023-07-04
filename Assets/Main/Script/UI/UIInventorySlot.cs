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
    private string m_ItemName = string.Empty;

    [SerializeField]
    private int m_ItemCount = 0;

    [SerializeField]
    private Image ItemImage;

    [SerializeField]
    private Sprite DefaltImage;

    void Start()
    {
        ItemCountText = GetComponentInChildren<TextMeshProUGUI>();
        UIItem = transform.Find("Item");
        ItemImage = UIItem.GetComponent<Image>();
        SetImage();
        SetItemCountText();
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


    private void SetImage(Sprite spr = null)
    {
        if (ItemImage == null)
        {
            UIItem = transform.Find("Item");
            ItemImage = UIItem.GetComponent<Image>();
        }
        if (spr == null)
        {
            spr = DefaltImage;
        }
        ItemImage.sprite = spr;
    }

    public int SetItem(string _name, Sprite sprite, int addItemCount)
    {
        SetImage(sprite);

        m_ItemName = _name;
        m_ItemCount += addItemCount;

        SetItemCountText();

        return m_ItemCount;
    }

    public string GetItemName()
    {
        return m_ItemName;
    }

    public int GetItemCount()
    {
        return m_ItemCount;
    }

    public void RemoveCount(int count)
    {
        m_ItemCount -= count;
        if (m_ItemCount <= 0)
        {
            SetImage();

            m_ItemCount = 0;
        }
        SetItemCountText();


    }

}
