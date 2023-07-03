using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIMaterialSlot : MonoBehaviour
{

    [SerializeField]
    private Image itemImage;

    [SerializeField]
    private TextMeshProUGUI CountText;


    public void SetMaterial(int count, Sprite spr)
    {
        CountText.text = count.ToString();
        itemImage.sprite = spr;
    }
}
