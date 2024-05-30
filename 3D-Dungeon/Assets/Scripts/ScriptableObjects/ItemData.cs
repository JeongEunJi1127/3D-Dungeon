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
    Health,
    Fast,
    JumpHigh
}

public enum EquipableType
{
    Fast,
    JumpHigh
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
    // �������� ���� �� �ִ��� -> ���� ������ ���� ���� �� X
    public bool canStack;
    public int maxStackAmount;

    [Header("Equip")]
    public GameObject equipPrefab;
    public EquipableType equipType;
    public int equipValue;

    [Header("Use")]
    public ItemDataConsumable[] itemDataConsumables;

}
