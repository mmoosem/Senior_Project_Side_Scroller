using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject gun;
    public bool hasGun=false;
    private void Start()
    {
        gun.GetComponent<SpriteRenderer>().enabled = false;

    }
    void Update()
    {
        if (hasGun == true)
        {
            gun.GetComponent<SpriteRenderer>().enabled = true;

        }
        else
        {
            gun.GetComponent<SpriteRenderer>().enabled = false;

        }
        hasGun = GameObject.Find("player_character").GetComponent<playerController>().hasGun;
        if (Input.GetKeyDown(KeyCode.S)&&hasGun==true)
        {
            Shoot();
        }
    }
    void Shoot()
    {
        Instantiate(bulletPrefab,firePoint.position,firePoint.rotation);
    }
}
