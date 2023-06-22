using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    [Header("작물 프리팹 리스트")]
    [SerializeField]
    private List<GameObject> m_CropObjList = new List<GameObject>();

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



    void Start()
    {
        
    }


    void Update()
    {

    }

    public void PlantCrop(Vector3 _playertrs, m_eCropName _name)
    {
        Instantiate(m_CropObjList[(int)_name], _playertrs, Quaternion.identity, m_TrsCropLayer);
    }
}
