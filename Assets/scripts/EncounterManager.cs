using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EncounterManager : MonoBehaviour
{
    public float encounterRate = 5f; // Waktu antar encounter dalam detik
    private float encounterTimer;
    private Vector3 lastPlayerPosition; // Untuk menyimpan posisi terakhir pemain saat encounter dimulai
    private bool isPlayerInSafeZone = false;
    private bool isInEncounter = false; // Flag untuk menandai jika pemain sedang dalam encounter

    private static EncounterManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Membuat EncounterManager tidak dihancurkan saat mengganti scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (!GameManager.isPaused && !isPlayerInSafeZone && !isInEncounter)
        {
            encounterTimer += Time.deltaTime;

            if (encounterTimer >= encounterRate)
            {
                StartRandomEncounter();
                encounterTimer = 0f; // Reset timer setelah encounter
            }
        }
    }

    public void StartRandomEncounter()
    {
        isInEncounter = true; // Tandai bahwa pemain sedang dalam encounter
        PlayerControl player = FindObjectOfType<PlayerControl>();
        if (player != null)
        {
            lastPlayerPosition = player.transform.position;
            PlayerPrefs.SetFloat("PlayerX", lastPlayerPosition.x);
            PlayerPrefs.SetFloat("PlayerY", lastPlayerPosition.y);
            PlayerPrefs.SetFloat("PlayerZ", lastPlayerPosition.z);
        }
        else
        {
            Debug.LogWarning("PlayerControl not found!");
        }

        Debug.Log("Random Encounter triggered!");
        SceneManager.LoadScene("ArenaScene"); // Ganti "BattleScene" dengan nama scene pertempuran Anda
    }

    public void EndEncounter()
    {
        isInEncounter = false; // Tandai bahwa encounter telah selesai
        SceneManager.LoadScene("GamePlay"); // Ganti "MainScene" dengan nama scene utama Anda yang baru
        StartCoroutine(RestorePlayerPosition());
    }

    private IEnumerator RestorePlayerPosition()
    {
        yield return new WaitForSeconds(0.1f); // Beri waktu sedikit untuk scene main dimuat

        PlayerControl player = FindObjectOfType<PlayerControl>();
        if (player != null)
        {
            float x = PlayerPrefs.GetFloat("PlayerX");
            float y = PlayerPrefs.GetFloat("PlayerY");
            float z = PlayerPrefs.GetFloat("PlayerZ");
            player.transform.position = new Vector3(x, y, z);
        }
        else
        {
            Debug.LogWarning("PlayerControl not found!");
        }
    }

    public void SetPlayerInSafeZone(bool isInSafeZone)
    {
        isPlayerInSafeZone = isInSafeZone;
    }

    public static EncounterManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<EncounterManager>();
            }
            return instance;
        }
    }
}
