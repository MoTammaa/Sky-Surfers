using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class MainMenuEvents : MonoBehaviour
{
    public UIDocument _mainMenuDocument, _optionsDocument;
    private Button _start, _options, _quit, _backToMenu;
    private Toggle _soundToggle;

    private void Awake()
    {
        if (_mainMenuDocument == null || _optionsDocument == null)
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                if (child.name.ContainsInsensitive("main"))
                    _mainMenuDocument = child.GetComponent<UIDocument>();
                else if (child.name.ContainsInsensitive("options"))
                    _optionsDocument = child.GetComponent<UIDocument>();

            }
        _start = _mainMenuDocument.rootVisualElement.Q<Button>("start");
        _options = _mainMenuDocument.rootVisualElement.Q<Button>("options");
        _quit = _mainMenuDocument.rootVisualElement.Q<Button>("quit");

        _backToMenu = _optionsDocument.rootVisualElement.Q<Button>("back");
        _soundToggle = _optionsDocument.rootVisualElement.Q<Toggle>("soundtog");


        _start.clicked += () => { StartGame(); };
        _quit.clicked += () => { QuitGame(); };
        _options.clicked += () => { GameOptions(); };

        _backToMenu.clicked += () => { BackToMenu(); };
        _soundToggle.RegisterValueChangedCallback((evt) => { ToggleSound(evt.newValue); });



        _optionsDocument.rootVisualElement.style.display = DisplayStyle.None;

    }

    private void StartGame()
    {
        Debug.Log("Game Started");
        // switch to MainScene
        SceneManager.LoadScene("MainScene");
    }

    private void QuitGame()
    {
        Debug.Log("Game Quit");
        Application.Quit();
    }

    private void GameOptions()
    {
        // show options
        _optionsDocument.rootVisualElement.style.display = DisplayStyle.Flex;
        _mainMenuDocument.rootVisualElement.style.display = DisplayStyle.None;
    }

    private void BackToMenu()
    {
        // show main menu
        _optionsDocument.rootVisualElement.style.display = DisplayStyle.None;
        _mainMenuDocument.rootVisualElement.style.display = DisplayStyle.Flex;
    }

    private void ToggleSound(bool soundOn)
    {
        Debug.Log("Sound Toggled: " + soundOn);
        // toggle sound
        GameController.SoundEnabled = soundOn;
    }




    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
