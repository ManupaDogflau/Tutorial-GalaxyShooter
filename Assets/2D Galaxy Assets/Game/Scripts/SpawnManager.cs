using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private GameObject[] powerups;
    private UIManager uimanager;
    // Start is called before the first frame update

    private void Start()
    {
        uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
        empezarrutina();
    }

    public void empezarrutina()
    {
        StartCoroutine(Enemy());
        StartCoroutine(Powerup());
    }
        
    private IEnumerator Enemy()
    {
         while(uimanager.estadoPartida==true)
         {
            Instantiate(enemy, new Vector3(Random.Range(-7f, 7f), 7f, 0f), Quaternion.identity);
            yield return new WaitForSeconds(5f);
         }
    }
    private IEnumerator Powerup()
    {
        while (uimanager.estadoPartida == true)
        {
            int nPowerup = Random.Range(0, 3);
            Instantiate(powerups[nPowerup], new Vector3(Random.Range(-7f, 7f), 7f, 0f), Quaternion.identity);
            yield return new WaitForSeconds(5f);
        }
    }
}
