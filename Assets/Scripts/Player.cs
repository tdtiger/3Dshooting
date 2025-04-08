using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 移動速度
    [SerializeField]
    private float speed;

    void Update(){
        // 左右キーで移動
        if(Input.GetKey(KeyCode.LeftArrow)){
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.RightArrow)){
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }
}