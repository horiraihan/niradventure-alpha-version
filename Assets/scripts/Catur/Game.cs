using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MyMove;
using TMPro;

public class Game : MonoBehaviour
{
    //Reference from Unity IDE
    public GameObject chesspiece;

    private GameObject[,] positions = new GameObject[8, 8];
    public GameObject[] player = new GameObject[16];
    public GameObject[] enemy = new GameObject[16];
    public GameObject panelKarakter;
    public GameObject panelEnemy;
    public GameObject resultPanel; 
    
    public TextMeshProUGUI namaKarakter;
    public TextMeshProUGUI HpKarakter;
    public TextMeshProUGUI namaEnemy;
    public TextMeshProUGUI HpEnemy;
    public TextMeshProUGUI resultText;


    // UI Elements
    public Button attackButton;
    public Button magicAttackButton;
    public Button destroyAllEnemiesButton;
    public Button destroyAllPlayerButton;

    //current turn
    private string currentPlayer = "player";
    private Chessman lastSelectedChessman = null;

    //Game Ending
    private bool gameOver = false;

    // Variabel untuk mengecek mode serangan
    private bool isAttackMode = false;
    public bool IsAttackMode
    {
        get { return isAttackMode; }
    }

    private bool isMagicAttackMode = false;
    public bool IsMagicAttackMode
    {
        get { return isMagicAttackMode; }
    }

    public ChessGameManager gameManager;

    public void Start()
    {
        gameManager = GameObject.FindObjectOfType<ChessGameManager>();

        // Inisialisasi tombol attack
        attackButton = GameObject.Find("AttackButton").GetComponent<Button>();
        attackButton.onClick.AddListener(() => EnableAttackMode());

        magicAttackButton = GameObject.Find("MagicAttackButton").GetComponent<Button>();
        magicAttackButton.onClick.AddListener(() => EnableMagicAttackMode());

        resultPanel.SetActive(false); // Panel tidak aktif saat permainan dimulai

        panelKarakter.SetActive(false); // Panel tidak aktif saat permainan dimulai
        panelEnemy.SetActive(false);

        destroyAllEnemiesButton = GameObject.Find("DestroyAllEnemiesButton")?.GetComponent<Button>(); 
        if (destroyAllEnemiesButton != null)
        {
            destroyAllEnemiesButton.onClick.AddListener(DestroyAllEnemyPieces);
        }

        Button destroyAllPlayerPiecesButton = GameObject.Find("DestroyAllPlayerPiecesButton")?.GetComponent<Button>(); 
        if (destroyAllPlayerPiecesButton != null)
        {
            destroyAllPlayerPiecesButton.onClick.AddListener(DestroyAllPlayerPieces);
        }

        player = new GameObject[] {
            //N1ra = King, Dayang = Queen, Bawi& Liaw = Knight, Semaring&Hakoz = rook, Galuh&Juja = bsop 
            Create("playerN1ra", 4, 0), Create("playerDayang", 3, 0), //King, Queen
            Create("playerBawi", 6, 0), Create("playerLiaw", 1, 0), // Knight
            Create("playerSemaring", 0, 0), Create("playerHakoz", 7, 0), // rook
            Create("playerGaluh", 2, 0), Create("playerJuja", 5, 0), //bishop
            Create("playerSoldier", 0, 1), Create("playerSoldier", 1, 1),
            Create("playerSoldier", 2, 1), Create("playerSoldier", 3, 1),
            Create("playerSoldier", 4, 1), Create("playerSoldier", 5, 1),
            Create("playerSoldier", 6, 1), Create("playerSoldier", 7, 1)
        };
        enemy = new GameObject[] { 
            Create("black_rook", 0, 7), Create("black_knight", 1, 7),
            Create("black_bishop", 2, 7), Create("black_queen", 3, 7),
            Create("black_king", 4, 7), Create("black_bishop", 5, 7),
            Create("black_knight", 6, 7), Create("black_rook", 7, 7),
            Create("black_pawn", 0, 6), Create("black_pawn", 1, 6),
            Create("black_pawn", 2, 6), Create("black_pawn", 3, 6),
            Create("black_pawn", 4, 6), Create("black_pawn", 5, 6),
            Create("black_pawn", 6, 6), Create("black_pawn", 7, 6)
        };

        // Set all piece positions on the position board
        for (int i = 0; i < player.Length; i++)
        {
            SetPosition(player[i], player[i].GetComponent<Chessman>().GetXBoard(), player[i].GetComponent<Chessman>().GetYBoard());
        }
        for (int i = 0; i < enemy.Length; i++)
        {
            SetPosition(enemy[i], enemy[i].GetComponent<Chessman>().GetXBoard(), enemy[i].GetComponent<Chessman>().GetYBoard());
        }
    }

