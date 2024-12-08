using System;
using UnityEngine;
using TMPro;

public class ShowGoldUi : MonoBehaviour
{
    public TextMeshProUGUI gold ;

    private void Start()
    {
        UpdateUi();
    }

    private void UpdateUi()
    {
        gold.text = ": " + GoldManager.Instance.Gold;
    }
    private void OnEnable()
    {
        GoldManager.Instance.OnGoldChanged += UpdateUi;
    }

    private void OnDisable()
    {
        GoldManager.Instance.OnGoldChanged -= UpdateUi;
    }
}
