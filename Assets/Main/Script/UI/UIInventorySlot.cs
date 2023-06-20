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
        if(ItemName == null)
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


    public void SetItem(string _name ,Sprite sprite)
    {
        Image image = UIItem.GetComponent<Image>();
        image.sprite = sprite;

        ItemName = _name;
        m_ItemCount++;
    }

    public string GetItemName()
    {
        return ItemName;
    }

}
