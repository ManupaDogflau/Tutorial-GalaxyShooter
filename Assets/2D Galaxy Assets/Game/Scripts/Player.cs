using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update


    //Movimiento
    [SerializeField]
    private float Speed = 5f;

    //Disparo
    [SerializeField]
    private GameObject laserPrefab;
    [SerializeField]
    private float fireRate = 0.25f;
    private float canFire = 0f;

    //Powerups
    public bool canShootTriple = false;
    public bool shield = false;
    public GameObject _Shield;

    //Enemigos
    [SerializeField]
    public int vidas = 3;

    //Muerte
    [SerializeField]
    private GameObject Explosion;

    private UIManager uimanager;
    private SpawnManager spawnmanager;
    private AudioSource audiosource;

    [SerializeField]
    private GameObject[] dañoAla;
    private int cuentaVidas;
    private int dañoAlaContador = 3;


    void Start()
    {
        spawnmanager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        transform.position = new Vector3(0, 0, 0);
        uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
        audiosource = GetComponent<AudioSource>();
        if (uimanager!= null)
        {
            uimanager.updateLives(vidas);
        }
        spawnmanager.empezarrutina();
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        Movement();
        Vidas();
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
        {
            if (Time.time > canFire)
            {
                
                canFire = Time.time + fireRate;
                Instantiate(laserPrefab, transform.position + new Vector3(0, 0.9f, 0), Quaternion.identity);
                if (canShootTriple == true)
                {
                    Instantiate(laserPrefab, transform.position + new Vector3(0.57f, -0.05f, 0), Quaternion.identity);
                    Instantiate(laserPrefab, transform.position + new Vector3(-0.5f, -0.05f, 0), Quaternion.identity);
                }
                audiosource.Play();
            }
        }
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * Speed);

        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }

        if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }

        if (transform.position.x > 9.5f)
        {
            transform.position = new Vector3(-9.5f, transform.position.y, 0);
        }

        if (transform.position.x < -9.5f)
        {
            transform.position = new Vector3(9.5f, transform.position.y, 0);
        }

    }

    public void TripleShoot()
    {
        canShootTriple = true;
        StartCoroutine(TripleShootPowerdownRutine());
    }
    
    private IEnumerator TripleShootPowerdownRutine()
    {
        yield return new WaitForSeconds(5f);
        canShootTriple = false;
    }

    public void SpeedUp()
    {
        Speed = Speed * 1.5f;
        StartCoroutine(SpeedUpPowerdownRutine());
    }

    private IEnumerator SpeedUpPowerdownRutine()
    {
        yield return new WaitForSeconds(5f);
        Speed = Speed / 1.5f;
    }

    private void Vidas()
    {
        if (cuentaVidas > vidas)
        {
            if (dañoAlaContador==1)
            {
                dañoAla[0].SetActive(true);
            }
            else if (dañoAlaContador == 0)
            {
                dañoAla[1].SetActive(true);
            }
            else
            {
                dañoAlaContador = Random.Range(0, 2);
                dañoAla[dañoAlaContador].SetActive(true);
            }
        }
        if (vidas == 0)
        {
            Instantiate(Explosion,transform.position, Quaternion.identity);
            uimanager.Start();
            uimanager.Partida();
            Destroy(gameObject);
        }
        cuentaVidas = vidas;
    }

    public void Shield()
    {
      shield = true;
      StartCoroutine(ShieldsDown());
      _Shield.SetActive(true);
    }

    private IEnumerator ShieldsDown()
    {
        yield return new WaitForSeconds(10f);
        shield = false;
        _Shield.SetActive(false);
    }
}
