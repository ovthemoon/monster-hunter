using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject enemyPrefab;
    public float spawnRadius = 10f; // 적을 소환할 반경
    public float safeDistance = 5f; // 플레이어와의 안전 거리
    public int enemySpawnCount = 10;
    public int spawnCooltime = 3;

    public int curDefeatedEnemyCount { get; private set; } = 0;
    int curEnemyCount = 0;
    [HideInInspector]
    public bool isGamePlaying = true;
    PlayerMove player;
    public bool isGameOver { get; private set; } = false;
    public bool isWin { get; private set; } = false;

    void Start()
    {
        instance = this;
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();
        StartCoroutine(SpawnEnemies());
    }

    void Update()
    {
        if (player.isDead)
        {
            isGamePlaying = false;
            StopCoroutine(SpawnEnemies()); // 플레이어가 죽으면 소환 중단
        }
    }

    IEnumerator SpawnEnemies()
    {
        while (curEnemyCount< enemySpawnCount)
        {
            Vector3 spawnPosition = RandomSpawnPosition();
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            curEnemyCount++;
            Debug.Log(curEnemyCount);
            yield return new WaitForSeconds(spawnCooltime); // 적 소환 간격 조절
        }
    }

    Vector3 RandomSpawnPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * spawnRadius;
        randomDirection += player.transform.position;
        randomDirection.y = player.transform.position.y; // Y 위치는 플레이어와 동일하게 설정

        // 플레이어와의 안전 거리를 보장하기 위한 조건 검사
        while (Vector3.Distance(randomDirection, player.transform.position) < safeDistance)
        {
            randomDirection = Random.insideUnitSphere * spawnRadius;
            randomDirection += player.transform.position;
            randomDirection.y = player.transform.position.y;
        }

        return randomDirection;
    }
    public void IncreaseDefeatedEnemyCount()
    {
        curDefeatedEnemyCount++;
        GameOverCheck();
    }
    public void GameOverCheck()
    {
        if (curDefeatedEnemyCount >= enemySpawnCount)
        {
            isGameOver = true;
            isWin = true;
            PauseGame();
        }
        else if (player.isDead)
        {
            isGameOver = true;
            PauseGame();
        }
    }
    public void PauseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        Cursor.visible = true;

    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
