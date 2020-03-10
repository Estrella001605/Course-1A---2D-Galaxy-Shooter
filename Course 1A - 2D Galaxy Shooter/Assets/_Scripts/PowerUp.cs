using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float _speed;

    //ID for powerup
    //0 = Triple Shot
    //1 = Speed Burst
    //2 = shields
    [SerializeField] private int _powerID;

    //private AudioSource _audioSource;
    [SerializeField] private AudioClip _clip;
    [Range(0.0f, 1.0f)]
    [SerializeField] public float volume = 0.1f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, _speed * Time.deltaTime * (-1), 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            AudioSource.PlayClipAtPoint(_clip, transform.position, volume);

            if(player != null)
            {
                switch (_powerID)
                {
                    case 0:
                        player.TripleShotIsActive();
                        Debug.Log("Triple shot collected...");
                        break;
                    case 1:
                        player.SpeedBoastIsActive();
                        Debug.Log("Speed Boast shot collected...");
                        break;
                    case 2:
                        player.PowerShieldIsActive();
                        Debug.Log("Power Shield shot collected...");
                        break;
                    default:
                        Debug.Log("Dafault Vale...");
                        break;
                }                
            }

            Destroy(this.gameObject);
        }
    }

}
