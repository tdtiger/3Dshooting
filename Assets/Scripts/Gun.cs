using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    Transform firePoint;

    private float bulletSpeed = 20f;

    [SerializeField]
    private AudioSource gunSound;

    [SerializeField]
    private AudioClip gunSoundClip;

    [SerializeField]
    private GameObject impactEffect;

    [SerializeField]
    private Crosshair crosshair;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")){
            Shoot();
        }
    }

    void Shoot(){
        if(crosshair != null){
            crosshair.ExpandCrosshair();
        }

        if(gunSound != null){
            gunSound.PlayOneShot(gunSoundClip);
        }

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = firePoint.forward * bulletSpeed;

        Destroy(bullet, 3f);
    }
}
