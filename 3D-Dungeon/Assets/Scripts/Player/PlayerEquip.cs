using Unity.VisualScripting;
using UnityEngine;

public class PlayerEquip : MonoBehaviour
{
    public GameObject curEquipItem;
    public Transform equipPosition;

    public void EquipItem(ItemData data)
    {
        UnEquipItem();
        SetEquipValue(data);
        curEquipItem = Instantiate(data.equipPrefab, equipPosition);
    }

     public void UnEquipItem()
    {
        if (curEquipItem != null)
        {
            SetUnEquipValue(curEquipItem.GetComponent<Item>().itemData);
            Destroy(curEquipItem);
            curEquipItem = null;
        }
    }

    void SetEquipValue(ItemData data)
    {
        switch (data.equipType)
        {
            case EquipableType.JumpHigh:
                CharacterManager.Instance.Player.controller.jumpHeight += data.equipValue;
                break;
            case EquipableType.Fast:
                CharacterManager.Instance.Player.controller.moveSpeed += data.equipValue;
                break;
        }
    }
    void SetUnEquipValue(ItemData data)
    {
        switch (data.equipType)
        {
            case EquipableType.JumpHigh:
                CharacterManager.Instance.Player.controller.jumpHeight -= data.equipValue;
                break;
            case EquipableType.Fast:
                CharacterManager.Instance.Player.controller.moveSpeed -= data.equipValue;
                break;
        }
    }
}
