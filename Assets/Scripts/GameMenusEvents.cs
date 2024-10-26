using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameMenusEvents : MonoBehaviour
{

    public UIDocument _pauseMenuDocument, _gameoverMenuDocument;
    private Button _resume, _pauseresart, _pausebackToMenu, _gameoverrestart, _gameoverbackToMenu;
    private Label _score, _infoText;

    private void Awake()
    {
        if (_pauseMenuDocument == null || _gameoverMenuDocument == null)
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                if (child.name.ContainsInsensitive("pause"))
                    _pauseMenuDocument = child.GetComponent<UIDocument>();
                else if (child.name.ContainsInsensitive("over"))
                    _gameoverMenuDocument = child.GetComponent<UIDocument>();
            }

        _resume = _pauseMenuDocument.rootVisualElement.Q<Button>("resume");
        _pauseresart = _pauseMenuDocument.rootVisualElement.Q<Button>("restart");
        _pausebackToMenu = _pauseMenuDocument.rootVisualElement.Q<Button>("quit");


        _gameoverrestart = _gameoverMenuDocument.rootVisualElement.Q<Button>("restart");
        _gameoverbackToMenu = _gameoverMenuDocument.rootVisualElement.Q<Button>("quit");
        _score = _gameoverMenuDocument.rootVisualElement.Q<Label>("score");
        _infoText = _gameoverMenuDocument.rootVisualElement.Q<Label>("info");


        _resume.clicked += () => { ResumeGame(); };
        _pauseresart.clicked += () => { RestartGame(); };
        _pausebackToMenu.clicked += () => { BackToMenu(); };

        _gameoverrestart.clicked += () => { RestartGame(); };
        _gameoverbackToMenu.clicked += () => { BackToMenu(); };


        // UI adjustments ///////////////////////////////////////////////////////////////////////////////////////////////
        VisualElement pausePanel = _pauseMenuDocument.rootVisualElement.Q<VisualElement>("Panel"),
            gameoverPanel = _gameoverMenuDocument.rootVisualElement.Q<VisualElement>("Panel"),
            gameoverButtonsPanel = _gameoverMenuDocument.rootVisualElement.Q<VisualElement>("buttons");
        foreach (var panel in new VisualElement[] { pausePanel, gameoverPanel, gameoverButtonsPanel })
        {
            panel.style.flexDirection = panel == gameoverButtonsPanel ? FlexDirection.Row : FlexDirection.Column;
            panel.style.flexGrow = 0;
            panel.style.flexShrink = 0;
            panel.style.paddingTop = panel.style.paddingBottom = panel.style.paddingLeft = panel.style.paddingRight = 10 * (panel == gameoverButtonsPanel ? 5 : 1);
            panel.style.marginTop = panel.style.marginBottom = panel.style.marginLeft = panel.style.marginRight = 5;
        }
        _gameoverrestart.RegisterCallback<GeometryChangedEvent>(evt =>
        {
            _gameoverrestart.style.height = _gameoverrestart.resolvedStyle.width;
        });

        _gameoverbackToMenu.RegisterCallback<GeometryChangedEvent>(evt =>
        {
            _gameoverbackToMenu.style.height = _gameoverbackToMenu.resolvedStyle.width;
        });
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // hide all menus
        _pauseMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
        _gameoverMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
    }

    private void ResumeGame()
    {
        Debug.Log("Game Resumed");
        Time.timeScale = 1;
        // hide pause menu
        _pauseMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
    }

    private void RestartGame()
    {
        Debug.Log("Game Restarted");
        // reload current scene
        GameController.current.game.StartGame();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void BackToMenu()
    {
        Debug.Log("Back to Menu");
        // switch to MainMenuScene
        SceneManager.LoadScene("Menu");
    }


    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        // check for pause by ESC key
        if (Input.GetKeyDown(KeyCode.Escape) && GameController.current.game.CurrentState == Game.GameState.Playing)
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                _pauseMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
            }
            else
            {
                Time.timeScale = 0;
                _pauseMenuDocument.rootVisualElement.style.display = DisplayStyle.Flex;
            }
        }

        // check for gameover
        if (GameController.current.game.CurrentState == Game.GameState.GameOver)
        {
            Time.timeScale = 0;
            _gameoverMenuDocument.rootVisualElement.style.display = DisplayStyle.Flex;
            // show score
            _score.text = "Your Score: " + Math.Round(GameController.current.game.Score, 0);
            _infoText.text = Math.Round(GameController.current.game.fuel, 0) <= 0 ? "You ran out of fuel!" : "";
        }

    }
}
