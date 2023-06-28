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

    [SerializeField, Range(0, 999999999)]
    private int Money;

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

}
