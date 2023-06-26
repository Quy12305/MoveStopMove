using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private Transform weapon;
    public List<Level> levels = new List<Level>();
    public Player player;
    public AttackRange attackRange;
    Level currentLevel;

    int level = 1;

    void Start()
    {
        UIManager.Instance.OpenMainMenuUI();
        LoadLevel();
    }

    public void LoadLevel()
    {
        LoadLevel(level);
        OnInit();
    }
    public void LoadLevel(int index)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }
        currentLevel = Instantiate(levels[index - 1]);
    }

    public void OnInit()
    {
        player.transform.position = currentLevel.startPointPlayer;
        attackRange.ResetRadius();
        weapon.gameObject.SetActive(true);

        if (!player.gameObject.activeSelf)
        {
            player.gameObject.SetActive(true);
            player.setIsDead(false);
            player.setIsAttack(false);
        }

        DeleteEnemy();
    }

    public void OnStart()
    {
        GameManager.Instance.ChangeState(GameState.GamePlay);
    }

    public void OnFinish()
    {
        UIManager.Instance.OpenFinishUI();
        GameManager.Instance.ChangeState(GameState.Finish);
    }
    public void OnLose()
    {
        UIManager.Instance.OpenLoseUI();
        GameManager.Instance.ChangeState(GameState.Lose);
    }

    public void NextLevel()
    {
        if (level < 3)
        {
            level++;
        }

        else
        {
            level = 1;
        }
        LoadLevel();
    }

    public void DeleteEnemy()
    {
        while(BotManager.Instance.getList().Count > 0)
        {
                GameObject gameobj = BotManager.Instance.getBot();
                IndicatorManager.Instance.RemoveIndicator(gameobj.GetComponent<Character>());
                Destroy(gameobj);
                BotManager.Instance.getList().Remove(gameobj);
        }
    }
}

