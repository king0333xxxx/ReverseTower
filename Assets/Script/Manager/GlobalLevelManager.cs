using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalLevelManager : MonoBehaviour
{
    public static GlobalLevelManager instance;

    private void Awake()
    {
        // Singleton agar tetap ada di semua scene jika perlu
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Optional: kalau ingin tetap hidup saat ganti scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Reset game dan mulai dari level 1
    /// </summary>
    public void StartNewGame()
    {
        PlayerPrefs.SetInt("unlockedLevel", 1);
        PlayerPrefs.Save();

        // Load Map Selection scene
        SceneManager.LoadScene("MapSelection");
    }

    /// <summary>
    /// Melanjutkan level terakhir yang sudah terbuka
    /// </summary>
    public void ContinueGame()
    {
        int lastUnlockedLevel = GetUnlockedLevel();
        //LoadLevel(lastUnlockedLevel);

        // Load Map Selection scene
        SceneManager.LoadScene("MapSelection");
    }

    /// <summary>
    /// Ambil level terakhir yang terbuka
    /// </summary>
    public int GetUnlockedLevel()
    {
        return PlayerPrefs.GetInt("unlockedLevel", 1);
    }

    /// <summary>
    /// Cek apakah level tertentu sudah terbuka
    /// </summary>
    public bool IsLevelUnlocked(int level)
    {
        return level <= GetUnlockedLevel();
    }

    /// <summary>
    /// Pindah ke scene level tertentu
    /// </summary>
    public void LoadLevel(int level)
    {
        string sceneName = "Level" + level; // Pastikan scene name mengikuti format Level1, Level2, dst
        Debug.Log("Loading " + sceneName);
        SceneManager.LoadScene(sceneName);
    }

    public void OnMouseDown()
    {
        LoadLevel(1);
    }
}
