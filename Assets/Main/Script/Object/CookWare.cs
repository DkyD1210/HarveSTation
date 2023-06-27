using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookWare : MonoBehaviour
{
    [SerializeField] Transform trsPlayer;
    [SerializeField, Range(0.1f, 100.0f)] float fDistance;

    public bool IsSelected;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (IsSelected)
        {
            Debug.Log("a");
            //���غ��� ����ﶧ
        }
        else
        {
            //���غ��� �ֶ� 
        }
    }

    private bool isClosedPlayer()
    {
        return Vector3.Distance(transform.position, trsPlayer.position) < fDistance;
    }
}
