using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlJugador : MonoBehaviour
{
    public float rapidezDesplazamiento = 10.0f;
    public Camera camaraPrimeraPersona;
    public GameObject proyectil;
    public int municion = 10;
    public TMPro.TMP_Text textoMunicion;
    void Start()
    {
        textoMunicion.text = "";
        Cursor.lockState = CursorLockMode.Locked;
        mostrarTextos();
    }

    public void mostrarTextos()
    {
        textoMunicion.text = "Municion: " + municion.ToString();
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
                Debug.Log("El rayo toc� al objeto: " + hit.collider.name); 
                if (hit.collider.name.Substring(0, 3) == "Bot") { 
                    GameObject objetoTocado = GameObject.Find(hit.transform.name);
                    ControlBot scriptObjetoTocado = (ControlBot)objetoTocado.GetComponent(typeof(ControlBot));
                    if (scriptObjetoTocado != null) { 
                        scriptObjetoTocado.recibirDa�o(); 
                    } 
                }
            }

        }

    }

}
