using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBot : MonoBehaviour
{
    // Start is called before the first frame update
    private int hp; 
    GameObject jugador;
    GameObject target;
    public int rapidez; 


    void Start()
    {
        hp = 100;
        target = GameObject.Find("Jugador"); 

        buscarJugador();
    }

    private void buscarJugador()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < 20)
        {
            jugador = GameObject.Find("Jugador");
            transform.LookAt(jugador.transform);
            transform.Translate(rapidez * Vector3.forward * Time.deltaTime);
        }
    }
    private void Update()
    {
        buscarJugador();
    }
    public void recibirDaño()
    {
        hp = hp - 25;
        if (hp <= 0) { 
            this.desaparecer(); 
        }
    }
    private void desaparecer()
    {
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("bala"))
        {
            recibirDaño();

        }

    }

}
    
