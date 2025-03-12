using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIInventoryDescription : MonoBehaviour
{
    [SerializeField]
    private Image itemImage; // Correct capitalization
    [SerializeField]
    private TMP_Text title; // Correct capitalization
    [SerializeField]
    private TMP_Text description; // Correct capitalization

    private void Awake() // Remove redundant 'void'
    {
        ResetDescription(); // Correct capitalization
    }

    public void ResetDescription() // Correct capitalization
    {
        this.itemImage.gameObject.SetActive(false);
        this.title.text = "";
        this.description.text = ""; // Now this should work as it is TMP_Text
    }

    public void SetDescription(Sprite sprite, string itemName, string itemDescription) // Correct capitalization
    {
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = sprite;
        this.title.text = itemName;
        this.description.text = itemDescription; // Now this should work as it is TMP_Text
    }
}
