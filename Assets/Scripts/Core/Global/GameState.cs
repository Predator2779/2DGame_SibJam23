using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts.Core.Global
{
    public class GameState : MonoBehaviour
    {
        public GameStates CurrentState { get; private set; }
        public static GameState instance;
        
        [SerializeField] private GameObject _menuPanel;
        [SerializeField] private GameObject _pausePanel;
        [SerializeField] private GameObject _levelPrefab;
        
        private GameObject _currentLevel;
        
        void Start()
        {
            if (instance == null) instance = this;
            else if (instance == this) Destroy(gameObject);
            
            EventHandler.OnPlayerDeath.AddListener(DestroyLevel);
        }

        private void Update()
        {
            if (InputFunctions.GetEscapeButton_Up()) ChangeMode();
        }

        private void ChangeMode()
        {
            if (CurrentState == GameStates.Playing) Pause();
            else Play();
        }

        public void Pause()
        {
            _pausePanel.SetActive(true);
            Time.timeScale = 0;

            CurrentState = GameStates.Paused;
        }

        public void Play()
        {
            _pausePanel.SetActive(false);
            Time.timeScale = 1;
        
            CurrentState = GameStates.Playing;
        }

        public void LoadLevel()
        {
            DestroyLevel();
            
            _currentLevel = Instantiate(_levelPrefab);
            _menuPanel.SetActive(false);
        }
        
        public void DestroyLevel()
        {
            if (_currentLevel != null)
                Destroy(_currentLevel);
            
            _currentLevel = null;
            _menuPanel.SetActive(true);
        }
    }

    public enum GameStates
    {
        Playing,
        Paused
    }
}