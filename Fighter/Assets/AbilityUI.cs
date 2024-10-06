using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{
    public Image icon;
    public Image frame;
    public TextMeshProUGUI lvT;

    public void UpdateUi(Sprite icon , Sprite frame , int lv)
    {
        this.icon.sprite = icon;
        this.frame.sprite = frame;
        lvT.text = lv.ToString();
    }
}
