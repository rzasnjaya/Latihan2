using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] GameState state;
    [SerializeField] PlayerMove playerMove;
    [SerializeField] int playerHealth = 10;

    private float currentHealth;
    private int enemyHit, shotsFired;
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
    }
}

public enum GameState
{
    Default,
    Start,
    Gameplay,
    LevelEnd
}
