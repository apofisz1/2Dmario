using UnityEngine;

public class PlayerInventory : MonoBehaviour {
    private ScoreText _arrowText;
    
    [SerializeField] private int _arrowCount;    // játékos nyilainak száma

    private void Awake() {
        _arrowText = GameObject.FindGameObjectWithTag("ArrowText").GetComponent<ScoreText>();
    }

    public int ArrowCount {
        get { return _arrowCount; }
        set {    // beállítja a változó értékét, és a kijelzőt is frissíti
            _arrowCount = value;
            _arrowText.ScoreValue = _arrowCount;
        }
    }
}
