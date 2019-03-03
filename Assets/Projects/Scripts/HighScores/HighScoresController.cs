using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScoresController : MonoBehaviour {
    [SerializeField] private HighScoreView _highScoreView1;
    [SerializeField] private HighScoreView _highScoreView2;
    [SerializeField] private HighScoreView _highScoreView3;
    
    public void OnBackClick() {
        // visszanavigálunk a főmenübe
        SceneManager.LoadSceneAsync("Main Menu");
    }

    public List<HighScoreRecord> HighScores;

    private void Awake() {
        LoadHighScores();
    }

    private void Start() {       
        // ha van érték és nézet, kiírjuk a rekordokat (top 3)
        if (HighScores.Count > 0 && _highScoreView1 != null) {
            _highScoreView1.HighScoreRecord = HighScores[0];
        }
        
        if (HighScores.Count > 1 && _highScoreView2 != null) {
            _highScoreView2.HighScoreRecord = HighScores[1];
        }
        
        if (HighScores.Count > 2 && _highScoreView3 != null) {
            _highScoreView3.HighScoreRecord = HighScores[2];
        }
    }

    public void AddHighScore(float score, string playerName = "Adam") {
        var record = new HighScoreRecord {
            PlayerName = playerName,
            Score = score
        };
        
        // új eredményt hozzáadjuk a listához, majd a 10 legnagyobb pontszámúból csökkenő sorrendű listát készítünk
        HighScores.Add(record);
        HighScores = HighScores
            .OrderByDescending(r => r.Score)
            .Take(10)
            .ToList();
        
        SaveHighScores();
    }

    private void SaveHighScores() {
        // a listából JSON objektumot készítünk, és elmentjük a PlayerPrefsbe
        var json = JsonConvert.SerializeObject(HighScores, Formatting.None);
        PlayerPrefs.SetString("HighScores", json);
    }

    private void LoadHighScores() {
        // betöltjük a JSON objektumot, és listát készítünk belőle (vagy új listát hozunk létre, ha még nincs mentve)
        var json = PlayerPrefs.GetString("HighScores", null);
        if (string.IsNullOrEmpty(json)) {
            HighScores = new List<HighScoreRecord>();
        } else {
            HighScores = JsonConvert.DeserializeObject<List<HighScoreRecord>>(json);
        }
    }
}