using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_Script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 3 * Time.deltaTime * (-1), 0);
    }
}
