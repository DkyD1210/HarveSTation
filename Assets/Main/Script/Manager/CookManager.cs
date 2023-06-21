using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookManager : MonoBehaviour
{
    public static CookManager Instance;

    [SerializeField]
    private GameObject m_CookUI;


    public bool m_IsCook
    {
        get { return m_CookUI.activeSelf; }
    }

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
        Cooking();
    }

    private void Cooking()
    {
        if(m_IsCook == false)
        {
            return;
        }
    }

    public void SetCookUI()
    {
        m_CookUI.SetActive(!m_CookUI.activeSelf);
    }
}
