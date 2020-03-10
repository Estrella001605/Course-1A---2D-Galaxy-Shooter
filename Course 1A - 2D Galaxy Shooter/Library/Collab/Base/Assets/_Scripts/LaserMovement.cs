using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMovement : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] private GameObject _laser;

    [SerializeField] private float _laserSpeed = 8f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(_laser, transform.position, Quaternion.identity);
        }
    }
}
