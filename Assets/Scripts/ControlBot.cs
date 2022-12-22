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
    public int Deteccion;
    public static int Deteccion_Normal;
    public static int Deteccion_Correr;
    public static int Deteccion_Agacahrse;


    void Start()
    {
        hp = 100;
        target = GameObject.Find("Jugador");
        Deteccion = 15;
        Deteccion_Normal = 15;
        Deteccion_Correr = 25;
        Deteccion_Agacahrse = 5;


        buscarJugador();
    }

    private void buscarJugador()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < Deteccion)
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

        if (Deteccion < 10000000 && Input.GetKeyDown(KeyCode.LeftShift))
        {

            Deteccion = Deteccion_Correr;


        }
        else if (Deteccion < 10000000 && Input.GetKeyUp(KeyCode.LeftShift))
        {
            Deteccion = Deteccion_Normal;
        }

        if (Deteccion < 10000000 && Input.GetKeyDown(KeyCode.C))
        {

            Deteccion = Deteccion_Agacahrse;


        }
        else if (Deteccion < 10000000 && Input.GetKeyUp(KeyCode.C))
        {
            Deteccion = Deteccion_Normal;
        }

    }
    public void recibirDaño()
    {
        hp = hp - 25;
        sangreBot((float)0.1);
        if (hp <= 0) { 
            this.desaparecer();
            ControlJugador.enem_elim += 1;
            ControlJugador.lvl = true;
        }

    }
    public void recibirDañoEsc()
    {
        hp = hp - 100;
        sangreBot((float)0.1);
        if (hp <= 0)
        {
            this.desaparecer();
            ControlJugador.enem_elim += 1;
            ControlJugador.lvl = true;
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
            Deteccion = 10000000;

        }

        if (collision.gameObject.CompareTag("BalaEsc"))
        {
            recibirDañoEsc();
            Deteccion = 10000000;

        }

    }

}
    
