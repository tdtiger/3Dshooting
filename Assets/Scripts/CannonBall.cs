using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    [SerializeField]
    private float lifeTime = 5f;
    [SerializeField]
    private float explosionRadius = 50f;
    [SerializeField]
    private float explosionForce = 7000f;
    [SerializeField]
    private GameObject explosionPrefab;
    [SerializeField]
    private AudioClip explosionSound;
    private AudioSource audioSource;

    void Start(){
        audioSource = GetComponent<AudioSource>();
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision){
        // 何かに衝突した時、爆発して消滅
        if(explosionPrefab){
            GameObject effect = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(effect.gameObject, 3f);
        }
    
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, explosionRadius);
        foreach(Collider nearby in colliders){
            Rigidbody rb = nearby.attachedRigidbody;
            if(rb != null)
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
        }

        if(explosionSound && audioSource){
            Debug.Log("Play Explosion Sound");
            AudioSource.PlayClipAtPoint(explosionSound, this.transform.position);
        }

        if(CameraShake.Instance != null)
            CameraShake.Instance.Shake(0.7f, 1f);

        Destroy(gameObject);
    }
}
