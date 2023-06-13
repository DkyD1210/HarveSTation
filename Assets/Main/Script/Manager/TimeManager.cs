using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{

    private int MaxMinit = 60;

    private float m_GameTime;

    void Start()
    {

    }

    void Update()
    {
        
        SetTime();
        Debug.Log(m_GameTime);
    }

    private void SetTime()
    {
        m_GameTime += Time.deltaTime * 10;
        if(m_GameTime >= MaxMinit)
        {
            m_GameTime = 0;
        }
    }
}
