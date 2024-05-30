using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemData itemData;
    public UIInventory inventory;

    public Image icon;
    public int index;
    public bool equipped;
    public TextMeshProUGUI quantityText;
    public int quantity;    

    public void Set()
    {
        icon.sprite = itemData.icon;
        quantityText.text = quantity > 1? quantity.ToString() : string.Empty;
    }

    public void Clear()
    {
        icon.sprite = null;
        quantityText.text = string.Empty;
    }

    public void OnClickSlot()
    {
        inventory.SelectItem(index);
    }
}
