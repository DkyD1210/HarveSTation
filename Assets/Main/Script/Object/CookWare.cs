using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookWare : MonoBehaviour
{
    public enum m_eCookWare
    {
        Range = 1000,
        Oven = 2000,
    }

    public m_eCookWare m_WareType;

    [SerializeField]
    private GameObject m_CookUI;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }


}
