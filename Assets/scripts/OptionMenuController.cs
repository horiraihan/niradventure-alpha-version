using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class OptionMenuController : MonoBehaviour
{
    public GameObject optionsPanel;
    public GameObject transparentPanel;

    public void ToggleOptionsMenu()
    {
        bool isActive = !optionsPanel.activeSelf;
        optionsPanel.SetActive(isActive);
        transparentPanel.SetActive(isActive);

        GameManager.isPaused = isActive;
        Time.timeScale = isActive ? 0 : 1;
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1; // Pastikan waktu berjalan normal
        SceneManager.LoadScene("Menu"); // Ganti "MainMenu" dengan nama scene menu utama Anda
    }

}
