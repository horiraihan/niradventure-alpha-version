// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// [System.Serializable]
// public class GameData
// {
//     public int playerLevel;
//     public float[] position;
//     // Tambahkan data yang ingin disimpan
// }

// public class SaveLoadManager : MonoBehaviour
// {
//     public void SaveGame(int slot)
//     {
//         GameData data = new GameData();
//         data.playerLevel = /* Level pemain */;
//         data.position = new float[3] { player.transform.position.x, player.transform.position.y, player.transform.position.z };

//         string json = JsonUtility.ToJson(data);
//         PlayerPrefs.SetString("SaveSlot" + slot, json);
//         PlayerPrefs.Save();
//     }

//     public void LoadGame(int slot)
//     {
//         string json = PlayerPrefs.GetString("SaveSlot" + slot);
//         if (!string.IsNullOrEmpty(json))
//         {
//             GameData data = JsonUtility.FromJson<GameData>(json);
//             /* Terapkan data ke pemain */
//         }
//         else
//         {
//             Debug.LogWarning("Slot tidak ditemukan!");
//         }
//     }
// }
