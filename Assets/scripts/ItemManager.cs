using UnityEngine;

[System.Serializable]
public class Item
{
    public string itemName;
    public string description;
    public int quantity;

    public Item(string name, string desc, int qty = 1)
    {
        itemName = name;
        description = desc;
        quantity = qty;
    }
}
