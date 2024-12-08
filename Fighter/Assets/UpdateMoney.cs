using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateMoney : MonoBehaviour
{
    public int addMoney;
    public Text text;
    void Start()
    {
        GoldManager.Instance.AddGold(addMoney);
        text.text = " + " + addMoney;
    }
  
}
