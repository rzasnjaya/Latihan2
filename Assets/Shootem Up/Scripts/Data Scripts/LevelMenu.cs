using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelMenu : MonoBehaviour
{
    public string sceneTarget, requiredLevel;

    public Button playButton;
    public TMP_Text killText, rescueText, untouchedText;

    public Color disabledColor, enabledColor;

    private Medals sceneMedal;


    // Start is called before the first frame update
    void Start()
    {
        //if (StatsManager.instance.achievmentList.ContainsKey(sceneTarget))
        //    sceneMedal = StatsManager.instance.achievmentList[sceneTarget];

        UpdateMenu();
        playButton.onClick.AddListener(GoToLevel);
        CheckIfUnlocked();
        playButton.GetComponentInChildren<TMP_Text>().text = (transform.GetSiblingIndex() + 1).ToString("00");
    }

    public void UpdateMenu()
    {
        if (StatsManager.instance.achievmentList.ContainsKey(sceneTarget))
            sceneMedal = StatsManager.instance.achievmentList[sceneTarget];

        if (sceneMedal != null)
        {
            killText.color = sceneMedal.kill ? enabledColor : disabledColor;
            rescueText.color = sceneMedal.rescue ? enabledColor : disabledColor;
            untouchedText.color = sceneMedal.untouched ? enabledColor : disabledColor;
        }
        else
        {
            killText.color = disabledColor;
            rescueText.color = disabledColor;
            untouchedText.color = disabledColor;
        }

        CheckIfUnlocked();
    }

    void CheckIfUnlocked()
    {
        bool unlockedCondition = StatsManager.instance.levelCompleted.ContainsKey(requiredLevel) && StatsManager.instance.levelCompleted[requiredLevel];
        playButton.interactable = unlockedCondition || string.IsNullOrEmpty(requiredLevel) ? true : false;
    }
    
    void GoToLevel()
    {
        SceneLoader.instance.ChangeScene(sceneTarget);
    }
}
