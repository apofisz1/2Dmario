using UnityEngine;

public class Jump : MonoBehaviour {
    public float JumpSpeed;    // ugrás sebessége

    private InputState _inputState;
    private Rigidbody2D _rigidbody2D;

    private void Awake() {
        _inputState = GetComponent<InputState>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        // ha a játékos a földön áll, és valamilyen gomb le van nyomva -> ugrás
        if (_inputState.IsActionButtonPressed && _inputState.IsStanding) {
            _rigidbody2D.velocity = new Vector2(0, JumpSpeed);
        }
    }
}
