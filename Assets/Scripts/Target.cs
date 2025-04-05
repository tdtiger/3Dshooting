using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetType{
    Normal,
    Metal
}

public class Target : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    private ScoreManager scoreManager;

    [SerializeField]
    private TargetManager targetManager;

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

    [SerializeField]
    private TargetType type;

    private bool isFallen = false;

    void Start(){
        rb = GetComponent<Rigidbody>();
        scoreManager = FindObjectOfType<ScoreManager>();
        targetManager = FindObjectOfType<TargetManager>();
        // "?." は null条件演算子で、targetManagerがnullでない場合にSetTargetsを呼び出す
        // if(targetManager != null)みたいな記述を省略できる．
        targetManager?.SetTargets(gameObject);

        switch(type){
            case TargetType.Normal:
                scoreValue = 1;
                break;
            case TargetType.Metal:
                scoreValue = 5;
                break;
            default:
                break;
        }
    }

    void Update(){
        if(!isFallen && transform.position.y <= 1f){
            isFallen = true;
            scoreManager?.Addscore(scoreValue);
            targetManager?.RemoveTargets(gameObject);

            Destroy(gameObject, 2f);
        }
    }

    public void TakeDamage(Vector3 impactForce){
        rb.AddForce(impactForce, ForceMode.Impulse);
        Vector3 torque = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * impactForce.magnitude;
        rb.AddTorque(torque, ForceMode.Impulse);
    }
}
