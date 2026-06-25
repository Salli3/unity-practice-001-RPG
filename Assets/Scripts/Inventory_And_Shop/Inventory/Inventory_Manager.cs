using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory_Manager : MonoBehaviour
{
    public static Inventory_Manager instance;

    public int gold;
    public TMP_Text goldText;
    //public Inventory_Slot[] itemSlots;
    public Use_Item useItem;
    public GameObject lootPrefab;
    public Transform player;

    public GameObject slotPrefab;
    public Transform slotContainer;

    public List<Inventory_Slot> itemSlots = new List<Inventory_Slot>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //foreach (Inventory_Slot slot in itemSlots)
        //{
        //    slot.UpdateUI();
        //}

        //destroy pre-placed editor slots
        foreach (Inventory_Slot slot in slotContainer.GetComponentsInChildren<Inventory_Slot>())
        {
            Destroy(slot.gameObject);
        }
        itemSlots.Clear();
    }

    private void OnEnable()
    {
        Loot.OnItemLooted += AddItem;
    }

    private void OnDisable()
    {
        Loot.OnItemLooted -= AddItem;
    }

    public void AddItem(Item_SO itemSO, int quantity)
    {
        if (itemSO.isGold)
        {
            gold += quantity;
            goldText.text = gold.ToString("D2");
            return;
        }

        foreach (var slot in itemSlots)
        {
            //check if there is another same item in inventory
            if (slot.itemSO == itemSO) //&& itemSO.stackSize -> scrap this idea
            {
                //int availableSpace = itemSO.stackSize - slot.quantity;
                //int amountToAdd = Mathf.Min(availableSpace, quantity);

                //slot.quantity += amountToAdd;
                //quantity -= amountToAdd;

                //slot.UpdateUI();
                //if (quantity <= 0)
                //{
                //    return;
                //}

                slot.quantity += quantity;
                slot.UpdateUI();
                return;
            }
        }

        //foreach (var slot in itemSlots)
        //{
        //    //look for empty slots
        //    if (slot.itemSO == null)
        //    {
        //        //int amountToAdd = Mathf.Min(itemSO.stackSize - quantity);
        //        slot.itemSO = itemSO;
        //        slot.quantity = quantity;
        //        slot.UpdateUI();
        //        return;
        //    }
        //}

        //if (quantity > 0)
        //{
        //    DropLoot(itemSO, quantity);
        //}

        AddNewSlot(itemSO, quantity);
    }

    private void AddNewSlot(Item_SO itemSO, int quantity)
    {
        Inventory_Slot newSlot = Instantiate(slotPrefab, slotContainer).GetComponent<Inventory_Slot>();

        newSlot.Initialize(this);
        newSlot.itemSO = itemSO;
        newSlot.quantity = quantity;
        newSlot.UpdateUI();

        itemSlots.Add(newSlot);
    }

    public void DropItem(Inventory_Slot slot)
    {
        Loot loot = Instantiate(lootPrefab, player.position, Quaternion.identity).GetComponent<Loot>();
        loot.Initialize(slot.itemSO, slot.quantity);
        slot.itemSO = null;
        slot.UpdateUI();
    }

    //private void DropItem(Inventory_Slot slot)
    //{
    //    DropLoot(slot.itemSO, slot.quantity);
    //    slot.itemSO = null;
    //    slot.UpdateUI();
    //}

    //private void DropLoot(Item_SO itemSO, int quantity)
    //{
    //    Loot loot = Instantiate(lootPrefab, player.position, Quaternion.identity).GetComponent<Loot>();
    //    loot.Initialize(itemSO, quantity);
    //}

    public void UseItem(Inventory_Slot slot)
    {
        if (slot.itemSO != null && slot.quantity > 0)
        {
            Debug.Log("Using item: " + slot.itemSO.itemName);
            useItem.ApplyItemEffects(slot.itemSO);
            slot.quantity--;
            //if (slot.quantity <= 0)
            //{
            //    slot.itemSO = null;
            //}
            slot.UpdateUI();
        }
    }

    public bool HasItem(Item_SO itemSO)
    {
        foreach (var slot in itemSlots)
        {
            if (slot.itemSO == itemSO && slot.quantity > 0)
            {
                return true;
            }
        }
        return false;
    }

    public int GetItemQuantity(Item_SO itemSO)
    {
        int total = 0;
        foreach (var slot in itemSlots)
        {
            if (slot.itemSO == itemSO)
            {
                total += slot.quantity;
            }
        }
        return total;
    }
}
