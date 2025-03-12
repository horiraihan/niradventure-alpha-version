using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    // Beberapa fungsi akan membutuhkan referensi ke controller
    public GameObject controller;
    public GameObject reference = null;  // Referensi ke bidak catur yang memanggil MovePlate
    public GameObject explosionEffectPrefab;

    // Lokasi pada papan catur
    int matrixX;
    int matrixY;

    // false: movement, true: attacking
    public bool attack = false;

    public void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        
        if (controller != null)
        {
            Game sc = controller.GetComponent<Game>(); // Tambahkan deklarasi variabel sc

            if (attack)
            {
                // Ubah warna tergantung mode
                if (sc.IsMagicAttackMode)
                {
                    gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 1.0f, 0.5f); // Biru
                }
                else
                {
                    gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 0.5f); // Merah
                }
            }
        }
        else
        {
            Debug.LogError("GameController tidak ditemukan.");
        }
    }

    void Awake()
    {
        // Inisialisasi controller
        if (controller == null)
        {
            controller = GameObject.FindGameObjectWithTag("GameController");
            if (controller == null)
            {
                Debug.LogError("GameController tidak ditemukan. Pastikan telah ditandai dengan benar.");
            }
        }
    }

public void OnMouseUp()
{
    if (controller == null)
    {
        Debug.LogError("Game controller not set!");
        return;
    }

    Game sc = controller.GetComponent<Game>();
    ChessGameManager gameManager = FindObjectOfType<ChessGameManager>();

    if (gameManager == null)
    {
        Debug.LogError("ChessGameManager not set!");
        return;
    }

    if (attack && reference != null)
    {
        Chessman attacker = reference.GetComponent<Chessman>();
        Chessman target = sc.GetPosition(matrixX, matrixY)?.GetComponent<Chessman>();

        if (target != null && attacker.player != target.player)
        {
            if (sc.IsMagicAttackMode)
            {
                attacker.StartCoroutine(attacker.PerformMagicAttack(target));
            }
            else
            {
                attacker.Attack(target);
            }

            sc.DisableAttackMode();
            sc.DisableMagicAttackMode();
        }
    }
    else if (reference != null)
    {
        Chessman cm = reference.GetComponent<Chessman>();
        if (cm != null)
        {
            sc.MovePiece(reference, matrixX, matrixY);
            gameManager.PlayerMoveCompleted();
            cm.DestroyMovePlates();
        }
    }
}


    void OnMouseDown()
    {
        ChessGameManager gameManager = FindObjectOfType<ChessGameManager>();
        if (gameManager != null && gameManager.GetCurrentPlayer() != "player")
        {
            Debug.LogWarning("It's not the player's turn!");
            return;
        }

        // Lanjutkan logika pemain...
    }

    private void InstantiateExplosion(int x, int y)
    {
        if (explosionEffectPrefab != null)
        {
            GameObject explosionEffect = Instantiate(explosionEffectPrefab, new Vector3(x, y, -1), Quaternion.identity);
            Animator explosionAnimator = explosionEffect.GetComponent<Animator>();
            if (explosionAnimator != null && explosionAnimator.runtimeAnimatorController != null)
            {
                explosionAnimator.SetTrigger("Explode");
            }
            else
            {
                Debug.LogError("Animator untuk ledakan tidak ditemukan atau tidak memiliki AnimatorController.");
            }
        }
        else
        {
            Debug.LogError("Referensi ke prefab ledakan tidak ditemukan.");
        }
    }

    public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }

    public void SetReference(GameObject obj)
    {
        reference = obj;
    }

    public GameObject GetReference()
    {
        return reference;
    }

    public int GetX()
    {
        return matrixX;
    }

    public int GetY()
    {
        return matrixY;
    }

    // Menambahkan metode GetXBoard dan GetYBoard untuk mengembalikan koordinat papan
    public int GetXBoard()
    {
        return matrixX;
    }

    public int GetYBoard()
    {
        return matrixY;
    }
}
