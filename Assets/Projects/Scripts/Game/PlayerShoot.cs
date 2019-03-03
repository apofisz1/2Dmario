using UnityEngine;

public class PlayerShoot : MonoBehaviour {
    [SerializeField] private GameObject _arrowPrefab;

    private InputState _inputState;
    private PlayerInventory _playerInventory;

    private void Awake() {
        _inputState = GetComponent<InputState>();
        _playerInventory = GetComponent<PlayerInventory>();
    }

    private void Update() {
        // ha van nyíl, és a felhasználó lenyomta a space gombot, akkor létrehozunk egy új nyilat a játékos pozíciójában)
        if (_inputState.IsSpacePressed && _playerInventory.ArrowCount > 0) {
            _playerInventory.ArrowCount--;
            Instantiate(_arrowPrefab, transform.position, _arrowPrefab.transform.rotation);
        }
    }
}
