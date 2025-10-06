using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWaveWeapon : WeaponBase
{
    [Header("衝撃波")]
    [SerializeField]
    private GameObject shockwaveEffectPrefab;
    [SerializeField]
    private float range = 10f;
    [SerializeField]
    private float force = 700f;

    protected override void Fire(float powerMultiplier){
        Vector3 origin = firePoint.position + GetFireDirection() * 10f;

        if(shockwaveEffectPrefab){
            GameObject effect = Instantiate(shockwaveEffectPrefab, origin, Quaternion.identity);
            Destroy(effect.gameObject, 3f);
        }

        Collider[] colliders = Physics.OverlapSphere(origin, range);
        foreach(Collider nearby in colliders){
            Rigidbody rb = nearby.attachedRigidbody;
            if(rb != null)
                rb.AddExplosionForce(force * powerMultiplier, origin, range, 2f, ForceMode.Impulse);
        }

        PlayEffects();

        if(CameraShake.Instance != null)
            CameraShake.Instance.Shake(0.25f, 0.3f);
    }
}
