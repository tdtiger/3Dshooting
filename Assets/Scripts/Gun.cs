using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GunType{
    Normal,
    Burst,
    Laser
}

public class Gun : MonoBehaviour
{
    // 弾のプレハブ
    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    GameObject laserPrefab;

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
            switch(GameManager.instance.CurrentGun){
                case GunType.Normal:
                    gauge.StartGauge(FireBullet);
                    break;
                case GunType.Burst:
                    gauge.StartGauge(BurstShoot);
                    break;
                case GunType.Laser:
                    gauge.StartGauge(FireLaser);
                    break;
            }
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

    void VerticalShoot(GameObject bullet1, GameObject bullet2, GameObject bullet3, float speed){
        Rigidbody rb1 = bullet1.GetComponent<Rigidbody>();
        Rigidbody rb2 = bullet2.GetComponent<Rigidbody>();
        Rigidbody rb3 = bullet3.GetComponent<Rigidbody>();

        if(crosshair != null){
            crosshair.ExpandCrosshair();
        }

        if(gunSound != null){
            gunSound.PlayOneShot(gunSoundClip);
        }

        rb1.velocity = (firePoint.forward + new Vector3(0.1f, 0, 0)) * speed;
        rb2.velocity = firePoint.forward * speed;
        rb3.velocity = (firePoint.forward + new Vector3(-0.1f, 0, 0)) * speed;

        // 発砲から3秒後で弾を消滅させる
        Destroy(bullet1, 3f);
        Destroy(bullet2, 3f);
        Destroy(bullet3, 3f);
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

    private void BurstShoot(bool isSuccess){
        GameObject bullet1 = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        GameObject bullet2 = Instantiate(bulletPrefab, firePoint.position + new Vector3(0, 0, 0.3f), firePoint.rotation);
        GameObject bullet3 = Instantiate(bulletPrefab, firePoint.position + new Vector3(0, 0, 0.6f), firePoint.rotation);

        float baseSpeed = 15f;
        // ミニゲームに成功時，弾の速度を1.8倍にする
        if(isSuccess)
            baseSpeed *= 1.8f;

        VerticalShoot(bullet1, bullet2, bullet3, baseSpeed);
        miniGameUI.SetActive(false);
    }

    private void FireLaser(bool isSuccess){
        GameObject bullet = Instantiate(laserPrefab, firePoint.position, firePoint.rotation);

        float baseSpeed = 15f;
        // ミニゲームに成功時，弾の速度を1.8倍にする
        if(isSuccess)
            baseSpeed *= 1.8f;

        Shoot(bullet, baseSpeed);

        miniGameUI.SetActive(false);
    }
}
