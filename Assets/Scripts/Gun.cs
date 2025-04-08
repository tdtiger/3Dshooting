using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // 弾のプレハブ
    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    Transform firePoint;

    // 発砲音
    [SerializeField]
    private AudioSource gunSound;

    [SerializeField]
    private AudioClip gunSoundClip;

    // 照準
    [SerializeField]
    private Crosshair crosshair;

    // 発砲時のミニゲーム用UI
    [SerializeField]
    private GameObject miniGameUI;

    private GaugeController gauge;

    void Start(){
        gauge = miniGameUI.GetComponent<GaugeController>();
        miniGameUI.SetActive(false);
    }

    void Update(){
        // 左クリックで発砲時のミニゲームに移行
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

        // 発砲から3秒後で弾を消滅させる
        Destroy(bullet, 3f);
    }

    private void FireBullet(bool isSuccess){
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        float baseSpeed = 15f;
        // ミニゲームに成功時，弾の速度を1.8倍にする
        if(isSuccess)
            baseSpeed *= 1.8f;

        Shoot(bullet, baseSpeed);

        miniGameUI.SetActive(false);
    }
}
