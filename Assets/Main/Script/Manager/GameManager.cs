using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    private UIManager uiManager;

    [Header("아이템 리스트")]
    public List<GameObject> m_ItemList;


    [SerializeField]
    private Transform m_TrsCropLayer;

    [Range(0, 999999999)]
    public int Money;

    [SerializeField]


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        uiManager = UIManager.Instance;
    }

    private void Update()
    {
        uiManager.SetMoneyText(Money);
    }

    public int GetItemCount()
    {
        return m_ItemList.Count;
    }
    public void PlantCrop(Vector3 _playertrs, m_eCropName _name)
    {
        Instantiate(m_ItemList[(int)_name], _playertrs, Quaternion.identity, m_TrsCropLayer);
    }

    public Item FindItemIndex(string _name)
    {
        int count = m_ItemList.Count;
        for (int _index = 0; _index < count; _index++)
        {
            Item item = m_ItemList[_index].GetComponent<Item>();
            if (item.ReturnName() == _name)
            {
                return item;
            }
        }
        return m_ItemList[0].GetComponent<Item>();
    }


}
