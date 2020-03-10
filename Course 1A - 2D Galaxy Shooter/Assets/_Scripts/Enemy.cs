using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator _anim;

    [Header("Enemy Start Position")]
    [SerializeField] public GameObject _enemyShip;
    [SerializeField] private Vector3 _enemyStart;

    [SerializeField] private float _enemySpeed;

    [SerializeField] private Player _player;

    [Header("Random 'x' Spawn Position")]
    [SerializeField] private float _RespawnPosition;
    [SerializeField] private float _minSpawn;
    [SerializeField] private float _maxSpawn;

    [SerializeField] private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();

        // error checking for components
        if(_player == null)
        {
            Debug.LogError("The Player is not available!");
        }

        _anim = gameObject.GetComponent<Animator>();
        if(_anim == null)
        {
            Debug.LogError("No Animator is available!");
        }

        if(_audioSource == null)
        {
            Debug.LogError("No audiosource on player is Null.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //move down at 4 meters per second
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);

        //respanw at the top of screen
        if (transform.position.y <= _RespawnPosition)
        {
            float randomLocation = Random.Range(_minSpawn, _maxSpawn);
            transform.position = new Vector3(randomLocation, 7, 0);     
        } 
    } 

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Hit : " + other.transform.name);

        if (other.tag == "Player")
        {          
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }

            _audioSource.Play();

            OnEnemyDeathExplosion();
            //Destroy(this.gameObject, 1f);
        }

        if (other.tag == "Laser")
        {

            if(_player != null)
            {
                _player.AddToScore();
            }

            _audioSource.Play();

            OnEnemyDeathExplosion();
            //Destroy(this.gameObject, 1f);
            //Destroy(other.gameObject, 1f);
        }
    }

    private void OnEnemyDeathExplosion()
    {
        _anim.SetTrigger("OnEnemyDeath");
        _enemySpeed = 0;
        gameObject.GetComponent<Collider2D>().enabled = false;
        Destroy(this.gameObject, 2.8f);
    }
}
