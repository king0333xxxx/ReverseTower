using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject mainBuilding; // drag bangunan utama
    public int currentLevelIndex = 1; // set dari inspector atau otomatis

    private bool gameEnded = false;

    void Update()
    {
        if (!gameEnded && mainBuilding == null) // Kalau object sudah dihancurkan
        {
            gameEnded = true;
            WinLevel();
        }
    }

    void WinLevel()
    {
        Debug.Log("Level Complete!");

        int unlockedLevel = PlayerPrefs.GetInt("unlockedLevel", 1);

        if (currentLevelIndex >= unlockedLevel)
        {
            PlayerPrefs.SetInt("unlockedLevel", currentLevelIndex + 1);
            PlayerPrefs.Save();
        }

        // Optional: lanjut ke map scene
        SceneManager.LoadScene("MapScene");
    }
}
