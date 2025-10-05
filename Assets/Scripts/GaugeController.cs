using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeController : MonoBehaviour
{
    // ゲージのUI要素
    [SerializeField]
    private RectTransform barFill;

    // ミニゲーム成功部分
    [SerializeField]
    private RectTransform sweetSpot;

    // ミニゲームの大成功部分
    [SerializeField]
    private RectTransform perfectSpot;

    // ゲージの移動速度
    [SerializeField]
    private float speed = 300f;

    // ミニゲームの成功範囲
    [SerializeField]
    private float sweetRange;

    // ミニゲームの大成功範囲
    [SerializeField]
    private float perfectRange;

    private bool movingRight = true;

    private bool isActive = false;

    public enum ResultType{
        Fail,
        Success,
        Perfect
    }

    private System.Action<ResultType> onFinish;

    void Update()
    {
        // 発射待機中でないならば何もしない
        if(!isActive)
            return;

        // ゲージの端に到達したら移動方向を反転
        float move = (movingRight ? 1 : -1) * speed * Time.deltaTime;

        barFill.anchoredPosition += new Vector2(move, 0);
        if(barFill.anchoredPosition.x >= 100)
            movingRight = false;
        if(barFill.anchoredPosition.x <= -100)
            movingRight = true;

        if(Input.GetMouseButtonDown(0)){
            isActive = false;

            float dist = Mathf.Abs(barFill.anchoredPosition.x - sweetSpot.anchoredPosition.x);
            ResultType result;

            if(dist <= perfectRange){
                result = ResultType.Perfect;
                Debug.Log("Perfect!");
            }
            else if(dist <= sweetRange){
                result = ResultType.Success;
                Debug.Log("Success!");
            }
            else{
                result = ResultType.Fail;
                Debug.Log("Fail...");
            }

            onFinish?.Invoke(result);
            // 発射できたらゲージを非表示にする
            gameObject.SetActive(false);
        }
    }

    public void StartGauge(System.Action<ResultType> callback){
        onFinish = callback;
        isActive = true;
        barFill.anchoredPosition = new Vector2(-100, 0);
        movingRight = true;
        gameObject.SetActive(true);
    }
}
