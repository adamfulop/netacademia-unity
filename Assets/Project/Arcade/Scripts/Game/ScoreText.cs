using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour {
    public string Prefix;
    
    protected Text Text;

    public float ScoreSeconds {
        set { Text.text = string.Format("{0}: {1}", Prefix, FormatTime(value)); }
    }

    public float ScoreValue {
        set { Text.text = string.Format("{0}: {1}", Prefix, value); }
    }

    private void Awake() {
        Text = GetComponent<Text>();
    }

    private static string FormatTime(float seconds) {
        var timeSpan = TimeSpan.FromSeconds(seconds);
        return string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
    }
}
