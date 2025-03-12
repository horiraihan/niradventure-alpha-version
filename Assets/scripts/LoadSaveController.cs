using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSaveController : MonoBehaviour
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

}
