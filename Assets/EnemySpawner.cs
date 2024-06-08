using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject Prefab;

    [SerializeField]
    private float minSpawnTime;

    [SerializeField]
    private float maxSpawnTime;

    private float TimeUntilSpawn;
    public uint Amount = 5;

    private void Awake()
    {
        SetTimeUntilSpawn();    
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Amount > 0)
        {
            TimeUntilSpawn -= Time.deltaTime;
            if (TimeUntilSpawn <= 0)
            {
                Instantiate(Prefab, transform.position, Quaternion.identity);
                SetTimeUntilSpawn();
                Amount--;
            }
        }

        if (Amount == 0) { 
            Destroy(gameObject);
        }
    }

    private void SetTimeUntilSpawn (){
        TimeUntilSpawn = Random.Range(minSpawnTime, maxSpawnTime);
    }
}
