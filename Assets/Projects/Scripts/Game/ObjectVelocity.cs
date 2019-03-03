using UnityEngine;

public class ObjectVelocity : MonoBehaviour {
    public Vector2 Velocity;    // ilyen sebességgel halad az objektum

    private Rigidbody2D _rigidbody2D;

    private void Awake() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        _rigidbody2D.velocity = Velocity;    // beállítjuk a sebességet
    }
}
