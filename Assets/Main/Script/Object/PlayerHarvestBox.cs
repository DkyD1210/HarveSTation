using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHarvestBox : MonoBehaviour
{

    private Player player;

    void Start()
    {
        player = GetComponentInParent<Player>();
    }


    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Crop")
        {
            player.InCrops(collision.gameObject, true);
            Debug.Log("´êÀ½");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Crop")
        {
            player.InCrops(collision.gameObject, false);
            Debug.Log("³ª°¨");
        }
    }
}
