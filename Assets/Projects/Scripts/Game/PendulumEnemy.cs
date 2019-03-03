using UnityEngine;

public class PendulumEnemy : MonoBehaviour {
    private void OnEnable() {
        transform.Translate(0, 128, 0);    // spawnerhez képest megfelelő magasság beállítása
    }
}
