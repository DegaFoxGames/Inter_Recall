using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogador : MonoBehaviour
{

    Rigidbody2D rb;
    public int velocidadeJogador;
    public int velocidadeJogadorRun;
    public int forcaPulo;
    float duracaoPulo;
    Animator animator;
    public GameObject player; // gameobject para inserir o player e mudar a escala dele
    public GameObject arm; // gameobject para inserir o braço e mudar a escala dele
    public GameObject firepoint;

    public int VidaJogador;
    public bool invulnerabilidade;


    enum Estados { PARADO, ANDANDO, CORRENDO, PULANDO }
    Estados estado;

    public enum Lado { DIREITA, ESQUERDA }
    public Lado lado;

    SpriteRenderer spriteJogador;
    bool travaCorrer;
    //SpriteRenderer spriteBraco;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        velocidadeJogador = 5;
        velocidadeJogadorRun = 10;
        forcaPulo = 12;
        estado = Estados.ANDANDO;
        lado = Lado.DIREITA;

        VidaJogador = 4;
        spriteJogador = GetComponent<SpriteRenderer>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        if (estado == Estados.ANDANDO)
        {
            Andando();

            duracaoPulo = 0;

        }

        else if (estado == Estados.PULANDO)
        {
            //Pulando();
            duracaoPulo -= Time.deltaTime;
            arm.SetActive(false);
        }

        else if (estado == Estados.CORRENDO)
        {
            Andando();
            arm.SetActive(false);
        }

        // print(duracaoPulo);
        // print(estado);
    }

    void Andando()
    {
        Vector2 vel = rb.velocity;
        vel.x = Input.GetAxis("Horizontal") * velocidadeJogador;

        if (vel.x > 0)
        {
            lado = Lado.DIREITA;
        }

        if (vel.x < 0)
        {
            lado = Lado.ESQUERDA;
        }

        if (vel.x == 0)
        {
            travaCorrer = true;
        }

        else
        {
            travaCorrer = false;
        }


        if (lado == Lado.ESQUERDA)
        {
            //GetComponent<SpriteRenderer>().flipX = true;


            Vector3 newScalep = player.transform.localScale;
            newScalep.x = -1;
            player.transform.localScale = newScalep;

            Vector3 newScalea = arm.transform.localScale;
            newScalea.x = -1;
            newScalea.y = -1;
            arm.transform.localScale = newScalea;

            Vector3 newScalef = arm.transform.localScale;
            newScalef.x = -1;
            newScalef.y = -1;
            firepoint.transform.localScale = newScalea;
        }

        else if (lado == Lado.DIREITA)
        {
            //GetComponent<SpriteRenderer>().flipX = false;

            Vector3 newScalep = player.transform.localScale;
            newScalep.x = 1;
            player.transform.localScale = newScalep;

            Vector3 newScalea = arm.transform.localScale;
            newScalea.x = 1;
            newScalea.y = 1;
            arm.transform.localScale = newScalea;

            Vector3 newScalef = arm.transform.localScale;
            newScalef.x = 1;
            newScalef.y = 1;
            firepoint.transform.localScale = newScalea;
        }


        if (Input.GetKey(KeyCode.Space))
        {
            vel.y = forcaPulo;
            duracaoPulo += 0.5f;
            animator.SetBool("Pulando", true);
        }

        else
        {
            animator.SetBool("Pulando", false);
        }

        if (Input.GetKey(KeyCode.LeftShift) && !travaCorrer)
        {
            vel.x = Input.GetAxis("Horizontal") * velocidadeJogadorRun;
            animator.SetBool("Correndo", true);
            estado = Estados.CORRENDO;
        }

        else
        {
            estado = Estados.ANDANDO;
            animator.SetBool("Correndo", false);
            arm.SetActive(true);
        }


        rb.velocity = vel;
        animator.SetInteger("Andando", (int)vel.x);
    }

    void Pulando()
    {
        if (duracaoPulo <= 0)
        {
            Vector2 vel = rb.velocity;
            vel.x = Input.GetAxis("Horizontal") * velocidadeJogador;
            rb.velocity = vel;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //print("Enter");
        if (collision.tag == "Plataforma_tag")
        {

        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //print("Stay");
        if (collision.tag == "Plataforma_tag")
        {
            if (estado == Estados.PULANDO)
            {
                estado = Estados.ANDANDO;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //print("Exit");
        if (collision.tag == "Plataforma_tag")
        {
            if (estado == Estados.ANDANDO || estado == Estados.CORRENDO)
            {
                estado = Estados.PULANDO;
            }
        }
    }


    IEnumerator Dano()
    {
        for (float i = 0f; i < 1; i += 0.1f)
        {
            //spriteJogador.enabled = false;
            spriteJogador.color = Color.red;
            //spriteBraco.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            //spriteJogador.enabled = true;
            spriteJogador.color = Color.white;
            // spriteBraco.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
        invulnerabilidade = false;
    }


    public void DanoJogador()
    {
        invulnerabilidade = true;
        VidaJogador--;
        StartCoroutine(Dano());

        if (VidaJogador < 1)
        {
            Debug.Log("Morreu");
        }
    }


    void MaquinaDeEstados()
    {
        if (estado == Estados.PARADO)
        {
            if (estado == Estados.ANDANDO)
            {
                Andando();
            }

            else if (estado == Estados.CORRENDO)
            {

            }

            else if (estado == Estados.PULANDO)
            {

            }
        }
    }
}


