using System.Collections.Generic;
using ControlFreak2;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "ScriptableObjects/ItemData", order = 1)]

public class DataSkill : ScriptableObject
{
    public string Name;
    public List<DataAbility> dataAbilities;
}