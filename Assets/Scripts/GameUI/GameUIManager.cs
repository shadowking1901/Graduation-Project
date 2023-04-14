using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameUIManager : MonoBehaviour
{
    public RectTransform userDefinedMenuUI;
    public GameObject EASMenuUI;
    public GameObject player;

    public static bool gameIsPaused = false;
    private bool Menu1 = false, Menu2 = false;

    private void Start()
    {
        EASMenuUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !Menu2)
        {
            CallUserDefinedMenu();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !Menu1)
        {
            CallEASMenu();
        }
    }

    public void CallUserDefinedMenu()
    {
        if (gameIsPaused)
        {
            Resume();
            userDefinedMenuUI.DOAnchorPosX(-500, .5f);
            Menu1 = false;
        }
        else
        {
            userDefinedMenuUI.DOAnchorPosX(0, 1f).OnComplete(()=> Pause());
            Menu1 = true;
        }
    }

    public void CallEASMenu()
    {
        if (gameIsPaused)
        {
            Resume();
            EASMenuUI.SetActive(false);
            Menu2 = false;
        }
        else
        {
            Pause();
            EASMenuUI.SetActive(true);
            Menu2 = true;
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        gameIsPaused = true;

        player.GetComponent<AimStateManager>().enabled = false;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;

        player.GetComponent<AimStateManager>().enabled = true;
    }
}
