using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class GestorDeAudio : MonoBehaviour
{


    public Sonido[] sonidos;
    public static GestorDeAudio instancia;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        foreach (Sonido s in sonidos)
        {
            s.FuenteAudio = gameObject.AddComponent<AudioSource>();
            s.FuenteAudio.clip = s.ClipSonido;
            s.FuenteAudio.volume = s.Volumen;
            s.FuenteAudio.pitch = s.pitch;
            s.FuenteAudio.loop = s.repetir;
        }
    }

    public void ReproducirSonido(string nombre)
    {
        Sonido s = Array.Find(sonidos, sound => sound.Nombre == nombre);
        if (s == null)
        {
            Debug.LogWarning("Sonido: " + nombre + " no encontrado.");
            return;
        }
        else
        {
            s.FuenteAudio.Play();
        }
    }
    public void PausarSonido(string nombre)
    {
        Sonido s = Array.Find(sonidos, sonido => sonido.Nombre == nombre);
        if (s == null)
        {
            Debug.LogWarning("Sonido: " + nombre + " no encontrado.");
            return;
        }
        else
        {
            s.FuenteAudio.Pause();
        }
    }


}

