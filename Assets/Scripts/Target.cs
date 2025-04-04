using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    private ScoreManager scoreManager;

    [SerializeField]
    private int scoreValue = 1;
    public int ScoreValue{
        get{
            return scoreValue;
        }
        set{
            scoreValue = value;
        }
    }

    private bool isFallen = false;

    void Start(){
        rb = GetComponent<Rigidbody>();
    }

    void Update(){
        if(!isFallen && transform.position.y <= 0.5f){
            isFallen = true;
            scoreManager.Addscore(scoreValue);
            Destroy(gameObject, 2f);
        }
    }

    public void TakeDamage(Vector3 impactForce){
        rb.AddForce(impactForce, ForceMode.Impulse);
        Vector3 torque = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * impactForce.magnitude;
        rb.AddTorque(torque, ForceMode.Impulse);
    }
}
