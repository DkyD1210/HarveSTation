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

    [Header("�۹� ��Ȯ")]

    [SerializeField]
    private List<GameObject> m_InCropsList; //���� �� �۹� ������Ʈ ����Ʈ

    void Start()
    {
        PlayerBox = GetComponent<BoxCollider2D>();
        PlayerRigid = GetComponent<Rigidbody2D>();
    }



    void Update()
    {
        PlayerMove();
        HarvestAct();
    }

    private void PlayerMove()
    {
        PlayerDir.x = Input.GetAxisRaw("Horizontal");
        PlayerDir.y = Input.GetAxisRaw("Vertical");

        PlayerRigid.velocity = PlayerDir * m_Speed;
    }


    private void HarvestAct() //��Ȯ���
    {
        List<GameObject> DestroyCrops = new List<GameObject>();
        int count = m_InCropsList.Count;
        if (count == 0)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            for (int i = (count - 1); i >= 0; i--)
            {
                GameObject obj = m_InCropsList[i];
                Crop _crop = obj.GetComponent<Crop>();

                if (_crop.m_CanHarvest == true)
                {
                    Debug.Log("��Ȯ ����");
                    Destroy(obj);
                }
                else
                {
                    Debug.Log("��Ȯ �Ұ�");
                }
            }


        }

    }

    public void InCrops(GameObject _obj, bool _bool)
    {
        if (_bool == true)
        {
            m_InCropsList.Add(_obj.gameObject);
        }
        else
        {
            m_InCropsList.Remove(_obj.gameObject);
        }
    }
}
