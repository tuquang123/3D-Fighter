using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemUi : MonoBehaviour
{
    public string id;
    public int gold;
    public GameObject soldUi;
    public TextMeshProUGUI prince;

    private void Start()
    {
        prince.text = gold.ToString();
        prince.color = GoldManager.Instance.Gold < gold ? Color.red : Color.white;
        
        if (IsItemPurchased(id))
        {
            soldUi.SetActive(true);
        }
    }
    private bool IsItemPurchased(string itemId)
    {
        string purchasedItems = PlayerPrefs.GetString("PurchasedItems", "");
        return purchasedItems.Contains(itemId);
    }

    public void Purchase()
    {
        if (GoldManager.Instance.Gold >= gold)
        {
            GoldManager.Instance.SubtractGold(gold);
            soldUi.SetActive(true);
            
            SavePurchasedItem(id);
        }
    }
    private void SavePurchasedItem(string itemId)
    {
        // Lấy danh sách đã lưu trước đó
        string purchasedItems = PlayerPrefs.GetString("PurchasedItems", "");

        // Kiểm tra nếu item chưa được lưu
        if (!purchasedItems.Contains(itemId))
        {
            if (string.IsNullOrEmpty(purchasedItems))
            {
                purchasedItems = itemId; // Nếu chưa có item nào, lưu trực tiếp
            }
            else
            {
                purchasedItems += $",{itemId}"; // Thêm item mới vào danh sách
            }

            // Lưu lại danh sách vào PlayerPrefs
            PlayerPrefs.SetString("PurchasedItems", purchasedItems);
            PlayerPrefs.Save();
        }
    }

    public void ActiveCharacter(int index)
    {
        UFE.config.storyMode.selectableCharactersInStoryMode.Add(index);
    }
}
