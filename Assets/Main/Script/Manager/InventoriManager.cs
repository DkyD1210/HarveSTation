using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoriManager : MonoBehaviour
{
    public static InventoriManager Instance;

    [SerializeField]
    private GameObject Inventori;

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

    // Update is called once per frame
    void Update()
    {
        ShowInvetori();
    }

    private void ShowInvetori()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (Inventori.activeSelf == false)
            {
                Inventori.SetActive(true);
            }
            else
            {
                Inventori.SetActive(false);
            }
        }
    }


}
