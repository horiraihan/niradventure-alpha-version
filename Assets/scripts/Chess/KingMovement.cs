using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingMovement : MonoBehaviour
{
    private Vector2Int boardPosition; // Posisi di papan catur
    private ChessBoard chessBoard;

    void Start()
    {
        chessBoard = FindObjectOfType<ChessBoard>();
        // Inisialisasi posisi awal di papan catur (A4)
        boardPosition = new Vector2Int(0, 3); // Posisi A4 (A adalah kolom ke-0 dan 4 adalah baris ke-3)
        UpdateWorldPosition();
    }

    void UpdateWorldPosition()
    {
        Vector3 worldPosition = new Vector3(boardPosition.x * chessBoard.tileSize, boardPosition.y * chessBoard.tileSize, 0);
        transform.position = worldPosition;
        Debug.Log($"Nira's initial position set to: {boardPosition} ({worldPosition})");
    }
}
