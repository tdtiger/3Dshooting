using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // 着弾時のエフェクト
    [SerializeField]
    private GameObject impactEffectPrefab;

    // 着弾時の衝撃調整用
    [SerializeField]
    private float bulletForceMultiplier;

    private Rigidbody rb;

    void Start(){
        // 自身のRigidbodyを取得
        rb = GetComponent<Rigidbody>();
    }

    // 衝突時の処理
    private void OnCollisionEnter(Collision collision){
        // Targetタグを持つオブジェクトに衝突した場合，ターゲットに衝撃を与える．
        if (collision.gameObject.CompareTag("Target")){
            Target target = collision.gameObject.GetComponent<Target>();
            if (target != null){
                Vector3 impactForce = rb.velocity * bulletForceMultiplier;
                target.TakeDamage(impactForce);
            }
        }

        // 着弾時のエフェクトを生成
        if (impactEffectPrefab != null){
            GameObject eff = Instantiate(impactEffectPrefab, transform.position, Quaternion.identity);
            Destroy(eff, 2f);
        }

        // 自身を削除
        Destroy(gameObject);
    }
}