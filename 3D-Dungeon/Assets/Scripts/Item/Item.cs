using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData itemData;

    public string ItemDataToString()
    {
        string str = $"{itemData.name}\n{itemData.itemDescription}";
        return str;
    }

    public void OnInteract()
    {
        CharacterManager.Instance.Player.itemData = itemData;
        CharacterManager.Instance.Player.addItem?.Invoke();
        Destroy(gameObject);
    }
}
