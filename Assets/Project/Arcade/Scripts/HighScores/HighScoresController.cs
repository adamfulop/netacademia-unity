using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScoresController : MonoBehaviour {
    public void OnBackClick() {
        SceneManager.LoadSceneAsync("Main Menu");
    }
    
    public List<HighScoreRecord> HighScores;

    private void Start() {
        LoadHighScores();
    }

    public void AddHighScore(string playerName, int score) {
        var record = new HighScoreRecord {
            PlayerName = playerName,
            Score = score
        };

        HighScores.Add(record);
        HighScores = HighScores
            .OrderByDescending(r => r.Score)
            .Take(10)
            .ToList();
            
        SaveHighScores();
    }

    private void SaveHighScores() {
        var json = JsonConvert.SerializeObject(HighScores, Formatting.None);
        PlayerPrefs.SetString("HighScores", json);
    }

    private void LoadHighScores() {
        var json = PlayerPrefs.GetString("HighScores", null);
        if (!string.IsNullOrEmpty(json)) {
            HighScores = JsonConvert.DeserializeObject<List<HighScoreRecord>>(json);
        } else {
            HighScores = new List<HighScoreRecord>();
        }
    }
}
