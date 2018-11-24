using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agente : MonoBehaviour
{

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
    bool fire;

    public float playerDistancia;
    public float ataqueDistancia;
    public GameObject player;

    public GameObject PrefabProjetil;
    public Transform instanciador;

    enum Lados { DIREITA, ESQUERDA }
    Lados lado;

    public int VidaInimigo;

    SpriteRenderer sprite;

    // Use this for initialization
    void Start()
    {
        // direcao = Direcao.DIREITA;
        anim = GetComponent<Animator>();
        //lado = Lados.ESQUERDA;
        eLadoDireito = true;
        estaPatrulhando = false;
        atacar = false;

        vel = 3;
        ataqueDistancia = 10;
        duracaoIdle = 2;
        duracaoPatrulhar = 5;
        duracaoAtacar = 2.4f;

        VidaInimigo = 4;
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        //print(VidaInimigo);
        MudarEstado();

        playerDistancia = transform.position.x - player.transform.position.x;

        if (Mathf.Abs(playerDistancia) < ataqueDistancia)
        {
            atacar = true;
            estaPatrulhando = false;

            if (atacar && !estaPatrulhando)
            {
                Idle();
                Atirar();
            }
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

        if (playerDistancia < 0 && !eLadoDireito || playerDistancia > 0 && eLadoDireito)
        {
            MudarDirecao();
        }

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
        if (playerDistancia < 0 && !eLadoDireito || playerDistancia > 0 && eLadoDireito)
        {
            MudarDirecao();
        }

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


    void Fire()
    {

        if (fire && !anim.GetCurrentAnimatorStateInfo(0).IsTag("estaAtirando"))
        {
            anim.SetTrigger("estaAtirando");
        }
    }

    private void InstanciarProjetil()
    {
        GameObject temp = (Instantiate(PrefabProjetil, instanciador.position, instanciador.rotation));
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Doug/Gun shots/Handgun", GetComponent<Transform>().position); // som do tiro

        if (eLadoDireito)
        {
            temp.GetComponent<Bala>().Inicializar(Vector2.right);
        }

        else if (!eLadoDireito)
        {
            temp.GetComponent<Bala>().Inicializar(Vector2.left);
        }
    }

    public void DanoAgente(int DanoBalaJogador)
    {
        VidaInimigo -= DanoBalaJogador;
        StartCoroutine(Dano());

        if (VidaInimigo < 1)
        {
            Destroy(gameObject);
        }
    }


    IEnumerator Dano()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }
}
