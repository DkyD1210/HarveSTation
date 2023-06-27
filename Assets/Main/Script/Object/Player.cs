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

    //매니저 인스턴스 시작
    private GameManager gameManager;

    private UIManager uIManager;

    private InventoryManager inventoryManager;
    //매니저 인스턴스 끝

    [Header("이동")]

    [Tooltip("이동속도")]
    [SerializeField]
    private float m_Speed;

    [Header("작물 수확")]

    [SerializeField]
    private List<GameObject> m_InCropsList; //범위 안 작물 오브젝트 리스트

    [SerializeField]
    private m_eCropName m_CropName;

    private bool IsMoveStop
    {
        get => UIManager.Instance.m_IsUIOpen;
    }

    private float ray = 1;//raycast 방향용

    void Start()
    {
        gameManager = GameManager.Instance;
        uIManager = UIManager.Instance;
        inventoryManager = InventoryManager.Instance;
        PlayerBox = GetComponent<BoxCollider2D>();
        PlayerRigid = GetComponent<Rigidbody2D>();
    }



    void Update()
    {
        UIAct();

        if (IsMoveStop == true)
        {
            PlayerRigid.constraints = RigidbodyConstraints2D.FreezeAll;
            return;
        }
        PlayerRigid.constraints = RigidbodyConstraints2D.FreezeRotation;

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
                Item _item = obj.GetComponent<Item>();

                if (_crop.m_CanHarvest == true)
                {
                    int SlotNum = inventoryManager.CheckEmptyInventory(_item.ReturnName());
                    if (SlotNum == -1)
                    {
                        Debug.Log("인벤토리 공간 없음");
                    }
                    else
                    {
                        Debug.Log("수확 성공");
                        _item.GetItem(SlotNum);
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
            gameManager.PlantCrop(transform.position, m_CropName);
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

    private void UIAct()
    {
        if (PlayerDir.y == 1)
        {
            ray = 1f;
        }
        else if (PlayerDir.y == -1)
        {
            ray = -1f;
        }
        RaycastHit2D hit = Physics2D.BoxCast(PlayerBox.bounds.center, new Vector2(0.1f, 0.1f), 0f, new Vector3(0f, ray, 0f), 15f, LayerMask.GetMask("InteractionObject"));
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (hit == true)
            {
                if (hit.transform.tag == "Stove")
                {
                    uIManager.SetCookUI();
                }
                else if (hit.transform.tag == "Shop")
                {
                    uIManager.SetShopUI();
                }
            }
            else
            {
                Debug.Log("상호작용 가능 물체 없음");
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            uIManager.SetInvetoryUI();
        }

    }
}
