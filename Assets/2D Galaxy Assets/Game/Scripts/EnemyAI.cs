using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private GameObject Explosion;
    private UIManager uimanager;
    [SerializeField]
    private AudioClip explosionSound;
    // Start is called before the first frame update
    void Start()
    {
        uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
        Vector3 position = new Vector3(Random.Range(-8f, 8f), 7f, 0f);
        transform.position = (position);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down *speed* Time.deltaTime);
        if (transform.position.y<-7f)
        {
            ReSpawn();
        }
        if (uimanager.estadoPartida == false)
        {
            Instantiate(Explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void ReSpawn()
    {
        Vector3 position = new Vector3(Random.Range(-7f, 7f), 7f, 0f);
        transform.position = (position);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Laser")
        {
            uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();

            if (uimanager != null)
            {
                if (Time.deltaTime * 0.01 < 0.5f)
                {
                    uimanager.updateScore(1 - Time.deltaTime * 0.01f);
                }
                else
                {
                    uimanager.updateScore(0.5f);
                }
            }
            AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, 1f);
            Destroy(collision.gameObject);
        }


        else if (collision.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                if (player.shield == false)
                {
                    player.vidas = player.vidas - 1;
                    uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();

                    if (uimanager != null)
                    {
                        uimanager.updateLives(player.vidas);
                    }
                }
                else
                {
                    player.shield = false;
                    player._Shield.SetActive(false);
                }
            }
        }
        Instantiate(Explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, 1f);
    }
}
