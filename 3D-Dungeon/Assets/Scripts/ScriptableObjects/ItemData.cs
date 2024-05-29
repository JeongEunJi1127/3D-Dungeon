using UnityEngine;

public enum ItemType
{
    Equipable,
    Useable,
    Resource
}

public enum ConsumableType
{
    Hunger,
    Health
}

[System.Serializable]
public class ItemDataConsumable
{
    public ConsumableType type;
    public float value;
}

[CreateAssetMenu(fileName ="Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string itemName;
    public string itemDescription;
    public ItemType itemType;
    public Sprite icon;
    public GameObject dropPrefab;

    [Header("Stack")]
    public bool canStack;
    public int maxStackAmount;

    [Header("Equip")]
    public GameObject equipPrefab;

    [Header("Use")]
    public ItemDataConsumable[] itemDataConsumables;

}
