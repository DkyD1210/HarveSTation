using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICookSlot : MonoBehaviour
{

    [SerializeField]
    private int Id;

    [SerializeField]
    private string ItemName;

    [SerializeField]
    private Transform TrsImage;

    [SerializeField]
    private Image ItemImage;

    [SerializeField]
    private TextMeshProUGUI ItemText;


    private void Start()
    {
        TrsImage = transform.Find("ItemImage");
    }

    private void SetText()
    {
        ItemText.text = ItemName;
    }

    private void SetImage(Sprite spr)
    {
        ItemImage = TrsImage.GetComponent<Image>();

        ItemImage.sprite = spr;
    }

    public void SetSlot(int id, string itemName)
    {
        Id = id;
        ItemName = itemName;

        Item item = GameManager.Instance.FindItemIndex(ItemName);
        Sprite spr = item.ReturnSprite();
        ItemText = GetComponentInChildren<TextMeshProUGUI>();
        SetText();

        SetImage(spr);
    }
}
