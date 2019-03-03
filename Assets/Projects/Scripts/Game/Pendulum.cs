using UnityEngine;

public class Pendulum : MonoBehaviour {
    [SerializeField] private GameObject _chain;
    [SerializeField] private GameObject _enemy;

    private Collider2D[] _colliders;
    private FixedJoint2D _fixedJoint2D;

    private Vector3 _startPosition;
    private Quaternion _startRotation;

    private void Awake() {
        _colliders = GetComponents<Collider2D>();
        _fixedJoint2D = GetComponent<FixedJoint2D>();
    }

    // poolból spawnoláskor
    public void OnSpawn() {
        // lánc és ellenség aktiválása
        _chain.SetActive(true);
        _enemy.SetActive(true);

        // ha a joint még engedélyezve van -> első példányosítás -> elmentjük a kezdő pozíciót/orientációt
        if (_fixedJoint2D.enabled) {
            _startPosition = transform.position;
            _startRotation = transform.rotation;
        } else {    // ha a joint nem él -> visszatöltjük az eredeti kiinduló állapotot
            transform.position = _startPosition;
            transform.rotation = _startRotation;
        }

        // engedélyezzük a jointot
        _fixedJoint2D.enabled = true;
        
        // engedélyezzük a collidereket
        foreach (var collider in _colliders) {
            collider.enabled = true;
        }
    }

    // polygon collider (trigger)
    private void OnTriggerEnter2D(Collider2D other) {
        // ha "lelövik" a láncot, akkor a joint inaktív lesz, és a lánc eltűnik
        _chain.SetActive(false);
        _fixedJoint2D.enabled = false;
    }

    // box collider
    private void OnCollisionEnter2D(Collision2D other) {
        // deaktiváljuk az ellenséget, ha beleütközött a rúd
        if (other.gameObject == _enemy) {
            _enemy.SetActive(false);
            foreach (var collider in _colliders) {
                collider.enabled = false;
            }
        }
    }
}
