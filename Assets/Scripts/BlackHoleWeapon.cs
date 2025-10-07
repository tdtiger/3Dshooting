using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleWeapon : WeaponBase
{
    [SerializeField]
    private GameObject blackHolePrefab;

    protected override void Fire(float powerMultiplier){
        if(!blackHolePrefab)
            return;
        
        Vector3 dir = GetFireDirection();
        Instantiate(blackHolePrefab, firePoint.position, Quaternion.LookRotation(dir));

        PlayEffects();
    }
}
