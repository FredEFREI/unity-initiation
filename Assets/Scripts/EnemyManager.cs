using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{   
    private int _maxEnemies = 7;
    public GameObject enemyTypeA;
    public GameObject enemyTypeB;
    public GameObject enemyTypeC;
    public float nextSpawn = 0f;
    
    private float _spawnTime = 7f;
    private int _difficulty = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= 30 && _difficulty == 2)
        {
            _difficulty = 3;
            _spawnTime = 6f;
        }
        if (Time.time >= 60 && _difficulty == 3)
        {
            _difficulty = 4;
            _spawnTime = 5f;
        }
        if (Time.time >= 120 && _difficulty == 4)
        {
            _difficulty = 5;
            _spawnTime = 4f;
        }
        
        if (Time.time >= 160 && _difficulty == 5)
        {
            _difficulty = 5;
            _spawnTime = 3f;
        }
        
        nextSpawn -= Time.deltaTime;
        if (nextSpawn <= 0)
        {
            nextSpawn = _spawnTime;

            if (_maxEnemies > Enemy.nbrEnemies)
            {
                switch (Random.Range(0, _difficulty))
                {
                    case 0:
                        Instantiate(enemyTypeA);
                        Instantiate(enemyTypeA);
                        Instantiate(enemyTypeA);
                        break;
                    case 1:
                        Instantiate(enemyTypeB);
                        Instantiate(enemyTypeB);
                        break;
                    case 2:
                        Instantiate(enemyTypeC);
                        break;
                    case 3:
                        Instantiate(enemyTypeA);
                        Instantiate(enemyTypeB);
                        Instantiate(enemyTypeB);
                        Instantiate(enemyTypeA);
                        break;
                    case 4:
                        Instantiate(enemyTypeC);
                        Instantiate(enemyTypeC);
                        break;
                    default:
                        Instantiate(enemyTypeA);
                        Instantiate(enemyTypeA);
                        Instantiate(enemyTypeB);
                        Instantiate(enemyTypeB);
                        Instantiate(enemyTypeC);
                        break;
                }
            }

        }
    }
}
