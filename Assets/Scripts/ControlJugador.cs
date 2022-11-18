using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlJugador : MonoBehaviour
{
    public float rapidezDesplazamiento = 10.0f;
    public Camera camaraPrimeraPersona;
    public GameObject proyectil;
    public GameObject Municion;

    public TMPro.TMP_Text textoMunicion;
    public TMPro.TMP_Text textoVida;
    public TMPro.TMP_Text textoBotiquin;

    public int municion = 10;
    public int hp;
    public int botiquin = 0;
    void Start()
    {
        GestorDeAudio.instancia.ReproducirSonido("musica");
        textoMunicion.text = "";
        textoVida.text = "";
        textoBotiquin.text = "";
        Cursor.lockState = CursorLockMode.Locked;
        mostrarTextos();
    }

    public void mostrarTextos()
    {
        textoMunicion.text = "Municion: " + municion.ToString();
        textoVida.text = "VIDA: " + hp.ToString();
        textoBotiquin.text = "Botiquines: " + botiquin.ToString();
    }

    private void recibirDaño()
    {
        hp = hp - 25;
        mostrarTextos();
    }

    private void curarse()
    {
        if (hp < 100 && botiquin >= 1) {

            hp = 100;
            botiquin -= 1;
            mostrarTextos();

        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bot"))
        {
            recibirDaño();

        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Municion") == true)
        {
            other.gameObject.SetActive(false);
            municion += 10;
            mostrarTextos();

        }

        if(other.gameObject.CompareTag("Botiquin") == true)
        {
            other.gameObject.SetActive(false);
            GestorDeAudio.instancia.ReproducirSonido("botiquin");
            botiquin += 1;
            mostrarTextos();
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

       if(Input.GetKey(KeyCode.H))
        {
            curarse();

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
