
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI enemiesText;
    public TextMeshProUGUI gameOverText;
    public Text walkaboutText;
    public RawImage exitVideo;
    public Button exitGameButton;
    public Button startGameButton;
    public Button walkaboutButton;
    public GameObject player;
    public Camera startEndCamera;
    public Camera playerCamera;
    public GameObject bow;
    public bool isGameActive;
    public GameObject titleScreen;
    public int walkabout = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera.gameObject.SetActive(false);
        playerCamera.enabled = false;
        exitVideo.gameObject.SetActive(false);
    }

    public void UpdateScore(int score, int liveEnemies)
    {
        scoreText.text = "Score:" + score;
        enemiesText.text = "Enemies Seen Approaching:" + liveEnemies;
    }

    public void SaveScore(int score) 
    {
        TheWallGameManager.Instance.AddSession(score);
        TheWallGameManager.Instance.SaveNames();
    }
    // Here is for the game is over and you lost for whatever reason
    public void GameOver()
    {
        isGameActive = false;
        exitVideo.gameObject.SetActive(true);
        exitVideo.enabled = true;

        gameOverText.gameObject.SetActive(true);
        walkaboutText.gameObject.SetActive(false);

        player.SetActive(false);
        playerCamera.gameObject.SetActive(false);
        playerCamera.enabled = false;

        startEndCamera.gameObject.SetActive(true);
        startEndCamera.enabled = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        enemiesText.gameObject.SetActive(false);
        exitGameButton.gameObject.SetActive(true);
        startGameButton.gameObject.SetActive(true);
    }

    public void GameTransition_1()
    {
        isGameActive = true;
        exitVideo.gameObject.SetActive(false);
        titleScreen.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        walkaboutText.gameObject.SetActive(true);
        exitGameButton.gameObject.SetActive(false);
        startGameButton.gameObject.SetActive(false);
        walkaboutButton.gameObject.SetActive(false);
        walkabout = 0;

        Cursor.lockState = CursorLockMode.Locked;

        startEndCamera.gameObject.SetActive(false);
        startEndCamera.enabled = false;

        player.SetActive(true);
        playerCamera.gameObject.SetActive(true);
        bow.SetActive(false);
        playerCamera.enabled = true;
    }
    public void StartGame()        
    {
        titleScreen.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        walkaboutText.gameObject.SetActive(true);
        exitGameButton.gameObject.SetActive(false);
        startGameButton.gameObject.SetActive(false);
        walkaboutButton.gameObject.SetActive(false);
        walkabout = 1;

        UpdateScore(0, 2);

        Cursor.lockState = CursorLockMode.Locked;

        startEndCamera.gameObject.SetActive(false);
        startEndCamera.enabled=false;   

        player.SetActive(true);
        playerCamera.gameObject.SetActive(true);
        playerCamera.enabled = true;

        scoreText.gameObject.SetActive(true);
        enemiesText.gameObject.SetActive(true);
    }

    public void Walkabout()
    {
        isGameActive = true;
        titleScreen.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        walkaboutText.gameObject.SetActive(true);
        exitGameButton.gameObject.SetActive(false);
        startGameButton.gameObject.SetActive(false);
        walkaboutButton.gameObject.SetActive(false);
        walkabout = 0;

        Cursor.lockState = CursorLockMode.Locked;

        startEndCamera.gameObject.SetActive(false);
        startEndCamera.enabled = false;

        player.SetActive(true);
        playerCamera.gameObject.SetActive(true);
        bow.SetActive(false);
        playerCamera.enabled = true;
    }
} 
