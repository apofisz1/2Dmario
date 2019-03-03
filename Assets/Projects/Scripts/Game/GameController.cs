using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    // a SerializeField attribútumnak köszönhetően a Unityben is láthatóak lesznek a mezők
    // (pontosan úgy, mintha publikusak lennének)
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Text _startText;
    [SerializeField] private ScoreText _scoreText;
    [SerializeField] private ScoreText _bestScoreText;

    private GameObject _player;
    private HighScoresController _highScoresController;
    private PixelPerfectCamera _pixelPerfectCamera;
    private Spawner _spawner;
    private TimeController _timeController;

    private bool _gameStarted;

    private bool _blink;
    private int _blinkTime;

    private float _elapsedTime;
    private float _bestTime;

    private void Awake() {
        _highScoresController = FindObjectOfType<HighScoresController>();
        _pixelPerfectCamera = FindObjectOfType<PixelPerfectCamera>();
        _spawner = FindObjectOfType<Spawner>();
        _timeController = GetComponent<TimeController>();
    }

    private void Start() {
        // kezdetben minden áll
        _spawner.Active = false;
        _gameStarted = false;
        Time.timeScale = 0;
        
        // ha van mentett pontszám, betöltjük
//        _bestTime = PlayerPrefs.GetFloat("BestTime");
        if (_highScoresController.HighScores.Count > 0) {
            _bestTime = _highScoresController.HighScores[0].Score;
        }
        _bestScoreText.ScoreSeconds = _bestTime; 
    }

    private void Update() {
        // ha minden áll, akkor bármilyen gomb megnyomására indul a játék
        if (!_gameStarted && Time.timeScale == 0) {
            if (Input.anyKeyDown) {
                _timeController.ManipulateTime(1, 1f);
                ResetGame();
            }
        }

        // ha áll a játék, akkor a _blinkTime számláló Update hívásonkénti növelésével mérjük az időt a start felirat villogásához
        if (!_gameStarted) {
            _blinkTime++;

            if (_blinkTime % 50 == 0) {    // minden 50. értéknél váltjuk a szöveg láthatóságát
                _blink = !_blink;
                _startText.canvasRenderer.SetAlpha(_blink ? 0 : 1);
            }
        } else {
            // ha megy a játék, mérjük a pontszámot (azaz az eltelt időt)
            _elapsedTime += Time.deltaTime;
            _scoreText.ScoreSeconds = _elapsedTime;
        }
    }

    private void ResetGame() {
        _spawner.Active = true;
        // a játékos a képernyő tetejéről esik a pályára
        var spawnPosition = new Vector3 { y = Screen.height / 2f / _pixelPerfectCamera.PixelPerUnit };
        _player = Instantiate(_playerPrefab, spawnPosition, Quaternion.identity);

        _player.GetComponent<DestroyOffscreen>().DestroyCallback += OnPlayerKilled;

        _gameStarted = true;
        // start szöveg láthatatlan
        _startText.canvasRenderer.SetAlpha(0);

        _elapsedTime = 0;
    }

    private void OnPlayerKilled() {
        Debug.Log("Player killed!");
        _spawner.Active = false;
        // leiratkozás az eseményről, objektum megsemmisítése és idő leállítása
        _player.GetComponent<DestroyOffscreen>().DestroyCallback -= OnPlayerKilled;
        Destroy(_player);
        _timeController.ManipulateTime(0, 5.5f);

        _gameStarted = false;

        // ha jobb a pontszám, mint az eddigi legjobb, akkor elmentjük
        if (_elapsedTime > _bestTime) {
            _bestTime = _elapsedTime;
//            PlayerPrefs.SetFloat("BestTime", _bestTime);
            _highScoresController.AddHighScore(_bestTime);
            _bestScoreText.ScoreSeconds = _bestTime;
        }
    }
}
