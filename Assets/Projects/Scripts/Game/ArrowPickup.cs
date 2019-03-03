using Lean.Pool;
using UnityEngine;

public class ArrowPickup : MonoBehaviour {
    [SerializeField] private float _heightOffset;

    private void OnEnable() {
        transform.Translate(0, _heightOffset + 128, 0);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // a játékos felveszi a nyilat
        if (other.CompareTag("Player")) {
            LeanPool.Despawn(gameObject);    // a GameObject megsemmisítése
            var inventory = other.GetComponent<PlayerInventory>();
            inventory.ArrowCount++;    // játékos nyilainak számának növelése
        }
    }
}
