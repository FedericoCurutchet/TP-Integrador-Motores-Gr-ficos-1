using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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
    public GameObject Jugador;

    public GameObject Mapa;
    public GameObject Combustible;
    public GameObject Tarjeta;
    public GameObject LlaveAuto;

    public Light LuzLinterna;

    public bool luzactivada = false;
    public bool correr;
    public bool set1 = true;

    public TMPro.TMP_Text textoMunicion;
    public TMPro.TMP_Text textoVida;
    public TMPro.TMP_Text textoBotiquin;
    public TMPro.TMP_Text textoObjetos;
    public TMPro.TMP_Text textoMapa;
    public TMPro.TMP_Text textoLlave;
    public TMPro.TMP_Text textoTarjeta;
    public TMPro.TMP_Text textoComb;
    public TMPro.TMP_Text textoHUD;
    public TMPro.TMP_Text textoPerdiste;
    public TMPro.TMP_Text textoCreditos;
    public RawImage PistolaLogo;
    public RawImage EscopetaLogo;


    public int valmaxp = 10;
    public int valmaxe = 6;
    public int munrec = 10;
    public int munrecesc = 6;
    public int municion = 0;
    public int municionesc = 0;
    public int hp;
    public int botiquin = 0;
    public int obj = 4;
    public float posicionY;
    void Start()
    {
        PistolaLogo.gameObject.SetActive(true);
        EscopetaLogo.gameObject.SetActive(false);
        Linterna2.SetActive(false);
        Escopeta.SetActive(false);
        GestorDeAudio.instancia.ReproducirSonido("musica");
        textoMunicion.text = "";
        textoVida.text = "";
        textoBotiquin.text = "";
        textoObjetos.text = "";
        textoMapa.text = "";
        textoLlave.text = "";
        textoTarjeta.text = " ";
        textoComb.text = "";
        textoHUD.text = "";
        textoPerdiste.text = "";
        textoCreditos.text = "";
        Cursor.lockState = CursorLockMode.Locked;
        mostrarTextos();
    }

    public void mostrarTextos()
    {
        if (set1 == true)
        {
            textoMunicion.text = munrec.ToString() + "|" + municion.ToString();
            PistolaLogo.gameObject.SetActive(true);
            EscopetaLogo.gameObject.SetActive(false);

        } else if (set1 == false)
        {
            textoMunicion.text = munrecesc.ToString() + "|" + municionesc.ToString();
            PistolaLogo.gameObject.SetActive(false);
            EscopetaLogo.gameObject.SetActive(true);
        }

        textoVida.text = "VIDA: " + hp.ToString();
        textoBotiquin.text = "Botiquines: " + botiquin.ToString();
        textoObjetos.text = "Objetos Restantes: " + obj.ToString();
        textoMapa.text = "Mapa";
        textoLlave.text = "Llave del Auto";
        textoTarjeta.text = "Tarjeta Garaje";
        textoComb.text = "Combustible";
        textoHUD.text = " 1 Pistola| 2 Escopeta| F Linterna | H Botiquin";
    }
    private void Recagar()
    {
        int numaux;
        numaux = valmaxp - munrec;

        if (municion > 0) {
            if (munrec == 0)
            {

                if (municion >= 10)
                {
                    munrec = valmaxp;
                    municion -= valmaxp;

                }
                else if (municion < valmaxp)
                {
                    munrec = municion;
                    municion -= municion;
                }
            }
            else if (munrec >= 1 || munrec <= 9 && municion > numaux)
            {
                numaux = valmaxp - munrec;
                munrec += numaux;
                municion -= numaux;
            }
        }

        mostrarTextos();

    }

    private void RecargarEsc()
    {
        int numaux;
        if (municionesc > 0)
        {
            if (munrecesc == 0)
            {

                if (municionesc >= 10)
                {
                    munrecesc = valmaxe;
                    municionesc -= valmaxe;

                }
                else if (municionesc < valmaxe)
                {
                    munrecesc = municionesc;
                    municionesc -= municionesc;
                }
            }
            else if (munrecesc >= 1 || munrecesc <= 5 && municionesc > valmaxe - municionesc)
            {
                numaux = valmaxe - munrecesc;
                munrecesc += numaux;
                municionesc -= numaux;
            }
        }

        mostrarTextos();
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

    private void Correr()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {

            rapidezDesplazamiento = 10.0f;


        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            rapidezDesplazamiento = 5.0f;
        }
    }

    private void Despacio()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {

            rapidezDesplazamiento = 2.0f;


        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            rapidezDesplazamiento = 5.0f;
        }
    }
 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bot"))
        {
            recibirDaño();

        }

    }
   /* public void Apuntar()
    {
        if (Input.GetMouseButtonDown(1))
        {
            camaraPrimeraPersona.fieldOfView = 15;
            Escopeta.transform.position = new Vector3(0f, -0.4619999f, 1.293003f);
           

        }
        if (Input.GetMouseButtonUp(1))
        {
            camaraPrimeraPersona.fieldOfView = 60;
            Escopeta.transform.position = new Vector3(0.4109993f, -0.4619999f, 1.293003f);
        }
    } */
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


        if (other.gameObject.CompareTag("Combustible") == true)
        {
            
            other.gameObject.SetActive(false);
            obj -= 1;
            
            mostrarTextos();
            textoComb.text = "Combustible (X)";
            textoComb.color = Color.green;
        }

        if (other.gameObject.CompareTag("Mapa") == true)
        {

            other.gameObject.SetActive(false);
            obj -= 1;

            mostrarTextos();
            textoMapa.text = "Mapa (X)";
            textoMapa.color = Color.green;
        }
        if (other.gameObject.CompareTag("Llave") == true)
        {

            other.gameObject.SetActive(false);
            obj -= 1;

            mostrarTextos();
            textoLlave.text = "Llave Del Auto (X)";
            textoLlave.color = Color.green;
        }
        if (other.gameObject.CompareTag("Tarjeta") == true)
        {

            other.gameObject.SetActive(false);
            obj -= 1;

            mostrarTextos();
            textoTarjeta.text = "Tarjeta Garaje (X)";
            textoTarjeta.color = Color.green;
        }

        if((other.gameObject.CompareTag("Salida") == true && obj == 0))
        {

            Ganaste();
            

        }

    }

    public void Disparar() {
        if (set1 == true)
        {

            if (Input.GetMouseButtonDown(0) && munrec > 0)
            {
               
                Ray ray = camaraPrimeraPersona.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                GameObject pro;
                pro = Instantiate(proyectil, ray.origin, transform.rotation);
                Rigidbody rb = pro.GetComponent<Rigidbody>();
                rb.AddForce(camaraPrimeraPersona.transform.forward * 80, ForceMode.Impulse);
                GestorDeAudio.instancia.ReproducirSonido("disparo");
                munrec -= 1;
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
            if (Input.GetMouseButtonDown(0) && munrecesc > 0)
            {
                GestorDeAudio.instancia.ReproducirSonido("disparoesc");
                Ray ray = camaraPrimeraPersona.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                GameObject proesc;
                proesc = Instantiate(proyectilEscopeta, ray.origin, transform.rotation);
                Rigidbody rb = proesc.GetComponent<Rigidbody>();
                rb.AddForce(camaraPrimeraPersona.transform.forward * 50, ForceMode.Impulse);
                munrecesc -= 1;
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

    private void Reinicio()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Ganaste()
    {

        if (Input.GetKey(KeyCode.R))
        {
            Reinicio();

        }
        textoMunicion.text = "";
        textoVida.text = "";
        textoBotiquin.text = "";
        textoObjetos.text = "";
        textoMapa.text = "";
        textoLlave.text = "";
        textoTarjeta.text = " ";
        textoComb.text = "";
        textoHUD.text = "";
        textoCreditos.text = "GANASTEEEE \n" +
            "\n" +
            "\n" +
            "\n" +
            "GRACIAS POR JUGAR \n" +
            "Este juego fue basado y diseñado en base a las clases del profesor  Sebastian Gabriel Blanco \n" +
            "Para la creacion de este juego se utilizaron los siguientes assets: \n" +
            "Ammo de Arcsine Technologies \n" +
            "First-Aid Set de GeeKay 3D \n" +
            "Guns Pack: Low Poly Guns Collection de Fun Assets \n" +
            "GAZ Street Props de Helsssoo \n" +
            "Un videojuego de Federico Curutchet \n";
      
        Time.timeScale = 0;
       
        
    }

    private void Perdiste()
    {
        textoPerdiste.text = "GAME OVER \n" +
            "\n" +
            "\n" +
            "Pulsa R para volver a empezar.";
        Time.timeScale = 0;
        mostrarTextos();
    }


    void Update()
    {
        

        float movimientoAdelanteAtras = Input.GetAxis("Vertical") * rapidezDesplazamiento;
        float movimientoCostados = Input.GetAxis("Horizontal") * rapidezDesplazamiento;
        movimientoAdelanteAtras *= Time.deltaTime; movimientoCostados *= Time.deltaTime;
        transform.Translate(movimientoCostados, 0, movimientoAdelanteAtras);


        if (hp <= 0)
        {
            Perdiste();

            if (Input.GetKey(KeyCode.R))
            {
                Reinicio();

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

        if (set1 == true && Input.GetKey(KeyCode.R))
        {
            Recagar();
        }else if (set1 == false && Input.GetKey(KeyCode.R))
        {
            RecargarEsc();
        }

        Despacio();
        Correr();
        Armas();
        Disparar();
        //Apuntar();
       
        
    }
}
