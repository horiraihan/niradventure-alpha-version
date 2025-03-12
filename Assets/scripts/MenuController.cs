using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void LoadGame()
    {
        // Tambahkan kode untuk memuat game di sini
    }

    public void OpenOptions()
    {
        // Tambahkan kode untuk membuka menu opsi di sini
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}