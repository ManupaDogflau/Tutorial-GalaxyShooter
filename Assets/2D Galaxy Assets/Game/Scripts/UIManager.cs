using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Sprite[] lives;
    public Image livesDisplay;
    public Text scoreText;
    private float score;
    private float tiempoPartida;
    public bool estadoPartida;
    public Image Inicio;
    public GameObject Jugador;
    

    // Start is called before the first frame update
    public void Start()
    {
        
        estadoPartida = false;

    }
    public void Update()
    {
        Partida();
        tiempoPartida += Time.deltaTime;

    }
    public void updateLives(int currentlives)
    {
        livesDisplay.sprite = lives[currentlives];
    }

    // Update is called once per frame
   public void updateScore(float multiplicador)
    {
        score = Mathf.Round (score + 10* Mathf.Round(tiempoPartida)* multiplicador);
        scoreText.text = "Score: " + score;
    }
    public void Partida()
    {
        
        if (estadoPartida == false)
        {
            Inicio.enabled = true;
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
            {
                Inicio.enabled = false;
                Instantiate(Jugador, transform.position, Quaternion.identity);
                estadoPartida = true;
                score = 0;
                updateScore(0);
                scoreText.text = "Score: " + score;

            }
        }
    }
}
