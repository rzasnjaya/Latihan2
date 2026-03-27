using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CustomWeaponData", menuName = "Weapon Data")]
public class WeaponData : ScriptableObject
{
    [SerializeField] FireType type;
    [SerializeField] float rate = 0.15f;
    [SerializeField] int maxAmmo;
    [SerializeField] int damageValue;
    [SerializeField] bool defaultWeapon;

    private Camera cam;
    private PlayerScript player;
    private int currentAmmo;
    private float nextFireTime;

    public void SetupWeapon(Camera cam, PlayerScript player)
    {
        this.cam = cam;
        this.player = player;
        nextFireTime = 0f;
        currentAmmo = maxAmmo;
    }

    public void WeaponUpdate()
    {
        if (type == FireType.SINGLE)
        {
            if (Input.GetMouseButtonDown(0) && currentAmmo > 0)
            {
                Fire();
                currentAmmo--;
            }
            else
            {
                Debug.Log("Ammo runs out, please reload");
            }
        }
        else
        {
            if (Input.GetMouseButton(0) && Time.time > nextFireTime && currentAmmo > 0)
            {
                Fire();
                currentAmmo--;
                nextFireTime = Time.time + rate;
            }
            else if (currentAmmo <= 0)
            {
                Debug.Log("Ammo runs out, please reload");
            }
        }

        if (defaultWeapon && Input.GetMouseButtonDown(1))
        {
            currentAmmo = maxAmmo;
        }

        if (!defaultWeapon && currentAmmo <= 0)
        {
            player.SwitchWeapon();
        }
    }

    private void Fire()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 50f))
        {
            if (hit.collider != null)
            {
                IHitable[] hitables = hit.collider.GetComponents<IHitable>();
                if (hitables != null && hitables.Length > 0)
                {
                    foreach (var hitable in hitables)
                    {
                        hitable.Hit(hit, damageValue);
                    }
                }
                Debug.Log(hit.collider.gameObject.name);
            }
        }
    }

    public enum FireType
    {
        SINGLE,
        RAPID
    }
}