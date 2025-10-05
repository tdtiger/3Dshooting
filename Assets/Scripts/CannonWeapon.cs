using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonWeapon : WeaponBase
{
    [Header("大砲")]
    [SerializeField]
    private GameObject cannonBallPrefab;
    [SerializeField]
    private float launchForce = 30f;
    [SerializeField]
    private GameObject explosionPrefab;

    protected override void Fire(float powerMultiplier){
        if(!cannonBallPrefab)
            return;

        Vector3 dir = GetFireDirection();

        // 弾の生成
        GameObject ball = Instantiate(cannonBallPrefab, firePoint.position, Quaternion.LookRotation(dir));
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        if(rb)
            rb.AddForce(dir * launchForce * powerMultiplier, ForceMode.Impulse);

        PlayEffects();
    }
}
