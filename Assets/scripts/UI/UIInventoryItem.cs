using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class UIInventoryItem : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler
{
    [SerializeField]
    private Image itemImage; 
    [SerializeField]
    private TMP_Text quantityTxt; 
    [SerializeField]
    private Image borderImage; 

    public event Action<UIInventoryItem> onItemClicked, onItemDroppedOn, onItemBeginDrag, onItemEndDrag, onRightMouseBtnClick; // Correct capitalization and names
    
    private bool empty = true;
    
    private void Awake() // Remove redundant 'void'
    {
        ResetData();
        Deselect();
    }

    public void ResetData() 
    {
        this.itemImage.gameObject.SetActive(false); 
        empty = true;
    }

    public void Deselect() // Correct capitalization
    {
        borderImage.enabled = false;
    }

    public void SetData(Sprite sprite, int quantity) // Correct capitalization
    {
        this.itemImage.gameObject.SetActive(true); 
        this.itemImage.sprite = sprite; 
        this.quantityTxt.text = quantity.ToString(); // Correct capitalization
        empty = false;
    }

    public void Select() // Correct capitalization
    {
        borderImage.enabled = true;
    }

    public void OnPointerClick(PointerEventData pointerData)
    {
        if (pointerData.button == PointerEventData.InputButton.Right) 
        {
            onRightMouseBtnClick?.Invoke(this); 
        }
        else
        {
            onItemClicked?.Invoke(this); 
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        onItemEndDrag?.Invoke(this);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (empty)
            return;
        onItemBeginDrag?.Invoke(this); 
    }

    public void OnDrop(PointerEventData eventData)
    {
        onItemDroppedOn?.Invoke(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Your code here
    }
}
