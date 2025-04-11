using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private GunType currrentGun;
    public GunType CurrentGun{
        get{
            return currrentGun;
        }
        set{
            currrentGun = value;
        }
    }

    private GunType[] availableGuns = {
        GunType.Normal,
        GunType.Burst,
        GunType.Laser
    };

    private int currentGunIndex = 0;

    void Awake(){
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Space)){
            SwitchGun();
        }
    }

    private void SwitchGun(){
        currentGunIndex = (currentGunIndex + 1) % availableGuns.Length;
        CurrentGun = availableGuns[currentGunIndex];
        Debug.Log("Current Gun: " + CurrentGun.ToString());
    }
}
