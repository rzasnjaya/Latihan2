using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public Medals medals = new Medals();
    public int totalEnemy, enemyKilled, totalRescue, humanRescued;

    public UnityEvent onGameEnd;

    private void Awake()
    {
        instance = this;
        medals.untouched = true;
    }

    public void RegisterEnemy()
    {
        totalEnemy++;
    }

    public void RegisterRescue()
    {
        totalRescue++;
    }

    public void AddEnemyKill()
    {
        enemyKilled++;
    }

    public void AddRescue()
    {
        humanRescued++;
    }

    public void PlayerHit()
    {
        medals.untouched = false;
    }

    public void GameEnd()
    {
        StartCoroutine(CountDelay());
    }

    IEnumerator CountDelay()
    {
        yield return new WaitForSeconds(0.25f);

        if (enemyKilled >= totalEnemy)
            medals.kill = true;

        if (humanRescued >= totalRescue)
            medals.rescue = true;

        onGameEnd.Invoke();
    }    
}

[System.Serializable]
public class Medals
{
    public bool rescue, kill, untouched;
}
