using TMPro;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public ItemSlot[] slots;
    public Transform dropPos;

    [Header("Selected Item")]
    private ItemSlot itemData;
    private int itemIndex;
    private int curEquippedItemIndex;
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;
    public TextMeshProUGUI selectedItemStatName;
    public TextMeshProUGUI selectedItemStatValue;
    public GameObject useButton;
    public GameObject equipButton;
    public GameObject unEquipButton;
    public GameObject dropButton;

    private void Start()
    {
        CharacterManager.Instance.Player.addItem += AddItem;
        CharacterManager.Instance.Player.controller.Inventory.SetActive(false);

        SetSlots();
        ClearExplanation();
    }

    void SetSlots()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].index = i;
            slots[i].inventory = this;
        }
    }

    void AddItem()
    {
        ItemData data = CharacterManager.Instance.Player.itemData;

        // ���� �� �ִ� �������̰�, �κ��丮�� �����Ѵٸ� �ױ�
        if (data.canStack)
        {
            ItemSlot slot = GetItemSlot(data);
            if(slot != null )
            {
                slot.quantity++;
                UpdateUI();
                CharacterManager.Instance.Player.itemData = null;
                return;
            }
        }
        // ������ ���� ĭ ã��
        ItemSlot newSlot = GetEmptySlot();

        if (newSlot != null)
        {
            newSlot.itemData = data;    
            newSlot.quantity = 1;
            UpdateUI();
            CharacterManager.Instance.Player.itemData = null;
            return;
        }
        // �κ��丮 �� á���Ƿ� ������ ������
        DropItem(data);
        CharacterManager.Instance.Player.itemData = null;
    }

    void DropItem(ItemData data)
    {
        Instantiate(data.dropPrefab, dropPos.position,Quaternion.identity);
    }
    // ���� �κ��丮�� ���� ������ �����Ͱ� �ְ�, �ִ� stack amount�� ���� �ʾҴٸ� �� ���Կ� �������� �־���ϹǷ�
    // �� �Լ��� �̿��� �� slot�� ��ȯ. �ƴϸ� null ��ȯ
    ItemSlot GetItemSlot(ItemData data)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].itemData == data && slots[i].quantity < data.maxStackAmount)
            {
                return slots[i];
            }
        }
        return null;
    }

    // ����ִ� ���� ã�Ƽ� ��ȯ, ������ null ��ȯ
    ItemSlot GetEmptySlot()
    {
        for(int i = 0;i < slots.Length ; i++)
        {
            if (slots[i].itemData == null)
            {
                return slots[i];
            }
        }
        return null;
    }

    void UpdateUI()
    {
        for(int i=0; i < slots.Length; i++)
        {
            if (slots[i].itemData != null)
            {
                slots[i].Set();
            }
            else
            {
                slots[i].Clear();
            }
        }
    }

    void ClearExplanation()
    {
        itemData = null;

        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;
        selectedItemStatName.text = string.Empty;
        selectedItemStatValue.text = string.Empty;

        useButton.SetActive(false);
        equipButton.SetActive(false);
        unEquipButton.SetActive(false);
        dropButton.SetActive(false);
    }

    public void SelectItem(int index)
    {
        if (slots[index].itemData == null) return;
        itemData = slots[index];
        itemIndex = index;

        selectedItemName.text = itemData.itemData.itemName;
        selectedItemDescription.text = itemData.itemData.itemDescription;
        selectedItemStatName.text = string.Empty;
        selectedItemStatValue.text = string.Empty;

        for (int i = 0; i<itemData.itemData.itemDataConsumables.Length; i++)
        {
            selectedItemStatName.text += itemData.itemData.itemDataConsumables[i].type.ToString() + "\n";
            selectedItemStatValue.text += itemData.itemData.itemDataConsumables[i].value.ToString() + "\n";
        }
        useButton.SetActive(itemData.itemData.itemType == ItemType.Useable);
        equipButton.SetActive(itemData.itemData.itemType == ItemType.Equipable && !slots[index].equipped);
        unEquipButton.SetActive(itemData.itemData.itemType == ItemType.Equipable && slots[index].equipped);
        dropButton.SetActive(true);
    }

    public void OnUseButton()
    {
        for(int i = 0; i < itemData.itemData.itemDataConsumables.Length; i++)
        {
            switch(itemData.itemData.itemDataConsumables[i].type)
            {
                case ConsumableType.Health:
                    CharacterManager.Instance.Player.condition.Heal(itemData.itemData.itemDataConsumables[i].value);
                    break;
                case ConsumableType.Hunger:
                    CharacterManager.Instance.Player.condition.Eat(itemData.itemData.itemDataConsumables[i].value);
                    break;
                case ConsumableType.Fast:
                    break;
            }            
        }
        RemoveItemFromSlot();
    }

    public void OnDropButton()
    {
        DropItem(itemData.itemData);
        RemoveItemFromSlot();
    }

    public void OnEquipButton()
    {
        if (slots[curEquippedItemIndex].equipped)
        {
            UnEquip(curEquippedItemIndex);
        }
        slots[itemIndex].equipped = true;
        curEquippedItemIndex = itemIndex;
        CharacterManager.Instance.Player.equip.EquipItem(slots[itemIndex].itemData);
        UpdateUI();
        SelectItem(itemIndex);
    }

    public void OnUnEquipButton()
    {
        UnEquip(itemIndex);
    }

    void UnEquip(int index)
    {
        slots[index].equipped = false;
        CharacterManager.Instance.Player.equip.UnEquipItem();
        UpdateUI();

        if(itemIndex == index)
        {
            SelectItem(itemIndex);
        }
    }

    void RemoveItemFromSlot()
    {
        itemData.quantity--;
        if(itemData.quantity <= 0 )
        {
            if (slots[itemIndex].equipped)
            {
                UnEquip(itemIndex);
            }
            itemData.itemData = null;
            ClearExplanation();
        }
        UpdateUI();
    }
}
