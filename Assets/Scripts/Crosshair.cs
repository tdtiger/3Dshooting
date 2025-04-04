using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    [SerializeField]
    private Image crosshairImage;

    [SerializeField]
    private Camera playerCamera;

    private Vector2 defaultSize;

    private float expandAmount = 40f;
    public float ExpandAmount{
        get{
            return expandAmount;
        }
        set{
            expandAmount = value;
        }
    }

    private float shrinkSpeed = 5f;
    public float ShrinkSpeed{
        get{
            return shrinkSpeed;
        }
        set{
            shrinkSpeed = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(crosshairImage != null){
            crosshairImage.enabled = true;
            defaultSize = crosshairImage.rectTransform.sizeDelta;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(crosshairImage != null && playerCamera != null){
            // Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
            // crosshairImage.rectTransform.position = screenCenter;
            crosshairImage.rectTransform.sizeDelta = Vector2.Lerp(crosshairImage.rectTransform.sizeDelta, defaultSize, Time.deltaTime * shrinkSpeed);
        }
    }

    public void ExpandCrosshair(){
        if(crosshairImage != null){
            crosshairImage.rectTransform.sizeDelta = defaultSize + new Vector2(expandAmount, expandAmount);
        }
    }
}
