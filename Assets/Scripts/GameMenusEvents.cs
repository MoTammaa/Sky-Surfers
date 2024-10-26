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
        if (_pauseMenuDocument != null)
        {
            _resume = _pauseMenuDocument.rootVisualElement.Q<Button>("resume");
            _pauseresart = _pauseMenuDocument.rootVisualElement.Q<Button>("restart");
            _pausebackToMenu = _pauseMenuDocument.rootVisualElement.Q<Button>("quit");
        }
        if (_gameoverMenuDocument != null)
        {
            _gameoverrestart = _gameoverMenuDocument.rootVisualElement.Q<Button>("restart");
            _gameoverbackToMenu = _gameoverMenuDocument.rootVisualElement.Q<Button>("quit");
        }

        if (_resume != null)
            _resume.clicked += () => { ResumeGame(); };
        if (_pauseresart != null)
            _pauseresart.clicked += () => { RestartGame(); };
        if (_pausebackToMenu != null)
            _pausebackToMenu.clicked += () => { BackToMenu(); };

        if (_gameoverrestart != null)
            _gameoverrestart.clicked += () => { RestartGame(); };
        if (_gameoverbackToMenu != null)
            _gameoverbackToMenu.clicked += () => { BackToMenu(); };

        if (_pauseMenuDocument != null)
            _pauseMenuDocument.rootVisualElement.style.display = DisplayStyle.None;

        VisualElement container = _pauseMenuDocument.rootVisualElement.Q<VisualElement>("Panel");
        container.style.flexDirection = FlexDirection.Column;
        container.style.flexGrow = 0;
        container.style.flexShrink = 0;
        container.style.paddingTop = 10;
        container.style.marginTop = 5;

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
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // check for pause by ESC key
        if (Input.GetKeyDown(KeyCode.Escape))
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
    }
}
