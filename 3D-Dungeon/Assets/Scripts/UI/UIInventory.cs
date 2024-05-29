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

        // 쌓을 수 있는 아이템이고, 인벤토리에 존재한다면 쌓기
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
        // 아이템 넣을 칸 찾기
        ItemSlot newSlot = GetEmptySlot();

        if (newSlot != null)
        {
            newSlot.itemData = itemData;    
            newSlot.quantity = 1;
            UpdateUI();
            CharacterManager.Instance.Player.itemData = null;
            return;
        }
        // 인벤토리 꽉 찼으므로 아이템 버리기
        DropItem();
        CharacterManager.Instance.Player.itemData = null;
    }

    void DropItem()
    {
        Instantiate(itemData.dropPrefab, dropPos);
    }
    // 현재 인벤토리에 같은 형식의 데이터가 있고, 최대 stack amount를 넘지 않았다면 그 슬롯에 아이템을 넣어야하므로
    // 이 함수를 이용해 그 slot을 반환. 아니면 null 반환
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

    // 비어있는 슬롯 찾아서 반환, 없으면 null 반환
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
