using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button quitButton;
    public GameUIManager gameUIManager;
    public InkColorChange resetColor;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
            gameUIManager.CallEASMenu();
        });
        mainMenuButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
            resetColor.ResetColor();
        });
        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
            resetColor.ResetColor();
        });
    }
}
