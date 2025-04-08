using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    // ターゲットのプレハブのリスト
    [SerializeField]
    private GameObject[] targetPrefabs;

    // ターゲットの設置位置のリスト
    [SerializeField]
    private Transform[] spawnPoints;

    // 落下していないターゲットのリスト
    private List<GameObject> activeTargets = new List<GameObject>();

    void Start(){
        SpawnTargets();
    }

    public void SetTargets(GameObject target){
        // ターゲットリストに追加
        if(!activeTargets.Contains(target)){
            activeTargets.Add(target);
        }
    }

    public void RemoveTargets(GameObject target){
        // ターゲットを取り除く
        if(activeTargets.Contains(target)){
            activeTargets.Remove(target);

            // すべてのターゲットが落下していたら，新しく設置しなおす
            if(activeTargets.Count == 0){
                Invoke(nameof(SpawnTargets), 2f);
            }
        }
    }

    public void SpawnTargets(){
        // 各設置位置ごとに，ランダムにターゲットを生成
        foreach(Transform point in spawnPoints){
            int index = Random.Range(0, targetPrefabs.Length);
            GameObject prefab = targetPrefabs[index];

            GameObject target = Instantiate(prefab, point.position, point.rotation);
            SetTargets(target);
        }
    }
}
