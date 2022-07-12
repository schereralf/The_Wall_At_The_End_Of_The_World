
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR 
using UnityEditor;
#endif

public class TheWallGameManager : MonoBehaviour
{
    public static TheWallGameManager Instance { get; set; }

    public List<ScoreData> namesList;
    public int maxScore1=0;
    public string maxScorer1;
    private int maxScore2=0;
    private string maxScorer2;
    private int maxScore3=0;
    private string maxScorer3;
    private int maxScore4=0;
    private string maxScorer4;
    private bool filled;
    private string playerID;
    public TMP_InputField inputField;
    public TextMeshProUGUI Scorer1;
    public TextMeshProUGUI Scorer2;
    public TextMeshProUGUI Scorer3;
    public TextMeshProUGUI Scorer4;

    private void Awake()
    {
        Reload();
    }

    public void Reload()
     {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        namesList = new List<ScoreData>();
        LoadNames();
        ListPlayers();
     }

    public void ListPlayers()
    {
        // check the past scores for highest three using three breathtakingly brutal if statements.....

        foreach (ScoreData item in namesList)
        {
            filled = false;
            if (item.PlayerScore > maxScore1)
            {
                maxScore4 = maxScore3; maxScorer4 = maxScorer3;
                maxScore3 = maxScore2; maxScorer3 = maxScorer2;
                maxScore2 = maxScore1; maxScorer2 = maxScorer1;
                maxScore1 = item.PlayerScore;
                maxScorer1 = item.Player;
                filled = true;
            }
            else if (item.PlayerScore > maxScore2 && !filled)
            {
                maxScore4 = maxScore3; maxScorer4 = maxScorer3;
                maxScore3 = maxScore2; maxScorer3 = maxScorer2;
                maxScore2 = item.PlayerScore;
                maxScorer2 = item.Player;
                filled = true;
            }
            else if (item.PlayerScore > maxScore3 && !filled)
            {
                maxScore4 = maxScore3; maxScorer4 = maxScorer3;
                maxScore3 = item.PlayerScore;
                maxScorer3 = item.Player;
                filled = true;
            }
            else if (item.PlayerScore > maxScore4 && !filled)
            {
                maxScore4 = item.PlayerScore;
                maxScorer4 = item.Player;
                filled = true;
            }
        }

        Debug.Log(maxScore1 + maxScore2 + maxScore3 + maxScore4);

        //Here we still need to List highest top scores and names on the glory board
        Scorer1.text = $"{maxScorer1} : {maxScore1}";
        Scorer2.text = $"{maxScorer2} : {maxScore2}";
        Scorer3.text = $"{maxScorer3} : {maxScore3}";
        Scorer4.text = $"{maxScorer4} : {maxScore4}";
    }
public void AddNewPlayer()
    {
        //Load player name from start menu input field, load past data from json repository.
        playerID = inputField.text;
    }

    public void AddSession(int points)
    {
        namesList.Add(new ScoreData { PlayerScore = points, Player = playerID });
    }
    // Begin links to Start button
    public void Begin()
    {
        SceneManager.LoadScene(1);
    }
    // Exit links to Exit process
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }

    // Serializable section.  Key is that the struct for the player+score data and its two variables are serializable,
    // otherwise a list of such structs converts to {} when we use JsonUtility

    [System.Serializable]

    public struct ScoreData
    {
        public int PlayerScore;
        public string Player;
    }
    public class Savedata
    {
        public List<ScoreData> savedList;
    }

    public void LoadNames()
    {
        string path = Application.persistentDataPath + "/savethewallfile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Savedata data = JsonUtility.FromJson<Savedata>(json);
            if (data.savedList != null) namesList = data.savedList;
        }
    }

    public void SaveNames()
    {
        Savedata data = new Savedata
        {
            savedList = Instance.namesList
        };

        string json = JsonUtility.ToJson(data);
        Debug.Log(Application.persistentDataPath);

        // Saving json copy of savedList in generic directory
        File.WriteAllText(Application.persistentDataPath + "/savethewallfile.json", json);
    }
}

