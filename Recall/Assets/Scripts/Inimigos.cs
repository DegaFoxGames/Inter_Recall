using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigos : MonoBehaviour
{ 

    public enum Estado { CAINDO, ANDANDO, PARADO }
    public Estado estado;

    Rigidbody2D rb;
    Animator anim;

    int pes;
    float tempo;

    // Use this for initialization
    void Start()
    {

        estado = Estado.CAINDO;
        pes = 0;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        if (estado == Estado.CAINDO)
        {
            Caindo();
        }
        else if (estado == Estado.ANDANDO)
        {
            Andando();
        }

        if (estado == Estado.PARADO)
        {
            Parado();
        }

    }

    void Caindo()
    {
        if (pes > 0)
        {
            estado = Estado.ANDANDO;
        }
    }

    void Andando()
    {
        Vector2 velo = rb.velocity;
        velo.x += 0.3f * transform.localScale.x;
        velo.x = Mathf.Clamp(velo.x, -5, 5);

        if (pes == 1 && Mathf.Abs(velo.x) > 2)
        {
            velo.x = 0;
            Vector3 escala = transform.localScale;
            escala.x = -escala.x;
            transform.localScale = escala;
        }

        rb.velocity = velo;

        anim.SetInteger("Walk_Inimigo", (int)velo.x);

        tempo += Time.deltaTime;
        if (tempo > 1.5f)
        {
            if (Random.value < 0.02f)
            {
                estado = Estado.PARADO;
                tempo = 0;
            }
        }
    }

    void Parado()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);

        anim.SetInteger("Walk_Inimigo", 0);

        tempo += Time.deltaTime;
        if (tempo > 1)
        {
            rb.AddForce(Vector2.up * 500);
            tempo = 0;
        }

        if (pes == 0) estado = Estado.CAINDO;
    }

    private void OnTriggerEnter2D(Collider2D colisor)
    {
        if (colisor.tag == "limite") pes++;

        if (colisor.tag == "Tiros")
        {
            Destroy(colisor.gameObject);
            Destroy(gameObject);
        }
    }


    private void OnTriggerExit2D(Collider2D colisor)
    {
        if (colisor.tag == "limite") pes--;
    }
}


