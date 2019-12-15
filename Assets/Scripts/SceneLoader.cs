using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{


    private static SceneLoader instance;
    public static SceneLoader Instance
    {
        get { return instance; }
        private set { instance = value; }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Screen.fullScreen = false;
        } else
        {
            Destroy(gameObject);
        }
    }

    public static void LoadScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
