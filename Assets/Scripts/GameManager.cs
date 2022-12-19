using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool set;
    public bool at;
    public int munpis;
    public int munesc;

     void Start()
    {
        GestorDeAudio.instancia.ReproducirSonido("musica");
        GestorDeAudio.instancia.ReproducirSonido("zombie");
        ControlJugador setx = GetComponent<ControlJugador>();
        set = setx.set1;

        ControlJugador MunPist = GetComponent<ControlJugador>();
        munpis = MunPist.munrec;

        ControlJugador MunEsco = GetComponent<ControlJugador>();
        munesc = MunEsco.munrecesc;

        ControlJugador atasc = GetComponent<ControlJugador>();
        at = atasc.atasc;

   
    }

    private void OnTriggerEnter(Collider other)
    {



        if (other.gameObject.CompareTag("Botiquin") == true)
        {

            GestorDeAudio.instancia.ReproducirSonido("botiquin");

        }


    }
    // Update is called once per frame
    void Update()
    {
        if (set == true)
        {
            if (at == false && Input.GetMouseButtonDown(0) &&  munpis > 0)
            {

                GestorDeAudio.instancia.ReproducirSonido("disparo");

            }else if (at == true && Input.GetMouseButtonDown(0) && munpis > 0)
            {
                GestorDeAudio.instancia.ReproducirSonido("sinbala");
            }
        }
        else if (set == false)
        {
            if (Input.GetMouseButtonDown(0) && munesc > 0)
            {
                GestorDeAudio.instancia.ReproducirSonido("disparoesc");
            }
        }
       

    }
}
