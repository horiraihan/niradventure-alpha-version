using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessAI : MonoBehaviour
{
    private Game gameController;
    private ChessGameManager gameManager;
    
    private Vector2Int lastMoveStart = new Vector2Int(-1, -1);
    private Vector2Int lastMoveEnd = new Vector2Int(-1, -1);
    private const int maxIterations = 1000;
    private Dictionary<Vector2Int, bool> positionThreatCache = new Dictionary<Vector2Int, bool>();
    private List<Vector2Int> previousMoves = new List<Vector2Int>(); // Tambahkan variabel riwayat langkah

    void Start()
    {
        gameController = FindObjectOfType<Game>();
        if (gameController == null)
        {
            Debug.LogError("Game controller not found!");
            return;
        }

        gameManager = FindObjectOfType<ChessGameManager>();
        if (gameManager == null)
        {
            Debug.LogError("ChessGameManager not found!");
            return;
        }
    }

    public IEnumerator MakeEnemyMove()
    {
        Debug.Log("MakeEnemyMove started.");
        yield return new WaitForSeconds(1f);

        if (gameManager.GetCurrentPlayer() != "enemy")
        {
            Debug.LogWarning("Attempting to move when it's not AI's turn.");
            yield break;
        }

        Debug.Log("AI is making a move.");

        List<GameObject> enemyPieces = new List<GameObject>();

        // Ambil semua bidak musuh
        foreach (GameObject piece in gameController.enemy)
        {
            if (piece != null)
            {
                enemyPieces.Add(piece);
            }
        }

        if (enemyPieces.Count > 0)
        {
            // Pilih bidak musuh secara acak
            GameObject selectedPiece = enemyPieces[Random.Range(0, enemyPieces.Count)];
            Chessman cm = selectedPiece.GetComponent<Chessman>();
            cm.DestroyMovePlates();
            cm.InitiateMovePlates();

            List<GameObject> movePlates = new List<GameObject>(GameObject.FindGameObjectsWithTag("MovePlate"));

            if (movePlates.Count > 0)
            {
                // Pilih move plate secara acak
                GameObject selectedMovePlate = movePlates[Random.Range(0, movePlates.Count)];
                Debug.Log($"AI selected piece {selectedPiece.name} and move plate at ({selectedMovePlate.GetComponent<MovePlate>().GetX()}, {selectedMovePlate.GetComponent<MovePlate>().GetY()}).");
                
                // Klik move tile yang dipilih
                selectedMovePlate.GetComponent<MovePlate>().OnMouseUp();
                Debug.Log("AI has made a random move and clicked the move tile.");
            }
            else
            {
                Debug.LogWarning("No valid moves available for AI.");
            }
        }
        else
        {
            Debug.LogWarning("No enemy pieces found for AI to move.");
        }

        gameManager.NextTurn();
    }


    public void EndPlayerTurn()
    {
        previousMoves.Clear(); // Hapus riwayat langkah AI
        gameManager.NextTurn();
    }
}
