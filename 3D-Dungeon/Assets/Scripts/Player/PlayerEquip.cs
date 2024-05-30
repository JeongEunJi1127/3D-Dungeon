using Unity.VisualScripting;
using UnityEngine;

public class PlayerEquip : MonoBehaviour
{
    public GameObject curEquipItem;
    public Transform equipPosition;

    public void EquipItem(ItemData data)
    {
        UnEquipItem();
        curEquipItem = Instantiate(data.equipPrefab, equipPosition);
    }

     public void UnEquipItem()
    {
        if (curEquipItem != null)
        {
            Destroy(curEquipItem);
            curEquipItem = null;
        }
    }
}
