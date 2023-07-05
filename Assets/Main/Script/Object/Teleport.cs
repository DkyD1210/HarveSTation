using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField]
    private m_eTeleport m_Teleport;

    private Collider2D Collider;

    private GameManager gameManager;

    [SerializeField]
    private bool m_telportstart = true;

    [SerializeField]
    private bool m_telportend = false;


    private void Start()
    {
        gameManager = GameManager.Instance;
        Collider = GetComponent<Collider2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" &&  m_telportstart == true && m_telportend == false)
        {
            Debug.Log($"{m_Teleport} ´êÀ½");
            gameManager.PlayerTeleport(m_Teleport);
            m_telportstart = false;
            m_telportend = true;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log($"{m_Teleport} ´ê´ÂÁß");
        m_telportstart = true;
        m_telportend = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        m_telportend = false;
    }

}
