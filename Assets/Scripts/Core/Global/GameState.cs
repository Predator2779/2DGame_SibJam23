using UnityEngine;

namespace Scripts.Core.Global
{
    public class GameState : MonoBehaviour
    {
        public static GameState instance;
        public GameStates State { get => _state; private set => _state = value; }

        [SerializeField] private GameStates _state;
        [SerializeField] private GameObject _menuPanel;
        [SerializeField] private GameObject _pausePanel;
        [SerializeField] private GameObject _levelPrefab;
        [SerializeField] private GameObject _currentLevel;
        [SerializeField] private CutsceneSwitcher _cutsceneSwitcher;

        void Start()
        {
            if (instance == null) instance = this;
            else if (instance == this) Destroy(gameObject);

            EventHandler.OnPlayerDeath.AddListener(DestroyLevel);
            EventHandler.OnLevelDestroy.AddListener(DestroyLevel);

            State = GameStates.Playing;
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape)) ChangeMode();
        }

        private void ChangeMode()
        {
            if (State == GameStates.Playing) Pause();
            else Play();
        }

        public void Pause()
        {
            _pausePanel.SetActive(true);
            Time.timeScale = 0;

            State = GameStates.Paused;
        }

        public void Play()
        {
            _pausePanel.SetActive(false);
            Time.timeScale = 1;

            State = GameStates.Playing;
        }

        public void LoadLevel()
        {
            DestroyLevel();

            _currentLevel = Instantiate(_levelPrefab);
            _menuPanel.SetActive(false);

            Play();
        }

        public void DestroyLevel()
        {
            if (_currentLevel != null) Destroy(_currentLevel);

            _currentLevel = null;
            _menuPanel.SetActive(true);
            _pausePanel.SetActive(false);
        }
    }

    public enum GameStates
    {
        Playing,
        Paused
    }
}