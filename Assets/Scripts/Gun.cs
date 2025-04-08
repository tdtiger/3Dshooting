using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    Transform firePoint;


    [SerializeField]
    private AudioSource gunSound;

    [SerializeField]
    private AudioClip gunSoundClip;

    [SerializeField]
    private Crosshair crosshair;

    [SerializeField]
    private GameObject miniGameUI;

    private GaugeController gauge;

    void Start(){
        gauge = miniGameUI.GetComponent<GaugeController>();
        miniGameUI.SetActive(false);
    }

    void Update(){
        if(Input.GetMouseButtonDown(0) && !miniGameUI.activeSelf){
            miniGameUI.SetActive(true);
            gauge.StartGauge(FireBullet);
        }
    }

    void Shoot(GameObject bullet, float speed){
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if(crosshair != null){
            crosshair.ExpandCrosshair();
        }

        if(gunSound != null){
            gunSound.PlayOneShot(gunSoundClip);
        }

        rb.velocity = firePoint.forward * speed;

        Destroy(bullet, 3f);
    }

    private void FireBullet(bool isSuccess){
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        float baseSpeed = 15f;
        if(isSuccess)
            baseSpeed *= 1.8f;

        Shoot(bullet, baseSpeed);

        miniGameUI.SetActive(false);
    }
}
