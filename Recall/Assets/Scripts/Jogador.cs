using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogador : MonoBehaviour {

    Rigidbody2D rb;
    public int velocidadeJogador;
    public int velocidadeJogadorRun;
    public int forcaPulo;
    float duracaoPulo;
    Animator animator;
    public GameObject player; // gameobject para inserir o player e mudar a escala dele
    public GameObject arm; // gameobject para inserir o braço e mudar a escala dele
    public int VidaJogador;
    public Component[] Braco;
    public bool invulnerabilidade;


    enum Estados {PARADO, ANDANDO, CORRENDO, PULANDO}
    Estados estado;

    public enum Lado { DIREITA, ESQUERDA }
    public Lado lado;

    SpriteRenderer spriteJogador;
    //SpriteRenderer spriteBraco;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        velocidadeJogador = 5;
        velocidadeJogadorRun = 10;
        forcaPulo = 15;
        estado = Estados.ANDANDO;
        lado = Lado.DIREITA;

        VidaJogador = 4;
        spriteJogador = GetComponent<SpriteRenderer>();
    }
    
	
	// Update is called once per frame
	void FixedUpdate () {

        if (estado == Estados.ANDANDO)
        {
            Andando();
            
            duracaoPulo = 0;
            
        }

        else if (estado == Estados.PULANDO)
        {
            Pulando();
            duracaoPulo -= Time.deltaTime;
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
        }
        

        if (Input.GetKey(KeyCode.Space))
        {
            vel.y = forcaPulo;
            duracaoPulo += 0.5f;  
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            vel.x = Input.GetAxis("Horizontal") * velocidadeJogadorRun;
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
            if (estado == Estados.ANDANDO)
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
}


