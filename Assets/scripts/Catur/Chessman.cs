using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chessman : MonoBehaviour
{
    // Referensi ke objek dalam Scene Unity kita
    public GameObject controller;
    public GameObject movePlate;

    // Posisi bidak ini di papan
    private int xBoard = -1;
    private int yBoard = -1;
    
    public bool isFirstMove = true;

    public string player;

    public Sprite black_queen, black_knight, black_bishop, black_king, black_rook, black_pawn;
    public Sprite playerN1ra, playerDayang, playerBawi, playerLiaw, playerJuja, playerSemaring, playerHakoz, playerGaluh, playerSoldier;

    public int HP;
    private Game gameController;

    private Dictionary<string, int> pieceValues = new Dictionary<string, int>()
    {
        { "pawn", 1 },
        { "knight", 3 },
        { "bishop", 3 },
        { "rook", 5 },
        { "queen", 9 },
        { "king", 7 } // Nilai raja dapat diatur sesuai kebutuhan
    };

    // Method untuk mendapatkan nilai bidak
    public int GetPieceValue()
    {
        string pieceType = this.GetType().Name.ToLower();
        if (pieceValues.ContainsKey(pieceType))
        {
            return pieceValues[pieceType];
        }
        return 0;
    }
    

    void Start() 
    { 

        gameController = FindObjectOfType<Game>(); // Inisialisasi gameController
        if (gameController == null)
        {
            Debug.LogError("Game Controller not found");
        }
        controller = GameObject.FindGameObjectWithTag("GameController"); // Inisialisasi controller
        if (controller == null)
        {
            Debug.LogError("Controller not found");
        }
    }

    public void SetHP(int hp)
    {
        HP = hp;
    }

    public void Activate()
    {
        // Dapatkan game controller
        controller = GameObject.FindGameObjectWithTag("GameController");

        // Ambil lokasi yang diinstansiasi dan sesuaikan transformasinya
        SetCoords();

        // Pilih sprite yang benar berdasarkan nama bidak
        switch (this.name)
        {
            case "playerN1ra":
                this.GetComponent<SpriteRenderer>().sprite = playerN1ra; player = "player"; 
                this.tag = "player";
                // animator.runtimeAnimatorController = animatorN1ra;
                break;
            case "playerDayang":
                this.GetComponent<SpriteRenderer>().sprite = playerDayang; player = "player"; 
                this.tag = "player";
                // animator.runtimeAnimatorController = animatorDayang;
                break;
            case "playerBawi":
                this.GetComponent<SpriteRenderer>().sprite = playerBawi; player = "player"; 
                this.tag = "player";
                // animator.runtimeAnimatorController = animatorBawi;
                break;
            case "playerLiaw":
                this.GetComponent<SpriteRenderer>().sprite = playerLiaw; player = "player"; 
                this.tag = "player";
                // animator.runtimeAnimatorController = animatorLiaw;
                break;
            case "playerJuja":
                this.GetComponent<SpriteRenderer>().sprite = playerJuja; player = "player"; 
                this.tag = "player";
                // animator.runtimeAnimatorController = animatorJuja;
                break;
            case "playerSemaring":
                this.GetComponent<SpriteRenderer>().sprite = playerSemaring; player = "player"; 
                this.tag = "player";
                // animator.runtimeAnimatorController = animatorSemaring;
                break;
            case "playerHakoz":
                this.GetComponent<SpriteRenderer>().sprite = playerHakoz; player = "player"; 
                this.tag = "player";
                // animator.runtimeAnimatorController = animatorHakoz;
                break;
            case "playerGaluh":
                this.GetComponent<SpriteRenderer>().sprite = playerGaluh; player = "player"; 
                this.tag = "player";
                // animator.runtimeAnimatorController = animatorGaluh;
                break;
            case "playerSoldier":
                this.GetComponent<SpriteRenderer>().sprite = playerSoldier; player = "player"; 
                this.tag = "player";
                // animator.runtimeAnimatorController = slashAttackController;
                break;
            case "black_queen": 
                this.GetComponent<SpriteRenderer>().sprite = black_queen; player = "enemy"; 
                this.tag = "enemy";
                break;
            case "black_knight": 
                this.GetComponent<SpriteRenderer>().sprite = black_knight; player = "enemy"; 
                this.tag = "enemy";
                break;
            case "black_bishop": 
                this.GetComponent<SpriteRenderer>().sprite = black_bishop; player = "enemy"; 
                this.tag = "enemy";
                break;
            case "black_king": 
                this.GetComponent<SpriteRenderer>().sprite = black_king; player = "enemy"; 
                this.tag = "enemy";
                break;
            case "black_rook": 
                this.GetComponent<SpriteRenderer>().sprite = black_rook; player = "enemy"; 
                this.tag = "enemy";
                break;
            case "black_pawn": 
                this.GetComponent<SpriteRenderer>().sprite = black_pawn; player = "enemy"; 
                this.tag = "enemy";
                break;
        }
    }

    public void SetCoords()
    {
        // Dapatkan nilai papan untuk dikonversi ke koordinat xy
        float x = xBoard;
        float y = yBoard;

        // Sesuaikan dengan offset variabel
        x *= 0.66f;
        y *= 0.66f;

        // Tambahkan konstanta (pos 0,0)
        x += -2.3f;
        y += -2.3f;

        // Set nilai unity yang sebenarnya
        this.transform.position = new Vector3(x, y, -1.0f);
    }

    public int GetXBoard()
    {
        return xBoard;
    }

    public int GetYBoard()
    {
        return yBoard;
    }

    public void SetXBoard(int x)
    {
        xBoard = x;
    }

    public void SetYBoard(int y)
    {
        yBoard = y;
    }

private void OnMouseUp()
{
    ChessGameManager gameManager = FindObjectOfType<ChessGameManager>();
    if (gameManager == null)
    {
        Debug.LogError("ChessGameManager not found");
        return;
    }

    Game gameController = gameManager.GetGameController();
    if (gameController == null)
    {
        Debug.LogError("Game Controller not found in ChessGameManager");
        return;
    }

    // Periksa apakah mode serangan aktif dan bidak yang berbeda dipilih
    if (gameManager.GetCurrentPlayer() == "player")
    {
        if (gameController.IsAttackMode && gameController.GetLastSelectedChessman() != null)
        {
            Chessman lastSelected = gameController.GetLastSelectedChessman();
            if (lastSelected != this)
            {
                // Nonaktifkan mode serangan
                gameController.DisableAttackMode();
                lastSelected.DestroyMovePlates();
            }
        }

        // Tampilkan informasi bidak saat ini
        if (player == "player")
        {
            gameManager.ShowPieceInfo(this);
        }
        else if (player == "enemy")
        {
            gameManager.ShowEnemyInfo(this);
        }

        DestroyMovePlates(); 
        InitiateMovePlates();
    }
}


    public void DestroyMovePlates()
    {
        // Hancurkan MovePlates lama
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        for (int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i]);
        }
    }

    private void MagicMovePlates()
    {
                // Panggil metode yang sama seperti serangan biasa untuk menampilkan move plates
        NormalMovePlates();
    }

    public void InitiateMovePlates()
    {
        Game sc = controller.GetComponent<Game>();

        if (sc == null)
        {
            Debug.LogError("Game controller not found.");
            return;
        }

        Debug.Log($"{name} is initiating move plates.");

        // Memeriksa apakah mode serangan aktif
        if (sc.IsMagicAttackMode)
        {
            MagicMovePlates();
        }
        else if (sc.IsAttackMode)
        {
            NormalMovePlates();
        }
        else
        {
            NormalMovePlates();
        }
    }

        
    private void NormalMovePlates()
    {
        // Logika untuk menampilkan move plates serangan biasa
        switch (this.name)
        {
            case "black_queen":
            case "playerDayang":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(1, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                LineMovePlate(-1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(1, -1);
                break;
            case "black_knight":
            case "playerBawi":
            case "playerLiaw":
                LMovePlate();
                break;
            case "black_bishop":
            case "playerJuja":
            case "playerGaluh":
                LineMovePlate(1, 1);
                LineMovePlate(1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(-1, -1);
                break;
            case "black_king":
            case "playerN1ra":
                SurroundMovePlate();
                break;
            case "black_rook":
            case "playerHakoz":
            case "playerSemaring":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                break;
            case "black_pawn":
                PawnMovePlate((int)xBoard, (int)yBoard - 1);
                break;
            case "playerSoldier":
                PawnMovePlate((int)xBoard, (int)yBoard + 1);
                if (yBoard == 1)
                {
                    PawnMovePlate((int)xBoard, (int)yBoard + 2);
                }
                PawnMovePlateAttack((int)xBoard - 1, (int)yBoard + 1);
                PawnMovePlateAttack((int)xBoard + 1, (int)yBoard + 1);
                break;
            default:
                break;
        }
    }

    private void LineMovePlate(int xIncrement, int yIncrement, bool attack = false)
    {
        Game sc = controller.GetComponent<Game>();

        int x = xBoard + xIncrement;
        int y = yBoard + yIncrement;

        while (sc.PositionOnBoard(x, y))
        {
            GameObject cp = sc.GetPosition(x, y);

            if (cp == null)
            {
                Debug.Log($"Spawning move plate at ({x}, {y}).");
                MovePlateSpawn(x, y);
            }
            else
            {
                Chessman target = cp.GetComponent<Chessman>();
                if (target.player != player && attack)
                {
                    Debug.Log($"Spawning attack move plate at ({x}, {y}) for target {target.name}.");
                    MovePlateAttackSpawn(x, y);
                }
                break; // Hentikan loop jika bertemu bidak lain, baik milik sendiri maupun musuh
            }
            x += xIncrement;
            y += yIncrement;
        }
    }

    public void LMovePlate()
    {
       
        PointMovePlate(xBoard + 1, yBoard + 2);
        PointMovePlate(xBoard - 1, yBoard + 2);
        PointMovePlate(xBoard + 2, yBoard + 1);
        PointMovePlate(xBoard + 2, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard - 2);
        PointMovePlate(xBoard - 1, yBoard - 2);
        PointMovePlate(xBoard - 2, yBoard + 1);
        PointMovePlate(xBoard - 2, yBoard - 1);
    }

    public void SurroundMovePlate()
    {
        
        PointMovePlate(xBoard, yBoard + 1);
        PointMovePlate(xBoard, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard);
        PointMovePlate(xBoard - 1, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard + 1);
        PointMovePlate(xBoard + 1, yBoard);
        PointMovePlate(xBoard + 1, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard + 1);
    }

    private void PointMovePlate(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();

        if (sc.PositionOnBoard(x, y))
        {
            GameObject cp = sc.GetPosition(x, y);
            if (cp == null)
            {
                Debug.Log($"Spawning move plate at ({x}, {y}).");
                MovePlateSpawn(x, y);
            }
            else if (cp.GetComponent<Chessman>().player != player && (sc.IsAttackMode || sc.IsMagicAttackMode))
            {
                Debug.Log($"Spawning attack move plate at ({x}, {y}) for target {cp.GetComponent<Chessman>().name}.");
                MovePlateAttackSpawn(x, y);
            }
        }
    }

       
    public void PawnMovePlate(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();
    

        if (sc.PositionOnBoard(x, y))
        {
            if (sc.GetPosition(x, y) == null)
            {
                MovePlateSpawn(x, y);
            }

        }
    }

    private void PawnMovePlateAttack(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();
        if (sc.PositionOnBoard(x, y))
        {
            GameObject cp = sc.GetPosition(x, y);
            if (cp != null)
            {
                Chessman target = cp.GetComponent<Chessman>();
                if (target.player != player && (sc.IsAttackMode || sc.IsMagicAttackMode))
                {
                    MovePlateAttackSpawn(x, y);
                }
            }
        }
    }


    public void MovePlateSpawn(int matrixX, int matrixY)
    {
        float x = matrixX;
        float y = matrixY;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += -2.3f;

        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);
        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }

    public void MovePlateAttackSpawn(int matrixX, int matrixY)
    {
        float x = matrixX;
        float y = matrixY;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += -2.3f;

        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);
        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.attack = true;
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }

    public void MoveTo(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();
        sc.SetPositionEmpty(xBoard, yBoard); // Set posisi lama menjadi kosong
        SetXBoard(x);
        SetYBoard(y);
        SetCoords();
        sc.SetPosition(this.gameObject, x, y); // Set posisi baru
    }

    public void Attack(Chessman target)
    {
        StartCoroutine(PerformAttack(target));
    }

    public void MagicAttack(Chessman player, Chessman target)
    {
        player.StartCoroutine(player.PerformMagicAttack(target));
    }

    private IEnumerator PerformAttack(Chessman target)
    {
        // Simpan posisi awal
        int originalX = GetXBoard();
        int originalY = GetYBoard();
 

        // Simpan posisi target
        int targetX = target.GetXBoard();
        int targetY = target.GetYBoard();

        // Pindahkan bidak ke posisi target dan serang
        MoveTo(targetX, targetY);
        controller.GetComponent<Game>().SetPosition(gameObject, targetX, targetY);


        // Serang target
        int damage = 30; 
        target.HP -= damage;

        if (target.HP <= 0)
        {
            controller.GetComponent<Game>().SetPositionEmpty(targetX, targetY);
            Destroy(target.gameObject);
            // Debug.Log($"{target.name} destroyed");
        }
        else
        {
            // Perbarui posisi target jika target masih hidup
            controller.GetComponent<Game>().SetPosition(target.gameObject, targetX, targetY);
  
        }

        // Tunggu selama 1 detik (atau sesuai kebutuhan animasi)
        yield return new WaitForSeconds(1.0f);

        // Hancurkan move plates untuk mencegah konflik
        DestroyMovePlates();

        // Kembali ke posisi awal
        MoveTo(originalX, originalY);
        controller.GetComponent<Game>().SetPosition(gameObject, originalX, originalY);
    

        // Pindahkan giliran ke AI setelah serangan
        controller.GetComponent<Game>().gameManager.NextTurn();
    }

public IEnumerator PerformMagicAttack(Chessman target)
{
    // Simpan posisi awal
    int originalX = GetXBoard();
    int originalY = GetYBoard();
    
    // Simpan posisi target
    int targetX = target.GetXBoard();
    int targetY = target.GetYBoard();
    
    // Pindahkan bidak ke posisi target dan serang
    MoveTo(targetX, targetY);
    controller.GetComponent<Game>().SetPosition(gameObject, targetX, targetY);
   

    // Berikan damage
    int magicDamage = 50; // Jumlah damage untuk serangan magic
    target.HP -= magicDamage;

    // Periksa apakah target masih hidup dan perbarui posisinya
    if (target.HP <= 0)
    {
        controller.GetComponent<Game>().SetPositionEmpty(targetX, targetY);
        Destroy(target.gameObject);
        Debug.Log($"{target.name} destroyed");
    }
    else
    {
        // Perbarui posisi target jika target masih hidup
        controller.GetComponent<Game>().SetPosition(target.gameObject, targetX, targetY);
    }

    // Tunggu selama 1 detik (atau sesuai kebutuhan animasi)
    Debug.Log("Waiting for 1 second.");
    yield return new WaitForSeconds(1.0f);

    // Hancurkan move plates untuk mencegah konflik
    DestroyMovePlates();

    // Kembali ke posisi awal
    MoveTo(originalX, originalY);
    controller.GetComponent<Game>().SetPosition(gameObject, originalX, originalY);

    // Pindahkan giliran ke AI setelah serangan
    controller.GetComponent<Game>().gameManager.NextTurn();
}


}
    