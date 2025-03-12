using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private UIInventoryPage inventoryUI; // Correct capitalization
    public int inventorySize = 10;

    private void Start()
    {
        inventoryUI.InitializeInventoryUI(inventorySize); // Correct capitalization
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) // Correct capitalization
        {
            if (inventoryUI.isActiveAndEnabled == false) // Correct capitalization
            {
                inventoryUI.Show(); // Correct capitalization
            }
            else
            {
                inventoryUI.Hide(); // Correct capitalization and method call
            }
        }
    }
}