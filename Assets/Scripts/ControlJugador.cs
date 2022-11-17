using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlJugador : MonoBehaviour
{
    public float rapidezDesplazamiento = 10.0f;
    public Camera camaraPrimeraPersona;
    public GameObject proyectil;
    
    public TMPro.TMP_Text textoMunicion;
    public TMPro.TMP_Text textoVida;

    public int municion = 10;
    public int hp;
    void Start()
    {
        GestorDeAudio.instancia.ReproducirSonido("musica");
        textoMunicion.text = "";
        textoVida.text = "";
        Cursor.lockState = CursorLockMode.Locked;
        mostrarTextos();
    }

    public void mostrarTextos()
    {
        textoMunicion.text = "Municion: " + municion.ToString();
        textoVida.text = "VIDA: " + hp.ToString();
    }
    
    private void recibirDaño()
    {
        hp = hp - 25;
        mostrarTextos();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bot"))
        {
            recibirDaño();

        }
    }
    void Update()
    {

       
        float movimientoAdelanteAtras = Input.GetAxis("Vertical") * rapidezDesplazamiento;
        float movimientoCostados = Input.GetAxis("Horizontal") * rapidezDesplazamiento;
        movimientoAdelanteAtras *= Time.deltaTime; movimientoCostados *= Time.deltaTime;
        transform.Translate(movimientoCostados, 0, movimientoAdelanteAtras);
        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;

        }

        if (Input.GetMouseButtonDown(0) && municion > 0)
        {
            Ray ray = camaraPrimeraPersona.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            GameObject pro;
            pro = Instantiate(proyectil, ray.origin, transform.rotation);
            Rigidbody rb = pro.GetComponent<Rigidbody>();
            rb.AddForce(camaraPrimeraPersona.transform.forward * 80, ForceMode.Impulse);
            municion -= 1;
            mostrarTextos();
            Destroy(pro, 5);

            RaycastHit hit;

            if ((Physics.Raycast(ray, out hit) == true))
            {
                
                if (hit.collider.name.Substring(0, 3) == "Bot")
                {
                    GameObject objetoTocado = GameObject.Find(hit.transform.name);
                    ControlBot scriptObjetoTocado = (ControlBot)objetoTocado.GetComponent(typeof(ControlBot));
                    if (scriptObjetoTocado != null)
                    {
                        scriptObjetoTocado.recibirDaño();
                    }
                }
            }

        }
    }
}
