using UnityEngine;
using UnityEngine.EventSystems;
using MonsterCouchTest.Gameplay.Enemy;
using MonsterCouchTest.UI;

namespace MonsterCouchTest.Gameplay
{
    public sealed class GameController : MonoBehaviour
    {
        public enum GameState { MainMenu, Settings, Play }

        [Header("Screens")]
        [SerializeField] private MainMenuController _mainMenu;
        [SerializeField] private SettingsController _settings;

        [Header("Gameplay")]
        [SerializeField] private EnemySpawner _spawner;

        private GameState _state;

        private void Awake()
        {
            _mainMenu?.Init(this);
            _settings?.Init(this);
        }

        private void Start()
        {
            ShowMainMenu();
        }

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
            {
                switch (_state)
                {
                    case GameState.Settings:
                        ShowMainMenu();
                        break;
                    case GameState.Play:
                        ShowMainMenu();
                        _spawner?.EndPlay();
                        break;
                }
            }
        }

        public void StartPlay()
        {
            _state = GameState.Play;
            _mainMenu?.Hide();
            _settings?.Hide();

            _spawner?.BeginPlay();
            ClearSelection();
        }

        public void ShowSettings()
        {
            _state = GameState.Settings;
            _mainMenu?.Hide();
            _settings?.Show();
            Focus(_settings);
        }

        public void ShowMainMenu()
        {
            _state = GameState.MainMenu;
            _settings?.Hide();
            _mainMenu?.Show();
            Focus(_mainMenu);
        }

        public void ExitGame()
        {
#if UNITY_EDITOR
            Debug.Log("Quit requested (Editor)");
#else
            Application.Quit();
#endif
        }

        private static void Focus(BaseScreen screen)
        {
            if (screen?.FirstSelectable == null)
            {
                return;
            }

            EventSystem.current?.SetSelectedGameObject(screen.FirstSelectable.gameObject);
        }

        private static void ClearSelection()
        {
            EventSystem.current?.SetSelectedGameObject(null);
        }
    }
}