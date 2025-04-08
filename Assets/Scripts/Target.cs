using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ターゲットのタイプ
public enum TargetType{
    Normal,
    Gold,
    Iron
}

public class Target : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    private ScoreManager scoreManager;

    [SerializeField]
    private TargetManager targetManager;

    private int scoreValue;
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

        // ターゲットのタイプに応じた得点を設定
        switch(type){
            case TargetType.Normal:
                scoreValue = 1;
                break;
            case TargetType.Gold:
                scoreValue = 3;
                break;
            case TargetType.Iron:
                scoreValue = 5;
                break;
            default:
                scoreValue = 1;
                break;
        }
    }

    void Update(){
        // y座標が1以下になったら，落下したとみなす
        if(!isFallen && transform.position.y <= 1f){
            isFallen = true;
            scoreManager?.Addscore(scoreValue);
            targetManager?.RemoveTargets(gameObject);

            Destroy(gameObject, 2f);
        }
    }

    public void TakeDamage(Vector3 impactForce){
        // ターゲットに衝撃を与える
        rb.AddForce(impactForce, ForceMode.Impulse);
        // ターゲットにランダムに回転させる
        Vector3 torque = new Vector3(Random.Range(-0.7f, -0.7f), Random.Range(-0.7f, 0.7f), Random.Range(-0.7f, 0.7f)) * impactForce.magnitude;
        rb.AddTorque(torque, ForceMode.Impulse);
    }
}
