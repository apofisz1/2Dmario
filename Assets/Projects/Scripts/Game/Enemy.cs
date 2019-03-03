using Lean.Pool;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField] private float _shootSeconds;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private float _projectileVelocity;
    [SerializeField] private Vector3 _projectileOffset;

    private Animator _animator;
    private SpawnedObjectsContainer _spawnedObjectsContainer;
    private Transform _player;

    private bool _canShoot;

    private void Awake() {
        _animator = GetComponent<Animator>();
        _spawnedObjectsContainer = FindObjectOfType<SpawnedObjectsContainer>();
    }

    public void OnSpawn() {
        // poolból spawnoláskor beállítjuk, hogy balra nézzen a játékos, időzítjük a lövés metódust
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.localScale = new Vector3(-0.3f, 0.3f, 0.3f);
        InvokeRepeating("TryShoot", _shootSeconds, _shootSeconds);
    }

    public void OnDespawn() {
        // lövés metódus időzítésének megszakítása megsemmisítéskor
        if (IsInvoking("TryShoot")) {
            CancelInvoke("TryShoot");
        }
    }

    private void OnDisable() {
        // lövés metódus időzítésének megszakítása megsemmisítéskor
        if (IsInvoking("TryShoot")) {
            CancelInvoke("TryShoot");
        }
    }

    private void TryShoot() {
        if (_player != null) {    // csak akkor lövünk, ha van játékos
            _canShoot = true;
            
            // az összes akadályra megnézzük, nincs-e a játékos és ellenség között
            foreach (var spawnedObject in _spawnedObjectsContainer.SpawnedObjects) {
                if (spawnedObject == transform || spawnedObject.parent == transform) continue;    // az ellenséget önmagára nem ellenőrizzük
                
                if (_player.transform.position.x < transform.position.x) { // ha a játékos balra van az ellenségtől
                    _canShoot = !(spawnedObject.position.x > _player.transform.position.x &&
                                  spawnedObject.position.x < transform.position.x);
                } else { // ha a játékos jobbra van az ellenségtől
                    _canShoot = !(spawnedObject.position.x < _player.transform.position.x &&
                                  spawnedObject.position.x > transform.position.x);
                }

                if (!_canShoot) break;    // ha nem tud lőni, akkor befejezzük az iterálást
            }
            
            if (_canShoot) {
                _animator.SetTrigger("skill_2");    // lövés karakter animáció
                var projectile = LeanPool.Spawn(_projectilePrefab, transform.position + _projectileOffset,    
                    Quaternion.identity); // lövedék létrehozása
                var direction = Mathf.Sign(_player.transform.position.x - transform.position.x); // lövés iránya (előjel)
                projectile.Velocity = new Vector2(    // lövedék sebessége az irány alapján
                    direction * _projectileVelocity - 150f / 2f,
                    0
                );
            }
        }
    }

    private void Update() {
        // mindig a játékos fele nézzen az ellenség
        if (_player != null && _player.position.x > transform.position.x) {
            transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        }
    }
}