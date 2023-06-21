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

    [SerializeField]
    private m_eCropName m_CropName;

    private bool IsCook = false;
    

    void Start()
    {
        PlayerBox = GetComponent<BoxCollider2D>();
        PlayerRigid = GetComponent<Rigidbody2D>();
    }



    void Update()
    {
        if (InventoryManager.Instance.m_InventoryOpen == true)
        {
            return;
        }

        CookAct();

        if(IsCook == true)
        {
            return;
        }

        PlayerMove();
        HarvestAct();
        PlantCropAct();
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
                    int SlotNum = InventoryManager.Instance.CheckEmptyInventory(_crop.ReturnName());
                    if (SlotNum == -1)
                    {
                        Debug.Log("인벤토리 공간 없음");
                    }
                    else
                    {
                        Debug.Log("수확 성공");
                        _crop.GetItem(SlotNum);
                        Destroy(obj);
                    }
                }
                else
                {
                    Debug.Log("수확 불가");
                }
            }


        }
    }

    private void PlantCropAct()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            int count = System.Enum.GetValues(typeof(m_eCropName)).Length;
            m_CropName++;
            if ((int)m_CropName >= count)
            {
                m_CropName = 0;
            }

        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameManager.Instance.PlantCrop(transform.position, m_CropName);
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

    private void CookAct()
    {
        bool GetKey = Input.GetKeyDown(KeyCode.J);
        RaycastHit2D hit =  Physics2D.BoxCast(PlayerBox.bounds.center, new Vector2(0.1f, 0.1f), 0f, Vector3.up, 10f, LayerMask.GetMask("CookWare"));
        if (hit)
        {
            if(GetKey)
            {
                CookManager.Instance.SetCookUI();
            }
        }
    }
}
