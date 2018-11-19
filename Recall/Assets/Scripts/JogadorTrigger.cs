using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JogadorTrigger : MonoBehaviour {

    private Jogador jogador;

    // Use this for initialization

    private void Awake()
    {
        jogador = GameObject.Find("Player").GetComponent<Jogador>();
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("BalaInimigo"))
        {
            if (!jogador.invulnerabilidade)
            {
                jogador.DanoJogador();
               
            }
        }

        if (collision.CompareTag("Sentinela"))
        {
            if (!jogador.invulnerabilidade)
            {
                jogador.DanoJogador();

            }
        }
    }
}
