using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    // 照準の画像
    [SerializeField]
    private Image crosshairImage;

    // プレイヤーの視点
    [SerializeField]
    private Camera playerCamera;

    // 照準の通常時の大きさ
    private Vector2 defaultSize;

    // 発射時の膨張率
    private float expandAmount = 40f;

    // 縮小していく速度
    private float shrinkSpeed = 5f;

    void Start(){
        // 照準をアクティブにし，大きさを取得
        if(crosshairImage != null){
            crosshairImage.enabled = true;
            defaultSize = crosshairImage.rectTransform.sizeDelta;
        }
    }

    void Update(){
        // 照準を視点の中心に設置
        if(crosshairImage != null && playerCamera != null){
            crosshairImage.rectTransform.sizeDelta = Vector2.Lerp(crosshairImage.rectTransform.sizeDelta, defaultSize, Time.deltaTime * shrinkSpeed);
        }
    }

    public void ExpandCrosshair(){
        // 照準の大きさを膨張
        if(crosshairImage != null){
            crosshairImage.rectTransform.sizeDelta = defaultSize + new Vector2(expandAmount, expandAmount);
        }
    }
}
