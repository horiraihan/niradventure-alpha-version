using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RookMovement : MonoBehaviour
{
    public ChessBoard chessBoard;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        chessBoard = FindObjectOfType<ChessBoard>();

        // Atur posisi awal di sini, misalnya A1 (0, 0)
        transform.position = new Vector3(0 * chessBoard.tileSize, 0 * chessBoard.tileSize, 0);
        Debug.Log($"Calli's initial position set to: {transform.position}");
    }

    void OnMouseDown()
    {
        Debug.Log("Calli has been clicked!"); // Log untuk memastikan Calli diklik
        ShowValidMovesForRook();
    }

    void ShowValidMovesForRook()
    {
        Vector2Int currentPos = chessBoard.WorldToBoardPosition(transform.position);
        Debug.Log($"Calli's current position: {currentPos}");
        List<Vector2Int> validMoves = new List<Vector2Int>();

        // Menambahkan gerakan vertikal
        for (int row = 0; row < chessBoard.rows; row++)
        {
            if (row != currentPos.y)
            {
                validMoves.Add(new Vector2Int(currentPos.x, row));
            }
        }

        // Menambahkan gerakan horizontal
        for (int col = 0; col < chessBoard.columns; col++)
        {
            if (col != currentPos.x)
            {
                validMoves.Add(new Vector2Int(col, currentPos.y));
            }
        }

        // Tampilkan kotak untuk setiap gerakan valid
        foreach (Vector2Int move in validMoves)
        {
            Debug.Log($"Checking move: {move}");
            if (chessBoard.IsWithinBounds(move))
            {
                Debug.Log($"Move {move} is within bounds, showing indicator.");
                chessBoard.ShowMoveIndicator(move, "green");
            }
            else
            {
                Debug.Log($"Move {move} is out of bounds.");
            }
        }
    }
}
