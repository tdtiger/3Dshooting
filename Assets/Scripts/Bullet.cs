using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private GameObject impactEffectPrefab;

    [SerializeField]
    private float bulletForceMultiplier;
    public float BulletForceMultiplier{
        get{
            return bulletForceMultiplier;
        }
        set{
            bulletForceMultiplier = value;
        }
    }

    private Rigidbody rb;

    void Start(){
        rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("Target")){
            Target target = collision.gameObject.GetComponent<Target>();
            if (target != null){
                Vector3 impactForce = rb.velocity * bulletForceMultiplier;
                target.TakeDamage(impactForce);
            }
        }

        if (impactEffectPrefab != null){
            GameObject eff = Instantiate(impactEffectPrefab, transform.position, Quaternion.identity);
            Destroy(eff, 2f);
        }

        Destroy(gameObject);
    }
}