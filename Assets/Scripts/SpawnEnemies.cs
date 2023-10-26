
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    private readonly float spawnRangeX = 10f;
    private readonly float spawnPosZ = 100f;

    public int enemyCount;
    private int lastEnemyCount;
    private int actualWave;
    public int waveNumber;
    public int score;
    private GameManager gameManager;
    bool trip = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        lastEnemyCount = 0;
        score = 0;
        trip = true;
    }
    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount < lastEnemyCount)
        {
            score++;
            gameManager.UpdateScore(score, enemyCount);
        }
        if (enemyCount == 0 && gameManager.playerCamera.enabled)
        {
            waveNumber++;
            Debug.Log(waveNumber);
            actualWave = Mathf.Min(waveNumber*gameManager.walkabout, 6);
            Debug.Log(actualWave);  
            SpawnEnemyWave(actualWave);
            gameManager.UpdateScore(score, actualWave);
        }
        if (!gameManager.isGameActive && trip)
        {
            gameManager.SaveScore(score);
            trip = false;
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            gameManager.GameOver();
        }

        lastEnemyCount = enemyCount; 
    }

        Vector3 GenerateSpawnPos()
        {
            float spawnPosX = Random.Range(-spawnRangeX, spawnRangeX);
            Vector3 randomPos = new Vector3(spawnPosX, 6, spawnPosZ);
            return randomPos;
        }

        void SpawnEnemyWave(int enemiesToSpawn)
        {
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                int enemyIndex = Random.Range(0, enemyPrefabs.Length);
                Instantiate(enemyPrefabs[enemyIndex], GenerateSpawnPos(), enemyPrefabs[enemyIndex].transform.rotation);
            }
        }
    }