    public GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(chesspiece, new Vector3(0, 0, -1), Quaternion.identity);
        Chessman cm = obj.GetComponent<Chessman>();
        cm.name = name;
        cm.SetXBoard(x);
        cm.SetYBoard(y);
        cm.Activate();

        // Inisialisasi HP berdasarkan nama bidak
        switch (name)
        {
            case "playerN1ra":
            case "playerDayang":
            case "black_king":
            case "black_queen":
                cm.SetHP(170);
                break;
            case "playerSemaring":
            case "playerJuja":
            case "playerHakoz":
            case "playerGaluh":
            case "playerLiaw":
            case "playerBawi":
            case "black_rook":
            case "black_knight":
            case "black_bishop":
                cm.SetHP(150);
                break;
            case "playerSoldier":
            case "black_pawn":
                cm.SetHP(90);
                break;
            default:
                cm.SetHP(0);
                break;
        }

        return obj;
    }

public void MovePiece(GameObject piece, int newX, int newY)
{
    Chessman cm = piece.GetComponent<Chessman>();

    // Set posisi lama sebagai kosong
    SetPositionEmpty(cm.GetXBoard(), cm.GetYBoard());

    // Perbarui posisi baru
    cm.SetXBoard(newX);
    cm.SetYBoard(newY);
    cm.SetCoords();
    SetPosition(piece, newX, newY);  // Pastikan posisi baru di matriks diperbarui
    
    // Tandai bahwa pemain telah menyelesaikan pergerakannya
    ChessGameManager gameManager = FindObjectOfType<ChessGameManager>();
    if (gameManager != null)
    {
        gameManager.PlayerMoveCompleted();
    }
}

    public void SetPosition(GameObject obj, int x, int y)
    {
        Chessman cm = obj.GetComponent<Chessman>();
        cm.SetXBoard(x);
        cm.SetYBoard(y);
        cm.SetCoords();
        positions[x, y] = obj;
        
    }


    public void SetPositionEmpty(int x, int y)
    {
        
        positions[x, y] = null;
    }

    public GameObject GetPosition(int x, int y)
    {
        if (x < 0 || y < 0 || x >= positions.GetLength(0) || y >= positions.GetLength(1))
        {
            
            return null;
        }
       
        return positions[x, y];
    }


    public bool PositionOnBoard(int x, int y)
    {
    
        if (x < 0 || y < 0 || x >= positions.GetLength(0) || y >= positions.GetLength(1))
        {
        
            return false;
        }

        return true;
    }

    public IEnumerator MakeEnemyMove()
    {
        yield return new WaitForSeconds(1f); // Beri jeda sebelum AI menggerakkan bidak

        while (gameManager.GetCurrentPlayer() == "enemy")
        {
            List<GameObject> enemyPieces = new List<GameObject>();
            foreach (GameObject piece in enemy)
            {
                if (piece != null)
                {
                    enemyPieces.Add(piece);
                }
            }

            if (enemyPieces.Count > 0)
            {
                GameObject selectedPiece = enemyPieces[Random.Range(0, enemyPieces.Count)];
                Chessman cm = selectedPiece.GetComponent<Chessman>();
                cm.DestroyMovePlates();

                GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
                if (movePlates.Length > 0)
                {
                    GameObject selectedMovePlate = movePlates[Random.Range(0, movePlates.Length)];
                    selectedMovePlate.GetComponent<MovePlate>().OnMouseUp();
                    yield break;
                }
            }
            else
            {
                Debug.LogWarning("No more moves available for enemy.");
                break;
            }
        }

        gameManager.NextTurn(); // Kembalikan giliran ke pemain
    }

    public int EvaluateBoard(GameObject[,] board)
    {
        int score = 0;
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (board[i, j] != null)
                {
                    score += GetPieceValue(board[i, j].GetComponent<Chessman>());
                    // Tambahkan evaluasi untuk kontrol pusat
                    if ((i == 3 || i == 4) && (j == 3 || j == 4))
                    {
                        score += 1; // Kontrol pusat lebih berharga
                    }
                }
            }
        }
        Debug.Log($"EvaluateBoard: Score={score}");
        return score;
    }


    public int GetPieceValue(Chessman piece)
    {
        switch (piece.name)
        {
            case "playerN1ra": return 1000;
            case "playerDayang": return 9;
            case "playerBawi": return 3;
            case "playerLiaw": return 3;
            case "playerSoldier": return 1;
            case "black_king": return -1000;
            case "black_queen": return -9;
            case "black_knight": return -3;
            case "black_bishop": return -3;
            case "black_pawn": return -1;
            default: return 0;
        }
    }

    public int Minimax(GameObject[,] board, int depth, bool isMaximizingPlayer, int alpha, int beta)
    {
        Debug.Log($"Minimax called: Depth={depth}, isMaximizingPlayer={isMaximizingPlayer}, Alpha={alpha}, Beta={beta}");
        int score = EvaluateBoard(board);
        if (depth == 0 || Mathf.Abs(score) == 1000)
        {
            Debug.Log($"Depth: {depth}, Score: {score}");
            return score;
        }

        if (isMaximizingPlayer)
        {
            int maxEval = int.MinValue;
            foreach (var move in GetAllPossibleMoves("enemy"))
            {
                MakeMove(board, move);
                int eval = Minimax(board, depth - 1, false, alpha, beta);
                UndoMove(board, move);
                maxEval = Mathf.Max(maxEval, eval);
                alpha = Mathf.Max(alpha, eval);
                if (beta <= alpha) break;
            }
            Debug.Log($"Maximizing: Depth: {depth}, MaxEval: {maxEval}, Alpha: {alpha}, Beta: {beta}");
            return maxEval;
        }
        else
        {
            int minEval = int.MaxValue;
            foreach (var move in GetAllPossibleMoves("player"))
            {
                MakeMove(board, move);
                int eval = Minimax(board, depth - 1, true, alpha, beta);
                UndoMove(board, move);
                minEval = Mathf.Min(minEval, eval);
                beta = Mathf.Min(beta, eval);
                if (beta <= alpha) break;
            }
            Debug.Log($"Minimizing: Depth: {depth}, MinEval: {minEval}, Alpha: {alpha}, Beta: {beta}");
            return minEval;
        }
    }

    public List<Move> GetAllPossibleMoves(string player)
    {
        List<Move> possibleMoves = new List<Move>();
        GameObject[] playerPieces = player == "player" ? this.player : this.enemy;

        foreach (GameObject piece in playerPieces)
        {
            if (piece != null)
            {
                Chessman cm = piece.GetComponent<Chessman>();
                cm.DestroyMovePlates();
                GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
                foreach (GameObject movePlate in movePlates)
                {
                    MovePlate mp = movePlate.GetComponent<MovePlate>();
                    if (cm.name == "playerSoldier" || cm.name == "black_pawn")
                    {
                        // Pion hanya bisa makan secara diagonal
                        if ((Mathf.Abs(mp.GetX() - cm.GetXBoard()) == 1) && (Mathf.Abs(mp.GetY() - cm.GetYBoard()) == 1))
                        {
                            possibleMoves.Add(new Move(cm.GetXBoard(), cm.GetYBoard(), mp.GetX(), mp.GetY()));
                        }
                    }
                    else
                    {
                        if (mp.GetX() != cm.GetXBoard() || mp.GetY() != cm.GetYBoard())
                        {
                            possibleMoves.Add(new Move(cm.GetXBoard(), cm.GetYBoard(), mp.GetX(), mp.GetY()));
                        }
                    }
                }
            }
        }

        return possibleMoves;
    }


    public void MakeMove(GameObject[,] board, Move move)
    {
        GameObject piece = board[move.startX, move.startY];
        if (piece == null)
        {
            Debug.LogError($"No piece found at start position ({move.startX}, {move.startY})");
            return;
        }

        board[move.startX, move.startY] = null;
        board[move.endX, move.endY] = piece;
        piece.GetComponent<Chessman>().SetXBoard(move.endX);
        piece.GetComponent<Chessman>().SetYBoard(move.endY);
        piece.GetComponent<Chessman>().SetCoords();
    }

    public void UndoMove(GameObject[,] board, Move move)
    {
        GameObject piece = board[move.endX, move.endY];
        if (piece == null)
        {
            Debug.LogError($"Piece to undo move is null at position ({move.endX}, {move.endY})");
            return;
        }

        board[move.endX, move.endY] = null;
        board[move.startX, move.startY] = piece;
        piece.GetComponent<Chessman>().SetXBoard(move.startX);
        piece.GetComponent<Chessman>().SetYBoard(move.startY);
        piece.GetComponent<Chessman>().SetCoords();
    }

