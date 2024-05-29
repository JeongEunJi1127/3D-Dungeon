using TMPro;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public ItemSlot[] slots;
    public Transform dropPos;

    [Header("Selected Item")]
    private ItemData itemData;
    private int selectedItemIndex;
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;
    public TextMeshProUGUI selectedItemStatName;
    public TextMeshProUGUI selectedItemStatValue;
    public GameObject useButton;
    public GameObject equipButton;
    public GameObject unEquipButton;
    public GameObject dropButton;

    private void Awake()
    {
        CharacterManager.Instance.Player.addItem += AddItem;
    }

    void AddItem()
    {
        itemData = CharacterManager.Instance.Player.itemData;

        // ���� �� �ִ� �������̰�, �κ��丮�� �����Ѵٸ� �ױ�
        if (itemData.canStack)
        {
            ItemSlot slot = GetItemSlot();
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
            newSlot.itemData = itemData;    
            newSlot.quantity = 1;
            UpdateUI();
            CharacterManager.Instance.Player.itemData = null;
            return;
        }
        // �κ��丮 �� á���Ƿ� ������ ������
        DropItem();
        CharacterManager.Instance.Player.itemData = null;
    }

    void DropItem()
    {
        Instantiate(itemData.dropPrefab, dropPos);
    }
    // ���� �κ��丮�� ���� ������ �����Ͱ� �ְ�, �ִ� stack amount�� ���� �ʾҴٸ� �� ���Կ� �������� �־���ϹǷ�
    // �� �Լ��� �̿��� �� slot�� ��ȯ. �ƴϸ� null ��ȯ
    ItemSlot GetItemSlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].itemData == itemData && slots[i].quantity < itemData.maxStackAmount)
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
            if (slots[i].itemData != null)
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
}
