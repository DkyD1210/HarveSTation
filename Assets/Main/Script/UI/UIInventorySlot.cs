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

    /// <summary>
    /// 이미지가 있을때 스프라이트 적용
    /// </summary>
    /// <param name="spr"></param>
    private void SetImage(Sprite spr)
    {
        if (ItemImage == null)
        {
            ItemImage = UIItem.GetComponent<Image>();
        }
        ItemImage.sprite = spr;

    }

    /// <summary>
    /// 이미지를 없앨때(= 기본이미지)
    /// </summary>
    private void SetImage()
    {
        if (ItemImage == null)
        {
            ItemImage = UIItem.GetComponent<Image>();
        }
        ItemImage.sprite = DefaltImage;
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
