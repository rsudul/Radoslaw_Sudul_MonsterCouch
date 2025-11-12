using UnityEngine;
using UnityEngine.UI;
using MonsterCouchTest.Gameplay;

namespace MonsterCouchTest.UI
{
    public sealed class SettingsController : BaseScreen
    {
        private GameController _gameController;

        [Header("Settings")]
        [SerializeField] private Toggle _checkbox1;
        [SerializeField] private Toggle _checkbox2;

        [Header("Buttons")]
        [SerializeField] private Button _backButton;

        public void Init(GameController game)
        {
            _gameController = game;
            if (_backButton)
            {
                _backButton.onClick.AddListener(() => _gameController.ShowMainMenu());
            }
        }
    }
}