using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    private float sensitivity = 100f;
    public float Sensitivity{
        get{
            return sensitivity;
        }
        set{
            sensitivity = value;
        }
    }

    [SerializeField]
    private Transform playerBody;

    float xRotation = 0f;

    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update(){
        // マウスの動きに合わせて視点を移動
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
