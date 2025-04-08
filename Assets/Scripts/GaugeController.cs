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

    // ゲージの移動速度
    [SerializeField]
    private float speed = 300f;

    // ミニゲームの成功範囲
    [SerializeField]
    private float sweetRange;

    private bool movingRight = true;

    private bool isActive = false;

    private System.Action<bool> onFinish;

    void Update()
    {
        // 発射待機中でないならば何もしない
        if(!isActive)
            return;

        // ゲージの端に到達したら移動方向を反転
        float move;
        if(movingRight)
            move = speed * Time.deltaTime;
        else
            move = speed * Time.deltaTime * (-1);

        barFill.anchoredPosition += new Vector2(move, 0);
        if(barFill.anchoredPosition.x >= 100)
            movingRight = false;
        if(barFill.anchoredPosition.x <= -100)
            movingRight = true;

        if(Input.GetMouseButtonDown(0)){
            isActive = false;

            float dist = Mathf.Abs(barFill.anchoredPosition.x - sweetSpot.anchoredPosition.x);
            bool isSuccess = dist < sweetRange;

            onFinish?.Invoke(isSuccess);
            // 発射できたらゲージを非表示にする
            gameObject.SetActive(false);
        }
    }

    public void StartGauge(System.Action<bool> callback){
        onFinish = callback;
        isActive = true;
        barFill.anchoredPosition = new Vector2(-100, 0);
        movingRight = true;
        gameObject.SetActive(true);
    }
}
