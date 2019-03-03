using System.Collections;
using UnityEngine;

public class TimeController : MonoBehaviour {
    // duration idő alatt newTime-ra állítja a timeScale-t
    public void ManipulateTime(float newTime, float duration) {
        if (Time.timeScale == 0) Time.timeScale = 0.1f;

        // Coroutine-ként hívjuk meg a függvényt, a különleges vezérlést lehetővé téve
        StartCoroutine(FadeTo(newTime, duration));
    }

    private static IEnumerator FadeTo(float newTime, float duration) {
        for (var t = 0f; t < 1; t += Time.deltaTime / duration) {      // 0 és 1 között interpoláció
            Time.timeScale = Mathf.Lerp(Time.timeScale, newTime, t);   // a timeScale-t beállítjuk az interpolált értékre 

            // ha kevesebb mint 0.01 az eltérés a jelenlegi érték és a cél érték között, beállítjuk véglegesre
            if (Mathf.Abs(newTime - Time.timeScale) < 0.01f) {
                Time.timeScale = newTime;
                // befejeztük az iterálást
                yield break;
            }
            
            // eldobjuk a vezérlést, a következő iteráció majd "kicsivel később" fut le
            // (Time.deltaTime-ban megkapjuk az eltelt időt)
            yield return null;
        }
    }
}
