using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ChessGameManager : MonoBehaviour
{
    // Variabel untuk melacak giliran pemain
    private string currentPlayer;
    private bool gameOver = false;

    // Referensi ke skrip Game
    private Game gameController;
    private bool isPlayerLocked = false;

    private bool playerHasMoved = false;

    void Start()
    {
        gameController = FindObjectOfType<Game>();
        if (gameController == null)
        {
            Debug.LogError("Game controller not found!");
            return;
        }

        // Tentukan giliran pertama secara acak
        // if (Random.value > 0.5f)
        // {
            currentPlayer = "player";
        //     Debug.Log("Player starts first.");
        // }
        // else
        // {
        //     currentPlayer = "enemy";
        //     Debug.Log("AI starts first.");
        // }
    }

    public void LockPlayerInteraction()
    {
        isPlayerLocked = true;
    }

    public void UnlockPlayerInteraction()
    {
        isPlayerLocked = false;
    }

    public bool IsPlayerLocked()
    {
        return isPlayerLocked;
    }

    public string GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public void Winner(string playerWinner)
    {
        gameOver = true;
        Debug.Log(playerWinner + " is the winner");

        if (playerWinner == "player")
        {
            gameController.resultText.text = "Menang!";
            StartCoroutine(EndMatch("GamePlay"));
        }
        else
        {
            gameController.resultText.text = "Game Over!";
            StartCoroutine(EndMatch("Menu"));
        }
        gameController.resultPanel.SetActive(true);
    }

    private IEnumerator EndMatch(string sceneName)
    {
        yield return new WaitForSeconds(3f);
        EncounterManager.Instance.EndEncounter();

        // Kembali ke scene yang ditentukan
        SceneManager.LoadScene(sceneName);
    }
    
    public void SwitchToPlayerTurn()
    {
        if (currentPlayer == "enemy")
        {
            currentPlayer = "player";
            Debug.Log("Switching to player turn.");
        }
        else
        {
            Debug.LogWarning("Attempted to switch to player turn when it's already player's turn.");
        }
    }

    public void PlayerMoveCompleted()
    {
        if (currentPlayer != "player")
        {
            Debug.LogWarning("PlayerMoveCompleted called when it's not player's turn!");
            return;
        }

        playerHasMoved = true; // Tandai bahwa pemain telah selesai bergerak
        Debug.Log("Player has completed their move.");
        NextTurn(); // Pindahkan giliran ke AI
    }

    public void NextTurn()
    {
        Debug.Log("NextTurn called.");
        
        if (gameController == null)
        {
            Debug.LogError("Game controller not set!");
            return;
        }

        StopAllCoroutines();  // Pastikan menghentikan semua coroutine saat ganti giliran

        if (currentPlayer == "player")
        {
            currentPlayer = "enemy";
            Debug.Log("Switching to enemy turn.");
            StartCoroutine(StartAIMove()); // Mulai pergerakan AI
        }
        else
        {
            currentPlayer = "player";
            Debug.Log("Switching to player turn.");
        }
    }


    public void EnableAttackMode()
    {
        gameController.EnableAttackMode();
    }

    public void EnableMagicAttackMode()
    {
        gameController.EnableMagicAttackMode();
    }

    // Tambahkan metode `ShowPieceInfo` dan `ShowEnemyInfo`
    public void ShowPieceInfo(Chessman chessman)
    {
        gameController.ShowPieceInfo(chessman);
    }

    public void ShowEnemyInfo(Chessman chessman)
    {
        gameController.ShowEnemyInfo(chessman);
    }
    
    public Game GetGameController()
    {
        return gameController;
    }

    private IEnumerator StartAIMove()
    {
        Debug.Log("Starting AI move after delay.");
        yield return new WaitForSeconds(1f); // Jeda sebelum memulai gerakan AI
        ChessAI chessAI = gameController.GetComponent<ChessAI>();
        if (chessAI != null)
        {
            Debug.Log("ChessAI component found.");
            yield return chessAI.MakeEnemyMove();
        }
        else
        {
            Debug.LogError("ChessAI component not found on gameController!");
        }
    }

}
