using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameOverManager : MonoBehaviour
{
    private UIDocument menu;
    private Button restartButton;
    private Button quitButton;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menu = GetComponent<UIDocument>();
        restartButton = menu.rootVisualElement.Q<Button>("StartButton");
        if (restartButton == null) Debug.Log("MAIS OU ETRE RESTARTBUTTON");

        quitButton = menu.rootVisualElement.Q<Button>("QuitButton");
        if (quitButton == null) Debug.Log("MAIS OU ETRE EXITBUTTON");

        restartButton.clicked += OnStartBtnClick;

        quitButton.clicked += OnExitBtnClick;

    }


    // Update is called once per frame
    void Update()
    {
        
    }

    void OnStartBtnClick()
    {
        Debug.Log("START");
        SceneManager.LoadScene("Menu");
    }

    void OnExitBtnClick()
    {
        Debug.Log("EXIT");

        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();

    }
}
