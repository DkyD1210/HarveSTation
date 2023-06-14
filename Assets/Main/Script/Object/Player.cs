using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    //�ڽ��ݶ��̴�
    private BoxCollider2D PlayerBox;

    //������ٵ�
    private Rigidbody2D PlayerRigid;

    //�÷��̾� �̵����Ͱ�
    private Vector3 PlayerDir;


    [Header("�̵�")]

    [Tooltip("�̵��ӵ�")]
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
