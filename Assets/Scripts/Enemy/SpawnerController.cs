using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnerController : MonoBehaviour
{
    [SerializeField] public GameObject SpawnerPrefab;
    [SerializeField] public int countSpawner;


    private void Start()
    {
        RandomSpawner();
    }



    
    private void RandomSpawner()
    {

        for (int i = 0; i < countSpawner; i++)
            {
                
                Vector2 pos = new Vector2(UnityEngine.Random.Range(-5f, 5f), UnityEngine.Random.Range(-5f, 5f));
                Instantiate(SpawnerPrefab, pos, Quaternion.identity);
                
                

            }

        
    }
}
