using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] WeaponData defaultWeapon;

    private Camera cam;
    private WeaponData currentWeapon;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();

        SwitchWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentWeapon != null) 
            currentWeapon.WeaponUpdate();
    }

    public void SwitchWeapon(WeaponData weapon = null)
    {
        currentWeapon = weapon != null ? weapon : defaultWeapon;
        currentWeapon.SetupWeapon(cam, this);
    }
}
