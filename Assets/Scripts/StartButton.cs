using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void Begin()
    {
        gameManager.isGameActive = true;
        gameManager.StartGame();
    }

    public void TakeAWalk()
    {
        gameManager.Walkabout();
    }

    public void End() 
    {
        SceneManager.LoadScene(0);
        TheWallGameManager.Instance.Reload();  
    }
}

