using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [Header("Inventroy Setting")]
    public int inventorySize = 20;
    public GameObject inventroyUl;
    public Transform itemSlotParent;
    public GameObject itemSletPrefab;

    [Header("Input")]
    public KeyCode inventoryKey = KeyCode.I;
    public List<InventorySlot> slots = new List<InventorySlot>();
    private bool islnventoryOpen = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        CreatelnventorySlots();
        inventroyUl.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(inventoryKey))
        {
            Togglelnventory();
        }
    }

    void CreatelnventorySlots()         //인벤토리 슬롯들을 생성하는 함수
    {
        for (int i = 0; i < inventorySize; i++)
        {
            GameObject slotObject = Instantiate(itemSletPrefab, itemSlotParent);
            InventorySlot slot = slotObject.GetComponent<InventorySlot>();
            slots.Add(slot);            // 리스트에 주가
        }
    }
    public void Togglelnventory()           //인벤토리 비를 열거나 닫는 함수
    {
        islnventoryOpen = !islnventoryOpen;
        inventroyUl.SetActive(islnventoryOpen);

        if (islnventoryOpen)
        {
            Cursor.lockState = CursorLockMode.None;     //인벤토리가 열리면 커서 보이기
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;       //인벤토리가 닫히면 커서 숨기기
            Cursor.visible = false;
        }
    }

    //아이템을 인벤토리에 추가 함수
    public bool AddItem(ItemData item, int amount = 1)
    {
        foreach (InventorySlot slot in slots)       // 1단계 : 이미 있는 아이템에 추가 시도 (스택)
        {
            if (slot.item == item && slot.amount < item.maxStack)       //같은 아이템이고 최대 스택 보다 작으면
            {
                int spaceLeft = item.maxStack - slot.amount;        //남은 공간 계산
                int amountToAdd = Mathf.Min(amount, spaceLeft);     //추가할 개수
                slot.AddAmount(amountToAdd);

                amount -= amountToAdd;          //기존 추가한 개수에 남은 개수를 구한다.

                if (amount <= 0)
                {
                    return true;
                }
            }
        }
        foreach (InventorySlot slot in slots)       // 2단계 : 빈 슬롯에 추가
        {
            if (slot.item == null)           // 빈 슬롯 찾기
            {
                slot.SetItem(item, amount);
                return true;
            }
        }
        Debug.Log("인벤토리가 가득 참");
        return false;
    }

    public void Removeltem(ItemData item, int amount = 1)       //아이템을 인벤토리에서 제거 하는 함수
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.item == item)
            {
                slot.RemoveAmount(amount);
                return;
            }
        }
    }

    public int GetltemCount(ItemData item)              //특정 아이팀의 총 개수를 반환하는 함수
    {
        int count = 0;
        foreach (InventorySlot slot in slots)
        {
            if (slot.item = item)
            {
                count += slot.amount;
            }
        }
        return count;
    }

}
