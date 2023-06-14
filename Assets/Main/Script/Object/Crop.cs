using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{
    private Transform m_trs;

    private SpriteRenderer m_spr;

    [Header("작물 시간")]
    [SerializeField]
    private int GrowTime = 5;

    //시간 경과
    [SerializeField]
    private int m_Time = 0;

    private int beforeTime;

    private bool m_Grow = false;

    [SerializeField]
    private bool m_CanHarvest = false;

    [SerializeField]
    private m_eHarvestType m_HarvestType;

    void Start()
    {
        m_spr = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        Grow();
        CheckHarvest();
    }

    private void Grow()
    {
        if (m_Grow == true)
        {
            return;
        }

        if (TimeManager.instance.CheckDay() != beforeTime)
        {
            m_Time++;
            beforeTime = TimeManager.instance.CheckDay();
        }
        if (m_Time >= GrowTime)
        {
            m_Grow = true;
            m_spr.color = Color.red;
        }
    }

    private void CheckHarvest()
    {
        if(m_Grow == false)
        {
            return;
        }
        switch (m_HarvestType)
        {
            case (m_eHarvestType.CanMorning):
                if (TimeManager.instance.GameDay == m_eGameDay.Mornig)
                {
                    m_CanHarvest = true;
                }
                else
                {
                    m_CanHarvest = false;
                }
                break;
        }


    }
}
