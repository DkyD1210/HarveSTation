using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Crop : MonoBehaviour
{
    public enum m_eHarvestType //��Ȯ ���� ����
    {
        Morning,
        Day,
        Noon,
        Night,
    }

    private Transform m_trs;

    private SpriteRenderer m_spr;

    [Header("�۹� �ð�")]
    [SerializeField]
    private int GrowTime = 5;


    [SerializeField]
    private int m_Time = 0; //�ð����

    private int beforeTime;

    private bool m_Grow = false; //���忩��

    public bool m_CanHarvest = false; //��Ȯ ���� ����

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

    private void Grow() //����
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
            m_spr.color = Color.red; // �� �ٲ�°� �ӽ� ��������Ʈ �ٲܶ� ����
        }
    }

    private void CheckHarvest() //��Ȯ���� ����Ȯ��
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
