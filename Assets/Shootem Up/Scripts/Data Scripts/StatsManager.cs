using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager instance;

    public List<ShootProfile> blaserUpgradeList = new List<ShootProfile>();
    public List<ShootProfile> missileUpgradeList = new List<ShootProfile>();
    public List<float> healthUpgradeList = new List<float>();
    public List<MegaBombData> megaBombUpgList = new List<MegaBombData>();
    public List<ShieldData> shieldUpgList = new List<ShieldData>();
    public List<LaserData> laserUpgList = new List<LaserData>();

    public Dictionary<string, Medals> achievmentList = new Dictionary<string, Medals>();
    public Dictionary<string, DateTime> statsTimer = new Dictionary<string, DateTime>();

    //singleton pattern init
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class StatsUpgradeInfo
{
    public string name;
    public int level;
    public float[] upgradeTime;
}

[System.Serializable]
public class MegaBombData
{
    public float radius;
    public float damage;
}

[System.Serializable]
public class ShieldData
{
    public float shieldDuration;
}

[System.Serializable]
public class LaserData
{
    public float laserDuration;
}