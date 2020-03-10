using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private GameObject _rotateAsteroid;
    [SerializeField] private float _rotateSpeed = 3.5f;
    [SerializeField] private GameObject _explosionPrefab;

    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if(_spawnManager == null)
        {
            Debug.LogError("Spawn Manager not available!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Laser")
        {                   
            _spawnManager.StartSpawning();

            Destroy(other.gameObject);
            Destroy(this.gameObject);
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            gameObject.GetComponent<Collider2D>().enabled = false;
            
        }
    }
}
