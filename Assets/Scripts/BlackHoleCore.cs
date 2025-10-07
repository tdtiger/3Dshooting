using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoreCore : MonoBehaviour
{
    [SerializeField]
    private float launchSpeed = 40f;
    [SerializeField]
    private float activateDelay = 1.5f;
    [SerializeField]
    private float maxLifeTime = 10f;
    [SerializeField]
    private float pullRadius = 25f;
    [SerializeField]
    private float pullForce = 300f;
    [SerializeField]
    private float pullDuration = 5f;
    [SerializeField]
    private float explosionForce = 500f;
    [SerializeField]
    private float explosionRadius = 20f;
    [SerializeField]
    private GameObject explosionPrefab;
    [SerializeField]
    private AudioClip explosionSound;

    private Rigidbody rb;
    private AudioSource audioSource;

    private float timer = 0f;
    private bool activated = false;
    private bool exploded = false;

    void Start(){
        rb = this.GetComponent<Rigidbody>();
        audioSource = this.GetComponent<AudioSource>();

        rb.velocity = transform.forward * launchSpeed;
        Destroy(this.gameObject, maxLifeTime);
    }

    void Update(){
        timer += Time.deltaTime;
        if(!activated && timer >= activateDelay)
            ActivateBlachHole();
        
        if(activated && !exploded){
            PullObjects();
            if(timer >= pullDuration)
                Explode();
        }
    }

    private void ActivateBlachHole(){
        activated = true;
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
    }

    private void PullObjects(){
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, pullRadius);
        foreach(var col in colliders){
            Rigidbody rb = col.attachedRigidbody;
            if(rb != null){
                Vector3 dir = (this.transform.position - rb.position).normalized;
                float dist = Vector3.Distance(this.transform.position, rb.position);
                float force = pullForce / Mathf.Max(dist, 1f);
                rb.AddForce(dir * force, ForceMode.Acceleration);
            }
        }
    }

    private void Explode(){
        exploded = true;
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

        if(explosionSound && audioSource)
            AudioSource.PlayClipAtPoint(explosionSound, this.transform.position);

        if(CameraShake.Instance != null)
            CameraShake.Instance.Shake(0.6f, 1.2f);

        Destroy(this.gameObject, 1f);
    }
}
