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

    [Header("작물 수확")]

    [SerializeField]
    private List<GameObject> m_InCropsList; //범위 안 작물 오브젝트 리스트

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


    private void HarvestAct() //수확기능
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
                    Debug.Log("수확 성공");
                    Destroy(obj);
                }
                else
                {
                    Debug.Log("수확 불가");
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
