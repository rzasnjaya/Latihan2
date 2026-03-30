using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

[System.Serializable]
public class UIManager
{
    [SerializeField] Slider healthBar;
    [SerializeField] TextMeshProUGUI timerText;

    [Header("Weapon HUD")]
    [SerializeField] Image weaponIcon;
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] GameObject reloadWarning;

    private WeaponData currentWeapon;

    public void Init(float maxHealth)
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
        PlayerScript.OnWeaponChanged += UpdateWeapon;
        TimerObject.OnTimerChanged += UpdateTimer;
    }

    private void UpdateTimer(int currentTimer)
    {
        timerText.SetText(currentTimer.ToString("00"));
    }

    public void RemoveEvent()
    {
        PlayerScript.OnWeaponChanged -= UpdateWeapon;
        TimerObject.OnTimerChanged -= UpdateTimer;
        currentWeapon.OnWeaponFired -= UpdateAmmo;
    }

    private void UpdateWeapon(WeaponData obj)
    {
        if (currentWeapon != null)
            currentWeapon.OnWeaponFired -= UpdateAmmo;

        currentWeapon = obj;
        currentWeapon.OnWeaponFired += UpdateAmmo;
        weaponIcon.sprite = currentWeapon.GetIcon;

    }

    public void UpdateHealth(float value)
    {
        healthBar.value = value;
    }

    void UpdateAmmo(int ammo)
    {
        reloadWarning.SetActive(ammo <= 0);

        ammoText.SetText(ammo.ToString("00"));
    }
}
