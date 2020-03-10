using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    //speed variable of 8
    [Header("Laser Settings")]
    [SerializeField] private float _laserSpeed = 8;
    [SerializeField] private float _laserDestroyGate;
    [SerializeField] private float _laserTimeToDestroy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //translate laser up using Vector.up at 8 meters per second
        transform.Translate(Vector3.up * _laserSpeed * Time.deltaTime);

        if(transform.position.y > _laserDestroyGate)
        {
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject, _laserTimeToDestroy);
        }
    }
}
