using System;
using System.Collections;
using System.Collections.Generic;
using UFE3D;
using UnityEngine;

public class ActiveSword : MonoBehaviour
{
    public GameObject Sword1;
    public GameObject Sword2;
    
    void Start()
    {
        ActiveSwordObj();
    }
    
    /*private void FixedUpdate()
    {
        if (Sword2.activeSelf && Sword1.activeSelf)
        {
            ActiveSwordObj();
        }
    }*/
    
    private void ActiveSwordObj()
    {
        string parentName = transform.parent != null ? transform.parent.name : "No parent";
        //Debug.LogError("P : " + parentName);
        
        Sword1.SetActive(true);
        Sword2.SetActive(true);

        if (parentName == "Player1")
        {
            Destroy(Sword1.gameObject);
        }
        else if(parentName == "Player2")
        {
            Destroy(Sword2.gameObject);
        }
    }
}