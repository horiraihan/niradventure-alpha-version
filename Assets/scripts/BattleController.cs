using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleController : MonoBehaviour
{
    public void ExitBattle()
    {
        EncounterManager.Instance.EndEncounter();
    }
}
