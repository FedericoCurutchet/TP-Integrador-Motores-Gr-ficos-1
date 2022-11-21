using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
     void Start()
    {
        GestorDeAudio.instancia.ReproducirSonido("musica");
        GestorDeAudio.instancia.ReproducirSonido("zombie");
       

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
        if (Input.GetMouseButtonDown(0))
        {

            GestorDeAudio.instancia.ReproducirSonido("disparo");
           
        } else if (Input.GetMouseButtonDown(0))
        {
            GestorDeAudio.instancia.ReproducirSonido("disparoesc");
        }

    }
}
