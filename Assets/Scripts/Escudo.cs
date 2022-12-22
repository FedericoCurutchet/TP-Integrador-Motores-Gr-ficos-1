using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escudo : MonoBehaviour
{

    public int escudo_hp;
    // Start is called before the first frame update
    void Start()
    {
        escudo_hp = 3;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void desaparecer()
    {
        Destroy(gameObject);
    }
    private void recibirDaño()
    {
        escudo_hp = escudo_hp - 1;
        if (escudo_hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bala"))
        {
            recibirDaño();

        }
        if (collision.gameObject.CompareTag("BalaEsc"))
        {
            recibirDaño();

        }
    }
}
