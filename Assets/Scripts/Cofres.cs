using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cofres : MonoBehaviour
{
    public ControlJugador mostartexo;
    public ControlJugador abrircofre;
    public TMPro.TMP_Text textoMunPis;
    public TMPro.TMP_Text textoMunEsc;
    public TMPro.TMP_Text escudo;
    public TMPro.TMP_Text botiquines;
    public int mun_cofre;
    public int mune_cofre;
    public int esc_cofre;
    public int bot_cofre;
     

    // Start is called before the first frame update
    void Start()
    {
        textoMunPis.text = "Muncion Pistola: " + mun_cofre.ToString();
        textoMunEsc.text = "Municion Escopeta " + mune_cofre.ToString();
        escudo.text = "Armadura: " + esc_cofre.ToString();
        botiquines.text = "Botiquines: " + bot_cofre.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        abrircofre.AbrirCofre();
        Textos();
    }

    private void Textos()
    {
       textoMunPis.text = "Muncion Pistola: " + mun_cofre.ToString();
       textoMunEsc.text = "Municion Escopeta " + mune_cofre.ToString();
       escudo.text = "Armadura: " + esc_cofre.ToString();
       botiquines.text = "Botiquines: " + bot_cofre.ToString();
    }
    public void dejarMunP(){
        int aux;
        if (ControlJugador.municion >= 10)
        {
            ControlJugador.municion -= 10;
            mun_cofre += 10;
            textoMunPis.text = "Muncion Pistola: " + mun_cofre.ToString();
            Textos();
            mostartexo.mostrarTextos();
        } else if (ControlJugador.municion < 10 && ControlJugador.municion > 1)
        {
            aux = ControlJugador.municion;
            ControlJugador.municion -= ControlJugador.municion;
            mun_cofre += aux;
            textoMunPis.text = "Muncion Pistola: " + mun_cofre.ToString();
            Textos();
            mostartexo.mostrarTextos();
        }
       

    }

    public void agarrarMunP()
    {
        int aux;
        if (ControlJugador.municion < 30 && mun_cofre >= 10)
        {
            ControlJugador.municion += 10;
            mun_cofre -= 10;
            textoMunPis.text = "Muncion Pistola: " + mun_cofre.ToString();
            Textos();
            mostartexo.mostrarTextos();
        }
        else if (ControlJugador.municion < 30 && (mun_cofre < 10 && mun_cofre >= 1))
        {
            aux = mun_cofre;
            ControlJugador.municion += mun_cofre;
            mun_cofre -= aux;
            textoMunPis.text = "Muncion Pistola: " + mun_cofre.ToString();
            Textos();
            mostartexo.mostrarTextos();
        }
    }
    public void dejarMunE()
    {
        int aux;
        if (ControlJugador.municionesc >= 6)
        {
            ControlJugador.municionesc -= 6;
            mune_cofre += 6;
            textoMunEsc.text = "Municion Escopeta " + mune_cofre.ToString();
            Textos();
            mostartexo.mostrarTextos();
        }
        else if (ControlJugador.municionesc < 6 && ControlJugador.municionesc >= 1)
        {
            aux = ControlJugador.municionesc;
            ControlJugador.municionesc -= ControlJugador.municionesc;
            mune_cofre += aux;
            textoMunEsc.text = "Municion Escopeta " + mune_cofre.ToString();
            Textos();
            mostartexo.mostrarTextos();
        }


    }
    public void agarrarMunE()
    {
        int aux;
        if (ControlJugador.municionesc < 20 && mune_cofre >= 6)
        {
            ControlJugador.municionesc += 6;
            mune_cofre -= 6;
            textoMunEsc.text = "Municion Escopeta " + mune_cofre.ToString();
            Textos();
            mostartexo.mostrarTextos();
        }
        else if (ControlJugador.municionesc < 20 && (mune_cofre < 6 && mune_cofre >= 1))
        {
            aux = mune_cofre;
            ControlJugador.municionesc += mune_cofre;
            mune_cofre -= aux;
            textoMunEsc.text = "Municion Escopeta " + mune_cofre.ToString();
            Textos();
            mostartexo.mostrarTextos();
        }
    }
    public void dejarArmadura()
    {
        if (ControlJugador.escudo >= 1)
        {
            ControlJugador.escudo -= 1;
            esc_cofre += 1;
            escudo.text = "Armadura: " + esc_cofre.ToString();
            Textos();
            mostartexo.mostrarTextos();

        }
    }
    public void agarrarArmadura()
    {
        if (ControlJugador.escudo < ControlJugador.escudo_max && esc_cofre >= 1)
        {
            ControlJugador.escudo += 1;
            esc_cofre -= 1;
            escudo.text = "Armadura: " + esc_cofre.ToString();
            Textos();
            mostartexo.mostrarTextos();

        }
    }

    public void dejarBotiquin()
    {
        if (ControlJugador.botiquin > 1)
        {
            ControlJugador.botiquin -= 1;
            bot_cofre += 1;
            botiquines.text = "Botiquines: " + bot_cofre.ToString();
            Textos();
            mostartexo.mostrarTextos();
        }
    }

    public void agarrarBotiquin()
    {
        if (ControlJugador.botiquin < 5 && bot_cofre >=1)
        {
            ControlJugador.botiquin += 1;
            bot_cofre -= 1;
            botiquines.text = "Botiquines: " + bot_cofre.ToString();
            Textos();
            mostartexo.mostrarTextos();
        }
    }
}
    

