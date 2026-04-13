using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class PowerUpMenu : MonoBehaviour
{
    public Button shieldButton, laserButton, megaBombButton;
    public TMP_Text shieldText, laserText, megaBombText;
    public int shieldAmount = 3, laserAmount = 3, megaBombAmount = 1;

    public ShieldScript shield;
    public LaserScript laser;
    public MegaBombScript megaBomb;

    private float defaultPhysicsStep;

    // Start is called before the first frame update
    void Start()
    {
        shieldText.text = "S" + shieldAmount;
        laserText.text = "L" + laserAmount;
        megaBombText.text = "M" + megaBombAmount;

        shieldButton.onClick.AddListener(EnableShield);
        laserButton.onClick.AddListener(EnableLaser);
        megaBombButton.onClick.AddListener(EnableMegaBomb);

        defaultPhysicsStep = Time.fixedDeltaTime;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        BulletTime(Input.GetMouseButton(1));

#elif UNITY_ANDROID
    BulletTime(Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId));
#endif
    }

    void BulletTime(bool slow)
    {
        if (slow)
        {
            Time.timeScale = 0.25f;
            Time.fixedDeltaTime = defaultPhysicsStep * Time.timeScale; 
        }
        else
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = defaultPhysicsStep;
        }

        shieldButton.gameObject.SetActive(slow);
        laserButton.gameObject.SetActive(slow);
        megaBombButton.gameObject.SetActive(slow);
    }

    void EnableShield()
    {
        if (shieldAmount == 0)
            return;

        shield.ShieldUp();

        shieldAmount--;

        shieldText.text = "S" + shieldAmount;

        if (shieldAmount == 0)
            shieldButton.interactable = false;
    }

    void EnableLaser()
    {
        if (laserAmount == 0)
            return;

        laser.ShootLaser();

        laserAmount--;

        laserText.text = "L" + laserAmount;

        if (laserAmount == 0)
            laserButton.interactable = false;
    }

    void EnableMegaBomb()
    {
        if (megaBombAmount == 0)
            return;

        megaBomb.DeployBomb();

        megaBombAmount--;

        megaBombText.text = "M" + megaBombAmount;

        if (megaBombAmount == 0)
            megaBombButton.interactable = false;
    }
}
