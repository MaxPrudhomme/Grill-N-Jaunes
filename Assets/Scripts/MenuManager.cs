using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuManager : MonoBehaviour
{
    private UIDocument menu;
    private Button startButton;
    private Button quitButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menu = GetComponent<UIDocument>();
        startButton = menu.rootVisualElement.Q<Button>("StartButton");
        if (startButton == null) Debug.Log("MAIS OU ETRE STARTBUTTON");

        quitButton = menu.rootVisualElement.Q<Button>("QuitButton");
        if (quitButton == null) Debug.Log("MAIS OU ETRE EXITBUTTON");

        startButton.clicked += OnStartBtnClick;

        quitButton.clicked += OnExitBtnClick;

    }


    // Update is called once per frame
    void Update()
    {
        
    }

    void OnStartBtnClick()
    {
        Debug.Log("START");
        SceneManager.LoadScene("BlockOut");
    }

    void OnExitBtnClick()
    {
        Debug.Log("EXIT");
        
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;

    }
}
