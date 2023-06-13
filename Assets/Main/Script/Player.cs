using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    //박스콜라이더
    private BoxCollider2D PlayerBox;

    //리지드바디
    private Rigidbody2D PlayerRigid;

    //플레이어 이동벡터값
    private Vector3 PlayerDir;


    [Header("이동")]

    [Tooltip("이동속도")]
    [SerializeField]
    private float m_Speed;



    void Start()
    {
        PlayerBox = GetComponent<BoxCollider2D>();
        PlayerRigid = GetComponent<Rigidbody2D>();
    }

    

    void Update()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        PlayerDir.x = Input.GetAxisRaw("Horizontal");
        PlayerDir.y = Input.GetAxisRaw("Vertical");

        PlayerRigid.velocity = PlayerDir * m_Speed;
    }
}
