using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]


public class Bala : MonoBehaviour {

    [SerializeField]
    private float velocidade;
    private Vector2 direcao;

    private Rigidbody2D rb;
    public GameObject explosao;
    public float tempo = 0.3f;
	// Use this for initialization
	void Start () {

        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        rb.velocity = direcao * velocidade;
	}


    private void OnBecameInvisible() // Função para destruir a bala quando ela deixa de ser visível pela câmera
    {
        Destroy(gameObject);
    }

    public void Inicializar(Vector2 _direcao)
    {
        direcao = _direcao;

        if (direcao == Vector2.left)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }

        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player"){

            explosao.SetActive(true); // SetActive Ativa ou Desativa o GameObject
            StartCoroutine("Destruir");
        }
    }

    IEnumerator Destruir()
    {
        yield return new WaitForSeconds(tempo);
        Destroy(gameObject);
    }
}
