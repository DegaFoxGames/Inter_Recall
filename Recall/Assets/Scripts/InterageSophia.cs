using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterageSophia : MonoBehaviour {

    private bool entrou;
    private int mudaDialogo;

    void Update()
    {
        if (entrou == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                mudaDialogo++;
                print("Diálogo Sophia " + mudaDialogo);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            print("Entrou");
            entrou = true;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        print("Ficou");
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            print("saiu");
            entrou = false;
        }
    }
}
