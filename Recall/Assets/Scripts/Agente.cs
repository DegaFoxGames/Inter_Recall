using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agente : MonoBehaviour {

    private bool eLadoDireito;

    [SerializeField]
    private float vel;
    private Animator anim;

    private float tempoIdle;
    [SerializeField] 
    private float duracaoIdle;

    private float tempoPatrulhar;
    [SerializeField]
    private float duracaoPatrulhar;

    private float tempoAtacar;
    [SerializeField]
    private float duracaoAtacar;

    public bool estaPatrulhando;
    public bool atacar;

    public float playerDistancia;
    public float ataqueDistancia;
    public GameObject player;
     

    // Use this for initialization
    void Start () {
       // direcao = Direcao.DIREITA;
        anim = GetComponent<Animator>();
        eLadoDireito = true;
        estaPatrulhando = false;
        atacar = false;

        vel = 1;
        ataqueDistancia = 6;
        duracaoIdle = 5;
        duracaoPatrulhar = 5;
        duracaoAtacar = 5;
	}
	
	// Update is called once per frame
	void Update ()
    {

        MudarEstado();

        playerDistancia = transform.position.x - player.transform.position.x;



        if (Mathf.Abs(playerDistancia) < ataqueDistancia)
        {
            atacar = true;
            estaPatrulhando = false;
            Idle();
            Atirar();
        }
        else
        {
            MudarEstado();
        }
    }

    // métodos para movimento 

    void Mover()
    {
        transform.Translate(PegarDirecao() * (vel * Time.deltaTime));
    }

    Vector2 PegarDirecao()
    {
         return eLadoDireito ? Vector2.right : Vector2.left;
    }

    void MudarDirecao()
    {
        eLadoDireito = !eLadoDireito;
        transform.localScale = new Vector3(-transform.localScale.x, 1, 1);   
    }



    // métodos para animação

    void Idle()
    {
        tempoIdle += Time.deltaTime;
        if (tempoIdle <= duracaoIdle)
        {
            anim.SetBool("estaPatrulhando", estaPatrulhando);
            tempoPatrulhar = 0;
        }
        else
        {
            estaPatrulhando = true;
        }
    }

    void Patrulhar()
    {
        tempoPatrulhar += Time.deltaTime;
        if (tempoPatrulhar <= duracaoPatrulhar)
        {
            anim.SetBool("estaPatrulhando", estaPatrulhando);
            Mover();
            tempoIdle = 0;
        }
        else
        {
            estaPatrulhando = false;
        }
    }

    void Atirar()
    {
        if (playerDistancia < 0 && !eLadoDireito || playerDistancia > 0 && eLadoDireito)
        {
            MudarDirecao();
        }

        if (atacar)
        {
            tempoAtacar += Time.deltaTime;

            if (tempoAtacar >= duracaoAtacar)
            {
                anim.SetTrigger("estaAtirando");
                tempoAtacar = 0;
            }
        }
    }
    

    void MudarEstado()
    {
        if (!atacar)
        {
            if (!estaPatrulhando)
            {
                Idle();
            }

            else
            {
                Patrulhar();
            }
        }
    }

    public void ResetarAtacar()
    {
        atacar = false;
    }
}
