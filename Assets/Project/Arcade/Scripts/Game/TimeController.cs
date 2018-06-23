using System.Collections;
using UnityEngine;

public class TimeController : MonoBehaviour {
    public void ManipulateTime(float newTime, float duration) {
        if (Time.timeScale == 0) Time.timeScale = 0.1f; // 0-nál fixedupdate nem hívódik meg

        StartCoroutine(FadeTo(newTime, duration));
    }

    private static IEnumerator FadeTo(float newTime, float duration) {
        for (var t = 0f; t < 1; t += Time.deltaTime / duration) {
            Time.timeScale = Mathf.Lerp(Time.timeScale, newTime, t);

            if (Mathf.Abs(newTime - Time.timeScale) < 0.01f) {
                Time.timeScale = newTime;
                yield break;
            }
            
            yield return null;
        }
    }
}
