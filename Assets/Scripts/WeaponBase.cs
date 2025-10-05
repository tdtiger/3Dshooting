using UnityEngine;

// 武器の基底クラス
public abstract class WeaponBase : MonoBehaviour{
    [Header("共通設定")]
    [SerializeField]
    protected string weaponName;
    public string WeaponName => weaponName;
    [SerializeField]
    protected float basePower;
    [SerializeField]
    protected float fireRate;
    [SerializeField]
    protected Transform firePoint;
    [SerializeField]
    protected GameObject muzzleFlashPrefab;
    [SerializeField]
    protected AudioClip fireSound;

    protected float nextFireTime = 0f;
    protected AudioSource audioSource;
    protected Camera mainCamera;

    protected virtual void Awake(){
        mainCamera = Camera.main;
        audioSource = GetComponent<AudioSource>();
    }

    public virtual void TryFire(float powerMultiplier){
        if(Time.time >= nextFireTime){
            Fire(powerMultiplier);
            nextFireTime = Time.time + fireRate;
        }
    }

    protected abstract void Fire(float powerMultiplier);

    protected virtual void PlayEffects(){
        if(muzzleFlashPrefab){
            GameObject effect = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation);
            Destroy(effect.gameObject, 3f);
        }
        if(fireSound && audioSource)
            audioSource.PlayOneShot(fireSound);
    }

    protected virtual Vector3 GetFireDirection(){
        return mainCamera.transform.forward;
    }

    public virtual void SetBaseStats(float power, float rate){
        basePower = power;
        fireRate = rate;
    }
}