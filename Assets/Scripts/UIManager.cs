using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Slider playerHp;
    public TMP_Text defeatedEnemy;
    public TMP_Text maxEnemy;

    [Header("GameOverUI")]
    public TMP_Text isWin;
    public GameObject gameOverUI;

    public GameObject pauseMenu;

    PlayerMove player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();
        playerHp.maxValue = player.maxHp;
        playerHp.minValue = 0;
        playerHp.value = player.currentHp;
        gameOverUI.SetActive(false);
    }

    private void Update()
    {
        SetPlayerHp();
        SetEnemyCount();
        SetGameOver();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeInHierarchy)
            {
                BackToGame();
            }
            else
            {
                ActivePauseMenu();
            }
        }
    }
    void SetPlayerHp()
    {
        playerHp.value = player.currentHp;
    }
    void SetEnemyCount()
    {
        defeatedEnemy.text = GameManager.instance.curDefeatedEnemyCount.ToString();
        maxEnemy.text= GameManager.instance.enemySpawnCount.ToString();
    }
    void SetGameOver()
    {
        if(GameManager.instance.isGameOver&& GameManager.instance.isWin)
        {
            gameOverUI.SetActive(true);
            isWin.text = "You Defeated all enemies";
        }
        else if(player.isDead)
        {
            gameOverUI.SetActive(true);
            isWin.text = "you are dead";
        }
        
    }
    public void SceneChange(string nam)
    {
        GameManager.instance.ResumeGame();
        SceneManager.LoadScene(nam);
    }
    public void ActivePauseMenu()
    {
        pauseMenu.SetActive(true);
        GameManager.instance.PauseGame();
        
    }
    public void BackToGame()
    {
        GameManager.instance.ResumeGame();
        pauseMenu.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
