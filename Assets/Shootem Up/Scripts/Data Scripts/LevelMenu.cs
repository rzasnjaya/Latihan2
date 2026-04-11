using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelMenu : MonoBehaviour
{
    public string sceneTarget;

    public Button playButton;
    public TMP_Text killText, rescueText, untouchedText;

    public Color disabledColor, enabledColor;

    private Medals sceneMedal;


    // Start is called before the first frame update
    void Start()
    {
        if (StatsManager.instance.achievmentList.ContainsKey(sceneTarget))
            sceneMedal = StatsManager.instance.achievmentList[sceneTarget];

        playButton.onClick.AddListener(GoToLevel);
    }

    public void UpdateMenu()
    {
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
    }
    
    void GoToLevel()
    {
        SceneLoader.instance.ChangeScene(sceneTarget);
    }
}
