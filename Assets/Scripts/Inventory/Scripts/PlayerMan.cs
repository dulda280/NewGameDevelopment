using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMan : MonoBehaviour
{
    public InventoryScript inventory;
    private bool start = true;
    public GameObject panel;
    public ThirdPersonController controller;
    public bool buttonBool = true;
    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<Item>();
        print("item" + item);
        if (item)
        {
            inventory.AddItem(item.item, 1);
            Destroy(other.gameObject);
        }
        inventory.Save();
    }

    private void Start()
    {
        panel.SetActive(false);
        //loadInven();
        controller = GetComponent<ThirdPersonController>();
    }

    public void Update()
    {
        if (Input.GetKeyDown("i") && buttonBool == true)
        {
            openIven();
            buttonBool = false;
        }
        else if (Input.GetKeyDown("i") && buttonBool == false)
        {
            closeInven();
            buttonBool = true;
        }
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }
    private void loadInven()
    {
        inventory.Load();
        start = false;
    }
    public void openIven()
    {
        panel.SetActive(true);
        print("hej");
    }
    public void closeInven()
    {
        panel.SetActive(false);
        print("hejsa");
    }
 
}
