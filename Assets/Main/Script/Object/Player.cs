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

    //�Ŵ��� �ν��Ͻ� ����
    private GameManager gameManager;

    private UIManager uIManager;

    private InventoryManager inventoryManager;
    //�Ŵ��� �ν��Ͻ� ��

    [Header("�̵�")]

    [Tooltip("�̵��ӵ�")]
    [SerializeField]
    private float m_Speed;

    [Header("�۹� ��Ȯ")]

    [SerializeField]
    private List<GameObject> m_InCropsList; //���� �� �۹� ������Ʈ ����Ʈ

    [SerializeField]
    private m_eCropName m_CropName;

    private bool IsMoveStop
    {
        get => UIManager.Instance.m_IsUIOpen;
    }

    private float ray = 1;//raycast �����

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
                Item _item = obj.GetComponent<Item>();

                if (_crop.m_CanHarvest == true)
                {
                    int SlotNum = inventoryManager.CheckEmptyInventory(_item.ReturnName());
                    if (SlotNum == -1)
                    {
                        Debug.Log("�κ��丮 ���� ����");
                    }
                    else
                    {
                        Debug.Log("��Ȯ ����");
                        _item.GetItem(SlotNum);
                        Destroy(obj);
                    }
                }
                else
                {
                    Debug.Log("��Ȯ �Ұ�");
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
                Debug.Log("��ȣ�ۿ� ���� ��ü ����");
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            uIManager.SetInvetoryUI();
        }

    }
}
