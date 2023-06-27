using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    [Header("아이템 리스트")]
    public List<GameObject> m_ItemList;

    [SerializeField]
    private Transform m_TrsCropLayer;

    [SerializeField]
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

    public int GetItemCount()
    {
        return m_ItemList.Count;
    }
    public void PlantCrop(Vector3 _playertrs, m_eCropName _name)
    {
        Instantiate(m_ItemList[(int)_name], _playertrs, Quaternion.identity, m_TrsCropLayer);
    }
}
