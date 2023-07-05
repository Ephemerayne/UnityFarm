using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {

        // Чтобы GameManager не пересозавался при возврате/телепорте на ту же локу
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            return;
        }

        instance = this;
        /*SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoaded;*/
    }

    // References
    public Player player;
    public Cat cat;
}

