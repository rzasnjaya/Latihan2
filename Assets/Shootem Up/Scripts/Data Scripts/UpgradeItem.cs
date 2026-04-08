using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UpgradeItem : MonoBehaviour
{
    [Header("Upgrade Menu Objects")]
    public string statName;
    public string itemName;
    public Text itemNameText, buyText;
    public Slider itemLevelBar;
    public Button buyButton;

    [Header("Item Prices Setup")]
    public int[] pricesLevel;

    private StatsUpgradeInfo stat;
    private bool isUpgrading;

    // Start is called before the first frame update
    void Start()
    {

    }
}
