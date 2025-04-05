using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] targetPrefabs;

    [SerializeField]
    private Transform[] spawnPoints;

    private List<GameObject> activeTargets = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        SpawnTargets();
    }

    public void SetTargets(GameObject target){
        if(!activeTargets.Contains(target)){
            activeTargets.Add(target);
        }
    }

    public void RemoveTargets(GameObject target){
        if(activeTargets.Contains(target)){
            activeTargets.Remove(target);

            if(activeTargets.Count == 0){
                Invoke(nameof(SpawnTargets), 2f);
            }
        }
    }

    public void SpawnTargets(){
        foreach(Transform point in spawnPoints){
            int index = Random.Range(0, targetPrefabs.Length);
            GameObject prefab = targetPrefabs[index];

            GameObject target = Instantiate(prefab, point.position, point.rotation);
            SetTargets(target);
        }
    }
}
