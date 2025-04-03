using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private Camera fpsCamera;

    private float range = 50f;
    public float Range{
        get{
            return range;
        }
        set{
            range = value;
        }
    }

    [SerializeField]
    private GameObject impactEffect;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")){
            Shoot();
        }
    }

    void Shoot(){
        RaycastHit hit;
        if(Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range)){
            if(hit.transform.CompareTag("Target")){
                Destroy(hit.transform.gameObject);
                Instantiate(impactEffect, hit.point, Quaternion.identity);
            }
        }
    }
}
