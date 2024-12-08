using System.Collections;
using System.Collections.Generic;
using ControlFreak2;
using ControlFreak2.Internal;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{
    public Image frameCost;
    public Image darkIcon;
    public Image icon;
    public Image frame;
    public TextMeshProUGUI lvT;
    public TouchButtonSpriteAnimator btn;

    public void UpdateUi(Sprite icon , TypeSkill type , int lv)
    {
        darkIcon.sprite = icon;
        btn.SetSprite(icon);
        this.icon.sprite = icon;
        lvT.text = lv.ToString();
        if (type == TypeSkill.Block)
        {
            frame.color = Color.blue;
            frameCost.color = Color.blue;
        }
        else if(type == TypeSkill.Break)
        {
            frame.color = Color.red;
            frameCost.color = Color.red;
        }
        else
        {
            frame.color = Color.yellow;
            frameCost.color = Color.yellow;
        }
    }

    public void UpdateFill(float amount)
    {
        icon.fillAmount = amount;
    }
}