//HP karakter

    public void ShowPieceInfo(Chessman chessman)
    {
        if (lastSelectedChessman == chessman)
        {
            panelKarakter.SetActive(false); // Sembunyikan panel jika bidak yang sama diklik dua kali
            chessman.DestroyMovePlates(); // Hapus move tiles
            lastSelectedChessman = null;
        }
        else
        {
            panelKarakter.SetActive(true);
            panelEnemy.SetActive(false); // Nonaktifkan panel musuh saat panel karakter aktif
            namaKarakter.text = chessman.name;
            HpKarakter.text = "HP: " + chessman.HP.ToString();
            lastSelectedChessman = chessman; // Simpan bidak yang terakhir diklik
            chessman.DestroyMovePlates(); // Hapus move tiles sebelumnya
        }
    }

    public void ShowEnemyInfo(Chessman chessman)
    {
        if (lastSelectedChessman == chessman)
        {
            panelEnemy.SetActive(false); // Sembunyikan panel jika bidak yang sama diklik dua kali
            lastSelectedChessman = null;
        }
        else
        {
            panelEnemy.SetActive(true);
            panelKarakter.SetActive(false); // Nonaktifkan panel karakter saat panel musuh aktif
            namaEnemy.text = chessman.name;
            HpEnemy.text = "HP: " + chessman.HP.ToString();
            lastSelectedChessman = chessman; // Simpan bidak yang terakhir diklik
        }
    }

    public void HidePieceInfo()
    {
        panelKarakter.SetActive(false);
        panelEnemy.SetActive(false);
    }

    public Chessman GetLastSelectedChessman()
    {
        return lastSelectedChessman;
    }

    // Logika untuk menyerang target (fungsi AttackSelectedEnemy sebelumnya)
    public void AttackSelectedEnemy()
    {
        if (lastSelectedChessman != null && lastSelectedChessman.player == "player")
        {
            Chessman target = GetSelectedEnemy();
            if (target != null)
            {
                lastSelectedChessman.Attack(target);
                UpdateEnemyPanel(target); // Perbarui panel musuh dengan HP terbaru
                if (target.HP <= 0)
                {
                    HidePieceInfo(); // Sembunyikan panel jika target mati
                }
                // Matikan mode serangan setelah serangan dilakukan
                isAttackMode = false;
            }
        }
    }

    private Chessman GetSelectedEnemy()
    {
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        foreach (GameObject movePlate in movePlates)
        {
            MovePlate mp = movePlate.GetComponent<MovePlate>();
            if (mp.attack && mp.GetReference() != null && mp.GetReference().GetComponent<Chessman>().player == "enemy")
            {
                return mp.GetReference().GetComponent<Chessman>();
            }
        }
        return null;
    }

    public void DisableAttackMode()
    {
        isAttackMode = false;
    }

    // Fungsi untuk memperbarui panel musuh
    private void UpdateEnemyPanel(Chessman target)
    {
        if (target != null)
        {
            namaEnemy.text = target.name;
            HpEnemy.text = "HP: " + target.HP.ToString();
        }
    }

    // Fungsi untuk end turn
    public void EndTurn()
    {
        gameManager.NextTurn(); // Pindahkan giliran ke AI
    }

    public void EnableAttackMode()
    {
        isMagicAttackMode = false;
        isAttackMode = true;
        
        // Periksa bidak yang terakhir dipilih
        if (lastSelectedChessman != null)
        {
            lastSelectedChessman.DestroyMovePlates();
            lastSelectedChessman.InitiateMovePlates();
        }
    }

    public void EnableMagicAttackMode()
    {
        isMagicAttackMode = true; // Aktifkan mode magic attack
        isAttackMode = false; // Pastikan mode serangan biasa dinonaktifkan

        // Periksa bidak yang terakhir dipilih
        if (lastSelectedChessman != null)
        {
            lastSelectedChessman.DestroyMovePlates();
            lastSelectedChessman.InitiateMovePlates();
        }
    }
    public void HandleMagicAttack()
    {
        // Dapatkan bidak player dan target untuk serangan magic
        Chessman player = GetPlayerChessman();
        Chessman target = GetTargetChessman();

        if (player != null && target != null)
        {
            player.StartCoroutine(player.PerformMagicAttack(target));
        }
    }

    public void DisableMagicAttackMode()
    {
        isMagicAttackMode = false;
    }

    public Chessman GetPlayerChessman()
    {
        // Logika untuk mendapatkan bidak player yang akan melakukan serangan
        return FindObjectOfType<Chessman>(); // Mengambil bidak pertama yang ditemukan, sesuaikan dengan logika permainan Anda
    }

    public Chessman GetTargetChessman()
    {
        // Logika untuk mendapatkan target bidak player
        return FindObjectOfType<Chessman>(); // Mengambil bidak pertama yang ditemukan, sesuaikan dengan logika permainan Anda
    }

    public GameObject[] GetEnemyPieces()
    {
        Debug.Log("Fetching enemy pieces...");
        GameObject[] enemyPieces = GameObject.FindGameObjectsWithTag("enemy");
        Debug.Log($"Found {enemyPieces.Length} enemy pieces.");
        return enemyPieces;
    }


    //////// ai

