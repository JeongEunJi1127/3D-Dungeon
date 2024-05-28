using UnityEngine;

public enum ItemType
{
    Equipable,
    Useable,
    Resource
}

[CreateAssetMenu(fileName ="ItemData", menuName = "Item")]
public class ItemData : ScriptableObject
{
    [Header("ItemData")]
    public string itemName;
    public string itemDescription;
    public ItemType itemType;
}
