using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BracoPisca : MonoBehaviour {

    SpriteRenderer spriteBraco;
    Jogador jogador;
	// Use this for initialization
	void Start () {
        spriteBraco = GetComponent<SpriteRenderer>();
        jogador = GetComponent<Jogador>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Dano()
    {
        for (float i = 0f; i < 1; i += 0.1f)
        {
            //spriteJogador.enabled = false;
            spriteBraco.color = Color.red;
            //spriteBraco.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            //spriteJogador.enabled = true;
            spriteBraco.color = Color.white;
            // spriteBraco.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
        jogador.invulnerabilidade = false;
    }


    public void DanoBraco()
    {
        jogador.invulnerabilidade = true;
        jogador.VidaJogador--;
        StartCoroutine(Dano());

        if (jogador.VidaJogador < 1)
        {
            Debug.Log("Morreu");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BalaInimigo"))
        {
            if (!jogador.invulnerabilidade)
            {
                DanoBraco();
                
            }
        }
    }
}
