using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private UIInventoryItem item;

    private void Awake()
    {
        canvas = transform.root.GetComponent<Canvas>(); // Kapitalisasi benar
        item = GetComponentInChildren<UIInventoryItem>(); // Kapitalisasi benar
    }

    public void SetData(Sprite sprite, int quantity)
    {
        item.SetData(sprite, quantity);
    }

    private void Update()
    {
        Vector2 position; // Kapitalisasi benar
        RectTransformUtility.ScreenPointToLocalPointInRectangle( // Kapitalisasi benar
            (RectTransform)canvas.transform, // Kapitalisasi benar
            Input.mousePosition, // Kapitalisasi benar dan typo diperbaiki
            canvas.worldCamera, // Kapitalisasi benar
            out position
        );
        transform.position = canvas.transform.TransformPoint(position);
    }

    public void Toggle(bool val)
    {
        Debug.Log($"item toggled {val}");
        gameObject.SetActive(val);
    }
}
