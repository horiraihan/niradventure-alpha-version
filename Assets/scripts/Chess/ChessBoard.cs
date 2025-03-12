using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoard : MonoBehaviour
{
    public GameObject tilePrefab;
    public GameObject moveIndicatorPrefab; // Prefab untuk menampilkan indikator gerakan
    public GameObject coordinateLabelPrefab; // Prefab untuk label koordinat
    public Sprite grassSprite;
    public Sprite stoneSprite;
    public int rows = 8;
    public int columns = 8;
    public float tileSize = 1.0f; // Pastikan nilai ini sesuai dengan ukuran tile Anda

    void Start()
    {
        GenerateBoard();
    }

    void GenerateBoard()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                GameObject tile = Instantiate(tilePrefab, transform);
                float posX = column * tileSize;
                float posY = row * tileSize;
                tile.transform.localPosition = new Vector3(posX, posY, 0);

                // Alternating tile sprites
                SpriteRenderer renderer = tile.GetComponent<SpriteRenderer>();
                if ((row + column) % 2 == 0)
                {
                    renderer.sprite = grassSprite;
                }
                else
                {
                    renderer.sprite = stoneSprite;
                }

                // Tambahkan label koordinat
                GameObject label = Instantiate(coordinateLabelPrefab, tile.transform);
                label.transform.localPosition = new Vector3(0.5f, 0.5f, 0); // Posisi di tengah tile
                TextMesh textMesh = label.GetComponent<TextMesh>();
                if (textMesh != null)
                {
                    textMesh.text = $"{(char)(column + 'A')}{(1 + row)}";
                }
                else
                {
                    Debug.LogError("TextMesh component missing on CoordinateLabel prefab.");
                }
            }
        }

        // Adjust the position of the board
        float boardWidth = columns * tileSize;
        float boardHeight = rows * tileSize;
        transform.position = new Vector3(-boardWidth / 2 + tileSize / 2, -boardHeight / 2 + tileSize / 2, 0);
    }

    public Vector2Int WorldToBoardPosition(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt(worldPosition.x / tileSize);
        int y = Mathf.FloorToInt(worldPosition.y / tileSize);
        return new Vector2Int(x, y);
    }

    public bool IsWithinBounds(Vector2Int position)
    {
        return position.x >= 0 && position.x < columns && position.y >= 0 && position.y < rows;
    }

    public void ShowMoveIndicator(Vector2Int boardPosition, string color)
    {
        GameObject indicator = Instantiate(moveIndicatorPrefab, transform);
        indicator.transform.localPosition = new Vector3(boardPosition.x * tileSize, boardPosition.y * tileSize, 0);
        Debug.Log($"MoveIndicator instantiated at {boardPosition.x}, {boardPosition.y}");

        SpriteRenderer renderer = indicator.GetComponent<SpriteRenderer>();
        if (color == "green")
        {
            renderer.color = new Color(0, 1, 0, 0.5f); // Hijau transparan
            Debug.Log("MoveIndicator color set to green");
        }
        else if (color == "red")
        {
            renderer.color = new Color(1, 0, 0, 0.5f); // Merah transparan
            Debug.Log("MoveIndicator color set to red");
        }
    }

    public void ClearMoveIndicators()
    {
        // Logika untuk membersihkan indikator gerakan
    }
}
