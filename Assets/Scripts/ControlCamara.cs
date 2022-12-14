using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCamara : MonoBehaviour
{
    Vector2 mouseMirar;
    Vector2 suavidadV;
    public float sensibilidad = 3.0f;
    public float suavizado = 1.0f;
    GameObject Jugador;
    GameObject Pistola;
    public Camera camaraPrimeraPersona;
    public GameObject Escopeta;


    void Start()
    {
        Jugador = this.transform.parent.gameObject;
    }

    public void Apuntar()
    {
        if (Input.GetMouseButtonDown(1))
        {
            camaraPrimeraPersona.fieldOfView = 15;
            


        }
        if (Input.GetMouseButtonUp(1))
        {
            camaraPrimeraPersona.fieldOfView = 60;
           
        }
    }
    void Update()
    {
        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        md = Vector2.Scale(md, new Vector2(sensibilidad * suavizado, sensibilidad * suavizado));
        suavidadV.x = Mathf.Lerp(suavidadV.x, md.x, 1f / suavizado);
        suavidadV.y = Mathf.Lerp(suavidadV.y, md.y, 1f / suavizado);
        mouseMirar += suavidadV; mouseMirar.y = Mathf.Clamp(mouseMirar.y, -90f, 90f);
        transform.localRotation = Quaternion.AngleAxis(-mouseMirar.y, Vector3.right);
        Jugador.transform.localRotation = Quaternion.AngleAxis(mouseMirar.x, Jugador.transform.up);

        Apuntar();
    }

}
