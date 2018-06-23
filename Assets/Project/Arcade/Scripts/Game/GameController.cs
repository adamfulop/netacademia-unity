using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public GameObject PlayerPrefab;
    public Text StartText;
    public ScoreText ScoreText;
    public ScoreText BestScoreText;

    private GameObject _player;
    private Spawner _spawner;
    private PixelPerfectCamera _pixelPerfectCamera;
    private TimeController _timeController;
    
    // restarthoz
    private bool _gameStarted;
    
    // villogáshoz
    private bool _blink;
    private int _blinkTime;
    
    // scorehoz
    private float _elapsedTime;
    private float _bestTime;

    private void Awake() {
        _pixelPerfectCamera = FindObjectOfType<PixelPerfectCamera>();
        _spawner = FindObjectOfType<Spawner>();
        _timeController = GetComponent<TimeController>();
    }

    private void Start() {
        _spawner.Active = false;

        // eleinte ehelyett: ResetGame()
        _gameStarted = false;
        Time.timeScale = 0;

        BestScoreText.ScoreSeconds = PlayerPrefs.GetFloat("BestTime");
    }

    private void Update() {
        if (!_gameStarted && Time.timeScale == 0) {
            if (Input.anyKeyDown) {
                _timeController.ManipulateTime(1, 1f);
                ResetGame();
            }
        }

        if (!_gameStarted) {
            _blinkTime++;

            if (_blinkTime % 50 == 0) {
                _blink = !_blink;
                StartText.canvasRenderer.SetAlpha(_blink ? 0 : 1);
            }
        } else {
            _elapsedTime += Time.deltaTime;
            ScoreText.ScoreSeconds = _elapsedTime;
        }
    }

    private void ResetGame() {
        _spawner.Active = true;
        _player = Instantiate(PlayerPrefab, new Vector3 {y = Screen.height / 2f / _pixelPerfectCamera.PixelPerUnit}, Quaternion.identity);

        _player.GetComponent<DestroyOffscreen>().DestroyCallback += OnPlayerKilled;
        _gameStarted = true;
        
        StartText.canvasRenderer.SetAlpha(0);

        _elapsedTime = 0;
    }

    private void OnPlayerKilled() {
        _spawner.Active = false;
        _player.GetComponent<DestroyOffscreen>().DestroyCallback -= OnPlayerKilled;
        Destroy(_player);
        _timeController.ManipulateTime(0, 5.5f);
        _gameStarted = false;

        if (_elapsedTime > _bestTime) {
            _bestTime = _elapsedTime;
            PlayerPrefs.SetFloat("BestTime", _bestTime);
            BestScoreText.ScoreSeconds = _bestTime;
        }
    }
}