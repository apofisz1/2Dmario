using System;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreView : MonoBehaviour {
    [SerializeField] private Text _nameText;
    [SerializeField] private Text _scoreText;

    public HighScoreRecord HighScoreRecord {
        set {
            // név és pontszám Text beállítása
            _nameText.text = value.PlayerName;
            _scoreText.text = FormatTime(value.Score);
        }
    }
    
    private static string FormatTime(float seconds) {
        var timeSpan = TimeSpan.FromSeconds(seconds);
        return string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
    }
}
