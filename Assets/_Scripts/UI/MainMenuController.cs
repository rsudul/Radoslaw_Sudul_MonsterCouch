using UnityEngine;
using UnityEngine.UI;
using MonsterCouchTest.Gameplay;

namespace MonsterCouchTest.UI
{
    public sealed class MainMenuController : BaseScreen
    {
        private GameController _gameController;

        [Header("Buttons")]
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _exitButton;

        public void Init(GameController game)
        {
            _gameController = game;

            if (_playButton)
            {
                _playButton.onClick.AddListener(() => _gameController.StartPlay());
            }

            if (_settingsButton)
            {
                _settingsButton.onClick.AddListener(() => _gameController.ShowSettings());
            }

            if (_exitButton)
            {
                _exitButton.onClick.AddListener(() => _gameController.ExitGame());
            }
        }
    }
}