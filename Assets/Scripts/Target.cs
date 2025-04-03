using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private int scoreValue = 10;
    public int ScoreValue{
        get{
            return scoreValue;
        }
        set{
            scoreValue = value;
        }
    }

    [SerializeField]
    private ScoreManager scoreManager;

    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.CompareTag("Bullet")){
            scoreManager.Addscore(scoreValue);
            Destroy(gameObject);
        }
    }
}
