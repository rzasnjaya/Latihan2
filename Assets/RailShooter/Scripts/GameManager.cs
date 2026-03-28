using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] GameState state;
    [SerializeField] PlayerMove playerMove;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SwitchState(GameState.Start);
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
}

public enum GameState
{
    Default,
    Start,
    Gameplay,
    LevelEnd
}
