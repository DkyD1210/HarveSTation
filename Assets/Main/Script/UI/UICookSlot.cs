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
    private Image ItemImage;

    [SerializeField]
    private TextMeshProUGUI ItemText;

    private void SetText()
    {
        ItemText.text = ItemName;
    }

    private void SetImage()
    {
        Sprite spr = GameManager.Instance.FindItemIndex(ItemName).GetComponent<Sprite>();
        ItemImage.sprite = spr;
    }

    public void SetSlot(int id, string itemName)
    {
        Id = id;
        ItemName = itemName;

        ItemText = GetComponentInChildren<TextMeshProUGUI>();
        SetText();
        SetImage();
    }
}
