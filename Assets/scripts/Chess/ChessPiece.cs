using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPiece : MonoBehaviour
{
    public bool isKing; // Menandakan apakah pion ini adalah Raja
    public ChessBoard chessBoard;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        chessBoard = FindObjectOfType<ChessBoard>();

        // Atur posisi awal
        if (isKing)
        {
            transform.position = new Vector3(0 * chessBoard.tileSize, 3 * chessBoard.tileSize, 0); // Posisi A4
        }
    }

    void OnMouseDown()
    {
        Debug.Log("Nira has been clicked!"); // Log untuk memastikan Nira diklik
        if (isKing)
        {
            Debug.Log("Nira is a king, showing valid moves.");
            chessBoard.ClearMoveIndicators(); // Bersihkan indikator gerakan sebelum menampilkan yang baru
            ShowValidMovesForKing();
        }
        // Tambahkan logika untuk jenis pion lainnya
    }

    void ShowValidMovesForKing()
    {
        Vector2Int currentPos = chessBoard.WorldToBoardPosition(transform.position);
        Debug.Log($"Nira's current position: {currentPos}");
        List<Vector2Int> validMoves = new List<Vector2Int>();

        // Tambahkan semua gerakan Raja
        validMoves.Add(currentPos + Vector2Int.up);
        validMoves.Add(currentPos + Vector2Int.down);
        validMoves.Add(currentPos + Vector2Int.left);
        validMoves.Add(currentPos + Vector2Int.right);
        validMoves.Add(currentPos + new Vector2Int(1, 1));
        validMoves.Add(currentPos + new Vector2Int(1, -1));
        validMoves.Add(currentPos + new Vector2Int(-1, 1));
        validMoves.Add(currentPos + new Vector2Int(-1, -1));

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
