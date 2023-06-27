using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShopSlot : MonoBehaviour
{
    private string ItemName;

    private Transform TrsImage;

    private Image ItemImage;

    private int NeedItemCount;

    private int Prise;

    [SerializeField]
    private bool m_IsSellSlot;

    public void SetItemSlot(Sprite image, string name, int discount, int prise)
    {
        TrsImage = transform.Find("Image");
        ItemImage = TrsImage.GetComponent<Image>();
        ItemImage.sprite = image;

        ItemName = name;
        NeedItemCount = discount;
        Prise = prise;
    }

    public (string name, int discount, int prise, bool slot) GetItemSlot()
    {
        return (ItemName, NeedItemCount, Prise, m_IsSellSlot);
    }

}
