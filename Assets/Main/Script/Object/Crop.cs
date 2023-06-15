using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Crop : MonoBehaviour
{
    public enum m_eHarvestType //수확 가능 조건
    {
        Morning,
        Day,
        Noon,
        Night,
    }

    private Transform m_trs;

    private SpriteRenderer m_spr;

    [Header("작물 시간")]
    [SerializeField]
    private int GrowTime = 5;


    [SerializeField]
    private int m_Time = 0; //시간경과

    private int beforeTime;

    private bool m_Grow = false; //성장여부

    public bool m_CanHarvest = false; //수확 가능 여부

    [SerializeField]
    private m_eHarvestType m_CanHarvestType;

    void Start()
    {
        m_spr = GetComponent<SpriteRenderer>();

        beforeTime = TimeManager.instance.CheckDay();
    }


    void Update()
    {
        Grow();
        CheckHarvest();
    }

    private void Grow() //성장
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
            m_spr.color = Color.red; // 색 바뀌는건 임시 스프라이트 바꿀때 삭제
        }
    }

    private void CheckHarvest() //수확가능 조건확인
    {

        if (m_Grow == false)
        {
            return;
        }

        if (m_CanHarvestType.ToString() == TimeManager.instance.GameDay.ToString())
        {
            m_CanHarvest = true;
        }
        else
        {
            m_CanHarvest = false;
        }


    }
}
