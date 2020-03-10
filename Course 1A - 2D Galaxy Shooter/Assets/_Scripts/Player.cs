using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    //public or private references
    // data type (int, float, bool or string)
    // every varialbe has a unique _name
    // every variable can have a optional value assigned

    //setup player movements
    public float horizontalInput;
    public float verticalInput;

    [Header("Player Constriants")]
    public float minXPoint;
    public float maxXPoint;
    public float minYPoint;
    public float maxYPoint;

    [Header("Player Move Variables")]
    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _speedMultiplier = 2;
    [SerializeField] private int _lives = 3;

    [SerializeField] private int _score;
    [SerializeField] UI_Manager _ui_Manager;

    [Header("Enter Player's Start position")]
    public Vector3 player;

    [Header("Game Objects")]
    [SerializeField] private GameObject _laser;
    [SerializeField] private GameObject _tripleShot;

    [SerializeField] private GameObject _shieldVisualizer;

    [Header("Fire Control System")]    
    [SerializeField] private KeyCode _fireKey = KeyCode.None;
    [SerializeField] private string _fireKeyRapid;

    [Header("Laser Speed Variables")]
    [SerializeField] private float _laserOffset;
    [SerializeField] private float _fireRate = 0.5f;
    [SerializeField] private float _fireRateRapid = 0.1f;
    [SerializeField] private float _fireRateRapidNextFire = 1f;
    [SerializeField] private float _nextFire = -1f;

    [Header("Enable TripleShot")]
    [SerializeField] private bool _isTripleShotActive = false;

    [Header("Enable Speed boost")]
    [SerializeField] private bool _isSpeedBoostActive = false;

    [Header("Enable Power Sheid")]
    [SerializeField] private bool _isPowerShieldActive = false;

    [Header("Damage Control")]
    [SerializeField] private GameObject leftThruster;
    [SerializeField] private GameObject rightThruster;

    [Header("Audio Controller")]
    [SerializeField] private AudioClip _lasserSoundClip;
    [SerializeField] private AudioSource _audioSource;

    
    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(player.x, player.y, player.z);

        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _ui_Manager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        _audioSource = GetComponent<AudioSource>();

        if(_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL.");
        }

        if(_ui_Manager == null)
        {
            Debug.Log("The UI Manager is NULL.");
        }

        if(_audioSource == null)
        {
            Debug.LogError("The audiosource on the player is NULL.");
        }
        else
        {
            _audioSource.clip = _lasserSoundClip;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if(Input.GetKeyDown(KeyCode.Space) && Time.time >= _nextFire)
        {
            FireControl();

        } else if(Input.GetKeyDown(KeyCode.X) && Time.time >= _fireRateRapid)
        {
            RapidFireControl();
        }
    }    

    private void CalculateMovement()
    {
        //using horizontal defined keys to move player left or right
        //**********************************************************
        horizontalInput = Input.GetAxis("Horizontal");

        //using vertical defined keys to move player up and down
        //******************************************************
        verticalInput = Input.GetAxis("Vertical");

        //using horizontal and vertical defined keys in a single variable
        //***************************************************************
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _playerSpeed * Time.deltaTime);        

        //using math.clamp to contrain player's movement
        //**********************************************
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minXPoint, maxXPoint), 
                                        Mathf.Clamp(transform.position.y, minYPoint, 0f), 0);
    }

    private void RapidFireControl()
    {
        _fireRateRapidNextFire = Time.time + _fireRateRapid;
        Instantiate(_laser, transform.position + new Vector3(0, _laserOffset, 0), Quaternion.identity);
        _audioSource.Play();
    }

    private void FireControl()
    {
    
        if (_isTripleShotActive == false)
        {
            _nextFire = Time.time + _fireRate;
            Instantiate(_laser, transform.position + new Vector3(0, _laserOffset, 0), Quaternion.identity);
        }
        else
        {
            TripleShotIsActive();
            Instantiate(_tripleShot, transform.position + new Vector3(0, _laserOffset, 0), 
                Quaternion.identity);
        }

        _audioSource.Play();
    }

    public void Damage()
    {
        if(_isPowerShieldActive == true)
        {
            //_isPowerShieldActive = false;
            return;
        } else
        {
            if(_isPowerShieldActive == true)
            {
                _isPowerShieldActive = false;
                _shieldVisualizer.SetActive(false);
                return;
            }

            //remove one life everything player dies
            _lives--;

            if(_lives == 2)
            {
                leftThruster.gameObject.SetActive(true);
            }
            else if(_lives == 1)
            {
                rightThruster.gameObject.SetActive(true);
            }

            //remove image
            _ui_Manager.UpDateLives(_lives);

            if (_lives < 1)
            {
                //Communicate with Spawn Manager
                _spawnManager.OnPlayerDeath();

                //let
                Destroy(this.gameObject);

            }
        }       
    }

    public void TripleShotIsActive()
    {
        _isTripleShotActive = true;
        _nextFire = Time.time + _fireRate;

        StartCoroutine(TripleShotCountDownRoutine());     
    }
    
    public void SpeedBoastIsActive()
    {
        _isSpeedBoostActive = true;
        _playerSpeed *= _speedMultiplier;

        StartCoroutine(SpeedboastCountDownRoutine());
    }    

    public void PowerShieldIsActive()
    {
        _isPowerShieldActive = true;
        _isPowerShieldActive = true;
        _shieldVisualizer.SetActive(true);

        StartCoroutine(PowerShieldCountDownRoutine());
    }

    public void AddToScore()
    {
        _score += 10;
        _ui_Manager.UpDateScore(_score);
    }

    IEnumerator TripleShotCountDownRoutine()
    {
        yield return new WaitForSeconds(5f);

        _isTripleShotActive = false;
    }

    IEnumerator SpeedboastCountDownRoutine()
    {
        yield return new WaitForSeconds(5f);

        _isSpeedBoostActive = false;
        _playerSpeed = 3;
    }

    IEnumerator PowerShieldCountDownRoutine()
    {
        yield return new WaitForSeconds(8f);

        _isSpeedBoostActive = false;
        _isPowerShieldActive = false;
        _shieldVisualizer.SetActive(false);
    }
}
