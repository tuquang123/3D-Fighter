using System;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public static GoldManager Instance { get; private set; }
    
    private const string GoldKey = "PlayerGold";
    
    private int _gold;
    public int Gold
    {
        get => _gold;
        private set
        {
            _gold = value;
            PlayerPrefs.SetInt(GoldKey, _gold); 
            PlayerPrefs.Save();
        }
    }

    private void Awake()
    {
        //AddGold(1000);
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        
        _gold = PlayerPrefs.GetInt(GoldKey, 0); 
    }
    
    public void AddGold(int amount)
    {
        if (amount > 0)
        {
            Gold += amount;
            Debug.Log($"Vàng hiện tại: {Gold}");
            OnGoldChanged?.Invoke();
        }
    }
    
    public void SubtractGold(int amount)
    {
        if (amount > 0 && _gold >= amount)
        {
            Gold -= amount;
            Debug.Log($"Vàng hiện tại: {Gold}");
            OnGoldChanged?.Invoke();
        }
        else
        {
            Debug.LogWarning("Không đủ vàng!");
        }
    }
    
    public void ResetGold()
    {
        Gold = 0;
        Debug.Log("Vàng đã được reset về 0.");
    }

    public event Action OnGoldChanged;
}