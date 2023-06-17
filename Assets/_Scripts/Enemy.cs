using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] GameObject hitVFX;


    
    [SerializeField] int scorePerHit = 2;
    [SerializeField] int health = 4;

    ScoreBoard scoreBoard;
    GameObject parentGameObject;

    private void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
        AddRigidBody();
    }

    private void OnParticleCollision(GameObject other)
    {
        HitEnemy(other);
        ProcessHit();
    }


    private void HitEnemy(GameObject other) 
    {
        if(health <= 0) 
        {
            KillEnemy(other);
        }
        else 
        {
            GameObject hit = Instantiate(hitVFX, transform.position, Quaternion.identity);
            health -= scorePerHit;
        }
        

    }
  
    private void KillEnemy(GameObject other)
    {
  
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);

        fx.transform.parent = parentGameObject.transform;

        Debug.Log($"{this.name} hit by {other.name}");

        Destroy(this.gameObject);
    }

    private void ProcessHit()
    {
        scoreBoard.IncreaseScore(scorePerHit);
        scoreBoard.PrintScore();
    }

    private void AddRigidBody() 
    {
      Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
    }
}
