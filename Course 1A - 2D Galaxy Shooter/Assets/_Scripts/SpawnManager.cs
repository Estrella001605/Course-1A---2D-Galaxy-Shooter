using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Enemy Objects")]
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;

    [Header("Power Up Array")]
    [SerializeField] private GameObject[] _powerUps;

    [Header("Enemy Spawn Settings")]
    [SerializeField] private int numberOfEnemies;
    [SerializeField] private float timeToSpawn; 

    [SerializeField] private bool _stopSpawning = false;

    [SerializeField] private float _SpawnPowerUpTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUp());
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
        timeToSpawn = 0;

        // destroy all enemy prefabs
        // Destroy(gameObject);

        StopCoroutine(SpawnEnemyRoutine());

        StopCoroutine(SpawnPowerUp());
    }


    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3f);

        while (_stopSpawning == false)
        {
            Vector3 postToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEmeny = Instantiate(_enemyPrefab, postToSpawn, Quaternion.identity);
            newEmeny.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(timeToSpawn);
        }      
    }

    IEnumerator SpawnPowerUp()
    {
        yield return new WaitForSeconds(3f);

        while (_stopSpawning == false)
        {
            Vector3 postToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);

            int RandomPowerUp = Random.Range(0, _powerUps.Length);
            Instantiate(_powerUps[RandomPowerUp], postToSpawn, Quaternion.identity);
            //
            yield return new WaitForSeconds(Random.Range(3, 10));
        }
    }    
}
