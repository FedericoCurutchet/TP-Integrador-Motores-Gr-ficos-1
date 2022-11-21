using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControlBot : MonoBehaviour
{
    // Start is called before the first frame update
    private int hp; 
    GameObject jugador;
    GameObject target;
    public GameObject sangre;
    public int rapidez; 


    void Start()
    {
        hp = 100;
        target = GameObject.Find("Jugador"); 

        buscarJugador();
    }

    private void buscarJugador()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < 15)
        {
            jugador = GameObject.Find("Jugador");
            transform.LookAt(jugador.transform);
            transform.Translate(rapidez * Vector3.forward * Time.deltaTime);
            GestorDeAudio.instancia.ReproducirSonido("zombie");
        }
    }
    private void Update()
    {
        buscarJugador();
    }
    public void recibirDaño()
    {
        hp = hp - 25;
        sangreBot((float)0.1);
        if (hp <= 0) { 
            this.desaparecer(); 
        }

    }
    public void recibirDañoEsc()
    {
        hp = hp - 100;
        sangreBot((float)0.1);
        if (hp <= 0)
        {
            this.desaparecer();
        }

    }

    public void sangreBot(float segundos)
    {
        Invoke("mostrarSangre",segundos);
        
    }
    public void mostrarSangre()
    {
        GameObject particulas = Instantiate(sangre, transform.position, Quaternion.identity) as GameObject; 
        Destroy(particulas, 2);
    }
    
    private void desaparecer()
    {
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bala"))
        {
            recibirDaño();
          

        }

        if (collision.gameObject.CompareTag("BalaEsc"))
        {
            recibirDañoEsc();


        }

    }

}
    
