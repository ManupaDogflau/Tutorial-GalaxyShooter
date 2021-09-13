using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    float _speed = 3f;
    [SerializeField]
    int _powerUpID; //0-tripleShoot 1-SpeedUp 2-Shield
    private UIManager uimanager;
    [SerializeField]
    private AudioClip PowerUpSound;

    // Start is called before the first frame update
    void Start()
    {
        uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _powerUpID = 2;
        if (tag == "TripleShootPowerup")
        {
            _powerUpID = 0;
        }
        else if (tag == "SpeedPowerup")
        {
            _powerUpID = 1;
        }
        else if (tag == "ShieldPowerup")
        {
            _powerUpID = 2;
        }


    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y<-7)
        {
            Destroy(gameObject);
        }

        if (uimanager.estadoPartida == false)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();

            if (player != null)
            {
                if (_powerUpID == 0)
                {
                    player.TripleShoot();
                }
                else if (_powerUpID == 1)
                {
                    player.SpeedUp();
                }
                else if (_powerUpID == 2)
                {
                    player.Shield();
                }
                uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();

                if (uimanager != null)
                {
                    uimanager.updateScore(0.5f);
                }
                AudioSource.PlayClipAtPoint(PowerUpSound, Camera.main.transform.position, 1f);
                Destroy(gameObject);
                
            }
        }
    }
}
