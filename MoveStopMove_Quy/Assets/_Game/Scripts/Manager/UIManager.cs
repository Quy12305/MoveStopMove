using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public GameObject mainmenuUI;
    public GameObject settingUI;
    public GameObject finishUI;
    public GameObject loseUI;

    public void OpenMainMenuUI()
    {
        mainmenuUI.SetActive(true);
        settingUI.SetActive(false);
        finishUI.SetActive(false);
        loseUI.SetActive(false);
    }

    public void OpenSettingUI()
    {
        mainmenuUI.SetActive(false);
        settingUI.SetActive(true);
        finishUI.SetActive(false);
        loseUI.SetActive(false);
    }

    public void OpenFinishUI()
    {
        mainmenuUI.SetActive(false);
        settingUI.SetActive(false);
        finishUI.SetActive(true);
        loseUI.SetActive(false);
    }
    public void OpenLoseUI()
    {
        mainmenuUI.SetActive(false);
        settingUI.SetActive(false);
        finishUI.SetActive(false);
        loseUI.SetActive(true);
    }

    public void PlayButton()
    {
        mainmenuUI.SetActive(false);
        LevelManager.Instance.OnStart();
    }

    public void ReplayButton()
    {
        LevelManager.Instance.LoadLevel();
        GameManager.Instance.ChangeState(GameState.MainMenu);
        OpenMainMenuUI();
    }

    public void NextLevelButton()
    {
        LevelManager.Instance.NextLevel();
        GameManager.Instance.ChangeState(GameState.MainMenu);
        OpenMainMenuUI();
    }

    public void SettingButton()
    {
        OpenSettingUI();
        GameManager.Instance.ChangeState(GameState.Setting);
    }

    public void ContinueButton()
    {
        GameManager.Instance.ChangeState(GameState.GamePlay);
        settingUI.SetActive(false);
    }

}
