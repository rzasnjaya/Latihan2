using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] GameState state;
    [SerializeField] PlayerMove playerMove;
    [SerializeField] PlayerScript playerScript;
    [SerializeField] int playerHealth = 10;

    [SerializeField] UIManager uiManager = new UIManager();

    private float currentHealth;
    private int enemyHit, shotsFired, enemyKilled, totalEnemy, hostageKilled;

    private TimerObject timerObject = new TimerObject();

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SwitchState(GameState.Start);
        Init();
    }

    void Init()
    {
        currentHealth = playerHealth;
        uiManager.Init(currentHealth);
    }

    private void OnDisable()
    {
        uiManager.RemoveEvent();
    }

    public void SwitchState(GameState newState)
    {
        if (state == newState)
            return;

        state = newState;
        switch (state)
        {
            case GameState.Start:
                Debug.Log("Game Start");
                playerMove.enabled = false;
                this.DelayedAction(delegate { SwitchState(GameState.Gameplay); }, 3f);
                break;
            case GameState.Gameplay:
                Debug.Log("State: Gameplay " + Time.time);
                playerMove.enabled = true;
                break;
            case GameState.LevelEnd:
                break;
        }
    }

    public void ShotHit(bool hit)
    {
        if (hit)
            enemyHit++;

        shotsFired++;
    }

    public void PlayerHit(float damage)
    {
        currentHealth -= damage;
        uiManager.UpdateHealth(currentHealth);
        playerScript.ShakeCamera(0.5f, 0.2f, 5f);
    }

    public void StartTimer(float duration)
    {
        timerObject.StartTimer(this, duration);
    }

    public void StopTimer()
    {
        timerObject.StopTimer(this);
    }

    public void RegisterEnemy()
    {
        totalEnemy++;
    }

    public void HostageKilled()
    {
        hostageKilled++;
    }

    public void EnemyKilled()
    {
        enemyKilled++;
    }
}

public enum GameState
{
    Default,
    Start,
    Gameplay,
    LevelEnd
}
