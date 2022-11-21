using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlJugador : MonoBehaviour
{
    public float rapidezDesplazamiento = 5.0f;
    public Camera camaraPrimeraPersona;
    public GameObject proyectil;
    public GameObject proyectilEscopeta;
    public GameObject Municion;
    public GameObject Escopeta;
    public GameObject Pistola;
    public GameObject Linterna;
    public GameObject Linterna2;

    public Light LuzLinterna;

    public bool luzactivada = false;
    public bool correr;
    public bool set1 = true;

    public TMPro.TMP_Text textoMunicion;
    public TMPro.TMP_Text textoVida;
    public TMPro.TMP_Text textoBotiquin;
    public TMPro.TMP_Text textoObjetos;

    public int municion = 10;
    public int municionesc = 6;
    public int hp;
    public int botiquin = 0;
    public int obj = 4;
    void Start()
    {
        Linterna2.SetActive(false);
        Escopeta.SetActive(false);
        GestorDeAudio.instancia.ReproducirSonido("musica");
        textoMunicion.text = "";
        textoVida.text = "";
        textoBotiquin.text = "";
        textoObjetos.text = "";
        Cursor.lockState = CursorLockMode.Locked;
        mostrarTextos();
    }

    public void mostrarTextos()
    {
        if(set1 == true)
        {
          textoMunicion.text = "Municion Pistola: " + municion.ToString();
        } else if (set1 == false)
        {
            textoMunicion.text = "Municion Escopeta: " + municionesc.ToString();
        }
        
        textoVida.text = "VIDA: " + hp.ToString();
        textoBotiquin.text = "Botiquines: " + botiquin.ToString();
        textoObjetos.text = "Objetos Restantes: " + obj.ToString();
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
        if (other.gameObject.CompareTag("Municionesc") == true)
        {
            other.gameObject.SetActive(false);
            municionesc += 6;
            mostrarTextos();

        }

        if (other.gameObject.CompareTag("Botiquin") == true)
        {
            GestorDeAudio.instancia.ReproducirSonido("botiquin");
            other.gameObject.SetActive(false);
            botiquin += 1;
            mostrarTextos();
        }


        if (other.gameObject.CompareTag("Objetos") == true)
        {
            
            other.gameObject.SetActive(false);
            obj -= 1;
            mostrarTextos();
        }


    }

    public void Disparar() {
        if (set1 == true)
        {

            if (Input.GetMouseButtonDown(0) && municion > 0)
            {
                GestorDeAudio.instancia.ReproducirSonido("disparo");
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
        else if (set1 == false)
        {
            if (Input.GetMouseButtonDown(0) && municionesc > 0)
            {
                GestorDeAudio.instancia.ReproducirSonido("disparoesc");
                Ray ray = camaraPrimeraPersona.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                GameObject proesc;
                proesc = Instantiate(proyectilEscopeta, ray.origin, transform.rotation);
                Rigidbody rb = proesc.GetComponent<Rigidbody>();
                rb.AddForce(camaraPrimeraPersona.transform.forward * 50, ForceMode.Impulse);
                municionesc -= 1;
                mostrarTextos();
                Destroy(proesc, 5);
                RaycastHit hit;


                if ((Physics.Raycast(ray, out hit) == true))
                {

                    if (hit.collider.name.Substring(0, 3) == "Bot")
                    {
                        GameObject objetoTocado = GameObject.Find(hit.transform.name);
                        ControlBot scriptObjetoTocado = (ControlBot)objetoTocado.GetComponent(typeof(ControlBot));
                        if (scriptObjetoTocado != null)
                        {
                            scriptObjetoTocado.recibirDañoEsc();
                        }
                    }
                }

            }
        }
    }



    public bool Armas()
    {
        
        if (Input.GetKey(KeyCode.Alpha1))
        {
            Pistola.SetActive(true);
            Linterna.SetActive(true);
            Escopeta.SetActive(false);
            Linterna2.SetActive(false);
            textoMunicion.text = "Municion Pistola: " + municion.ToString();
            mostrarTextos();
            set1 = true;

        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            Pistola.SetActive(false);
            Linterna.SetActive(false);
            Escopeta.SetActive(true);
            Linterna2.SetActive(true);
            textoMunicion.text = "Municion Escopeta: " + municionesc.ToString();
            mostrarTextos();
            set1 = false;
        }

        return set1;
    }


    void Update()
    {
        

        float movimientoAdelanteAtras = Input.GetAxis("Vertical") * rapidezDesplazamiento;
        float movimientoCostados = Input.GetAxis("Horizontal") * rapidezDesplazamiento;
        movimientoAdelanteAtras *= Time.deltaTime; movimientoCostados *= Time.deltaTime;
        transform.Translate(movimientoCostados, 0, movimientoAdelanteAtras);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            correr = !correr;
            if (correr == true)
            {
                rapidezDesplazamiento = 10.0f;
            }

            if (correr == false)
            {
                rapidezDesplazamiento = 5.0f;
            }
           

        }


        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;

        }

       if(Input.GetKey(KeyCode.H))
        {
            curarse();

        }

        

        if (Input.GetKey(KeyCode.F))
        {
            luzactivada = !luzactivada;
            if (luzactivada == true)
            {
                LuzLinterna.enabled = true;
            }

            if (luzactivada == false)
            {
                LuzLinterna.enabled = false;
            }

        }

        Armas();
        Disparar();

       
        
    }
}
