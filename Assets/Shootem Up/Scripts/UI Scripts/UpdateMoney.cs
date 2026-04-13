using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateMoney : MonoBehaviour
{
    public static UpdateMoney instance;

    public TMP_Text moneyDisplay, scoreDisplay;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        DisplayMoney(StatsManager.instance.money);
    }

    public void UpdateMoneyDisplay()
    {
        DisplayMoney(StatsManager.instance.money);
    }

    public void DisplayMoney(int value)
    {
        if (moneyDisplay)
            moneyDisplay.text = "$ " + value.ToString();
    }

    public void DisplayScore(int value)
    {
        if (scoreDisplay)
            scoreDisplay.text = value.ToString("00000000");
    }
}
