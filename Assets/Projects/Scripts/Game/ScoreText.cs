using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour {
	public string Prefix;

	private Text _text;

	// másodpercek formázott kiírása
	public float ScoreSeconds {
		set { _text.text = string.Format("{0}: {1}", Prefix, FormatTime(value)); }
	}
	
	public float ScoreValue {
		set { _text.text = string.Format("{0}: {1}", Prefix, value); }
	}

	private void Awake() {
		_text = GetComponent<Text>();
	}
	
	private static string FormatTime(float seconds) {
		var timeSpan = TimeSpan.FromSeconds(seconds);
		return string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
	}
}
