using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIInventoryPage : MonoBehaviour
{
    [SerializeField]
    private UIInventoryItem itemPrefab; // Correct capitalization
    [SerializeField]
    private RectTransform contentPanel;
    [SerializeField]
    private UIInventoryDescription itemDescription;
    [SerializeField]
    private MouseFollower mouseFollower; // Correct capitalization

    private List<UIInventoryItem> listOfUIItems = new List<UIInventoryItem>(); // Correct capitalization
    private int currentlyDraggedItemIndex = -1;

    public event Action<int> OnDescriptionRequest, OnItemActionRequested, OnStartDragging; // Correct capitalization
    public event Action<int, int> OnSwapItems; // Correct capitalization

    private void Awake()
    {
        Hide();
        mouseFollower.Toggle(false);
        itemDescription.ResetDescription(); // Fix typo
    }

    public void InitializeInventoryUI(int inventorySize)
    {
        for (int i = 0; i < inventorySize; i++) // Fix typo (removed extra space in inventorySize)
        {
            UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity); // Correct capitalization
            uiItem.transform.SetParent(contentPanel); // Correct capitalization
            listOfUIItems.Add(uiItem); // Correct capitalization

            uiItem.onItemClicked += HandleItemSelection; // Correct capitalization
            uiItem.onItemBeginDrag += HandleBeginDrag;   // Correct capitalization
            uiItem.onItemDroppedOn += HandleSwap;        // Correct capitalization
            uiItem.onItemEndDrag += HandleEndDrag;       // Correct capitalization
            uiItem.onRightMouseBtnClick += HandleShowItemActions; // Correct capitalization
        }
    }

    public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity) // Fix typos
    {
        if (listOfUIItems.Count > itemIndex) // Correct capitalization
        {
            listOfUIItems[itemIndex].SetData(itemImage, itemQuantity); // Correct capitalization
        }
    }

    private void HandleItemSelection(UIInventoryItem inventoryItemUI)
    {
        int index = listOfUIItems.IndexOf(inventoryItemUI); // Correct capitalization
        if (index == -1)
            return;

        OnDescriptionRequest?.Invoke(index); // Correct capitalization
    }

    private void HandleBeginDrag(UIInventoryItem inventoryItemUI)
    {
        int index = listOfUIItems.IndexOf(inventoryItemUI); // Correct capitalization
        if (index == -1)
            return;

        currentlyDraggedItemIndex = index;
        HandleItemSelection(inventoryItemUI);
        OnStartDragging?.Invoke(index); // Correct capitalization
    }

    public void CreateDraggedItem(Sprite sprite, int quantity) // Correct capitalization
    {
        mouseFollower.Toggle(true);
        mouseFollower.SetData(sprite, quantity); // Correct typo
    }

    private void ResetDraggedItem()
    {
        mouseFollower.Toggle(false);
        currentlyDraggedItemIndex = -1;
    }

    private void HandleSwap(UIInventoryItem inventoryItemUI)
    {
        int index = listOfUIItems.IndexOf(inventoryItemUI); // Correct capitalization
        if (index == -1)
        {
            return;
        }
        OnSwapItems?.Invoke(currentlyDraggedItemIndex, index); // Correct capitalization
    }

    private void HandleEndDrag(UIInventoryItem inventoryItemUI)
    {
        ResetDraggedItem();
    }

    private void HandleShowItemActions(UIInventoryItem inventoryItemUI)
    {
        // Your code here
    }

    public void Show()
    {
        gameObject.SetActive(true);
        ResetSelection();
    }

    private void ResetSelection()
    {
        itemDescription.ResetDescription(); // Fix typo
        DeselectAllItems(); // Fix typo
    }

    private void DeselectAllItems() // Fix typo
    {
        foreach (UIInventoryItem item in listOfUIItems)
        {
            item.Deselect();
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        ResetDraggedItem();
    }
}
