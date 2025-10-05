using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private WeaponBase[] weapons;
    private int currentIndex = 0;

    [SerializeField]
    private GaugeController gaugeController;

    [SerializeField]
    private Transform weaponHolder;

    void Start(){
        weapons = weaponHolder.GetComponentsInChildren<WeaponBase>(true);

        for(int i = 0; i < weapons.Length; i++)
            weapons[i].gameObject.SetActive(i == currentIndex);
    }

    void Update(){
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        // マウスのホイールまたはQ/Eで武器の切り替え
        if(scroll > 0f)
            NextWeapon();
        else if(scroll < 0f)
            PreviousWeapon();

        if(Input.GetKeyDown(KeyCode.Q))
            NextWeapon();
        if(Input.GetKeyDown(KeyCode.E))
            PreviousWeapon();

        if(Input.GetMouseButtonDown(0)){
            if(!gaugeController.gameObject.activeSelf)
                gaugeController.StartGauge(OnGaugeFinish);
        }
    }

    private void NextWeapon(){
        weapons[currentIndex].gameObject.SetActive(false);
        currentIndex = (currentIndex + 1) % weapons.Length;
        weapons[currentIndex].gameObject.SetActive(true);
        Debug.Log("Switched to weapon: " + weapons[currentIndex].WeaponName);
    }

    private void PreviousWeapon(){
        weapons[currentIndex].gameObject.SetActive(false);
        currentIndex = (currentIndex - 1 + weapons.Length) % weapons.Length;
        weapons[currentIndex].gameObject.SetActive(true);
        Debug.Log("Switched to weapon: " + weapons[currentIndex].WeaponName);
    }

    private void OnGaugeFinish(GaugeController.ResultType result){
        float powerMultiplier = 1f;
        switch(result){
            case GaugeController.ResultType.Fail:
                powerMultiplier = 0.5f;
                break;
            case GaugeController.ResultType.Success:
                powerMultiplier = 1f;
                break;
            case GaugeController.ResultType.Perfect:
                powerMultiplier = 1.5f;
                break;
        }
        
        weapons[currentIndex].TryFire(powerMultiplier);
    }
}