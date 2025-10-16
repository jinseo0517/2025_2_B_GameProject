using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public ItemData item;       //이 슬롯에 있는 아이템
    public int amount;      //아이템 개수


    [Header("UI Refernece")]
    public Image itemlcon;      //아이템 아이콘 이미지
    public Text amountText;     //개수 텍스트
    public GameObject emptySlotlmage;       //빈 슬롯 일때 보여줄 이미지


    // Start is called before the first frame update
    void Start()
    {
        UpdateSlotUI();
    }

    public void SetItem(ItemData newItem, int newAmount)        //슬롯에 아이템 설정하는 함수
    {
        item = newItem;
        amount = newAmount;
        UpdateSlotUI();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //UI를 업데이트하는 함수
    private void UpdateSlotUI()
    {
        if (item != null)
        {
            itemlcon.sprite = item.itemlcon;
            itemlcon.enabled = true;

            amountText.text = amount > 1 ? amount.ToString() : "";
            if (emptySlotlmage != null)
            {
                emptySlotlmage.SetActive(false);
            }
        }
        else
        {
            itemlcon.enabled = false;
            amountText.text = "";

            if (emptySlotlmage != null)
            {
                emptySlotlmage.SetActive(true);
            }
        }
    }

    public void AddAmount(int value)
    {
        amount += value;
        UpdateSlotUI();
    }
    public void RemoveAmount(int value)
    {
        amount -= value; 

        if(amount <= 0)
        {
            ClearSlot();
        }
        else
        {
            UpdateSlotUI();
        }
    }
    public void ClearSlot()
    {
        item = null;
        amount = 0;
        UpdateSlotUI();
    }

}
