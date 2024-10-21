using System.Collections;
using System.Collections.Generic;
using ControlFreak2;
using ControlFreak2.Internal;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{
    public Image icon;
    public Image frame;
    public TextMeshProUGUI lvT;
    public TouchButtonSpriteAnimator btn;

    public void UpdateUi(Sprite icon , Sprite frame , int lv)
    {
        btn.SetSprite(icon);
        this.icon.sprite = icon;
        this.frame.sprite = frame;
        lvT.text = lv.ToString();
    }
}