public void DestroyAllEnemyPieces()
{
    Debug.Log("DestroyAllEnemyPieces button clicked.");

    for (int i = 0; i < enemy.Length; i++)
    {
        if (enemy[i] != null)
        {
            Destroy(enemy[i]);
            enemy[i] = null; // Hapus referensi dari array setelah menghancurkan bidak
        }
    }
    Debug.Log("All enemy pieces have been destroyed.");

    // Periksa apakah semua bidak musuh telah dihancurkan
    bool allEnemiesDestroyed = true;
    foreach (GameObject enemyPiece in enemy)
    {
        if (enemyPiece != null)
        {
            allEnemiesDestroyed = false;
            break;
        }
    }

    if (allEnemiesDestroyed)
    {
        gameManager.Winner("player");
    }
}

public void DestroyAllPlayerPieces()
{
    if (player == null)
    {
        Debug.LogError("Player array is null!");
        return;
    }

    for (int i = 0; i < player.Length; i++)
    {
        if (player[i] != null)
        {
            Destroy(player[i]);
            player[i] = null; // Hapus referensi dari array setelah menghancurkan bidak
        }
    }
    Debug.Log("All player pieces have been destroyed.");

    // Periksa apakah semua bidak pemain telah dihancurkan
    bool allPlayersDestroyed = true;
    foreach (GameObject playerPiece in player)
    {
        if (playerPiece != null)
        {
            allPlayersDestroyed = false;
            break;
        }
    }

    if (allPlayersDestroyed)
    {
        gameManager.Winner("enemy");
    }
}




}

