using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sentinela : MonoBehaviour
{

    private bool eLadoDireito;
    private bool eCima;

    [SerializeField]
    private float vel;
    //private Animator anim;

    public bool estaPatrulhando;
    public bool estaPerseguindo;

    private float tempoPatrulha;
    private float duracaoPatrulha;

    public float playerDistanciaX;
    public float playerDistanciaY;

    public float ataqueDistanciaX;
    public float ataqueDistanciaY;

    public GameObject player;

    public int VidaSentinela;
    SpriteRenderer sprite;
    

    // Use this for initialization
    void Start()
    {
        //anim = GetComponent<Animator>();
        eLadoDireito = true;
        eCima = true;
        estaPatrulhando = true;
        estaPerseguindo = false;
        duracaoPatrulha = 5;

        vel = 3;
        ataqueDistanciaX = 10;
        ataqueDistanciaY = 5;

        sprite = GetComponent<SpriteRenderer>();
        VidaSentinela = 4;

        
    }

    // Update is called once per frame
    void Update()
    {

        //print(VidaSentinela);
        playerDistanciaX = transform.position.x - player.transform.position.x;
        playerDistanciaY = transform.position.y - player.transform.position.y;

        if (Mathf.Abs(playerDistanciaX) < ataqueDistanciaX && Mathf.Abs(playerDistanciaY) < ataqueDistanciaY)
        {
            estaPerseguindo = true;
            PerseguirHorizontal();
            PerseguirVertical();
        }

        else
        {
            Patrulhar();
        }
    }

    // métodos para movimento 

    void MoverHorizontal()
    {
        transform.Translate(PegarDirecaoHorizontal() * (vel * Time.deltaTime));
    }

    Vector2 PegarDirecaoHorizontal()
    {
        return eLadoDireito ? Vector2.right : Vector2.left;
    }

    void MudarDirecaoHorizontal()
    {
        eLadoDireito = !eLadoDireito;
        transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
        GetComponent<SpriteRenderer>().flipX = true;
    }

    void PerseguirHorizontal()
    {
        if (playerDistanciaX < 0 && !eLadoDireito || playerDistanciaX > 0 && eLadoDireito)
        {
            MudarDirecaoHorizontal();
        }
            MoverHorizontal();
    }


    void MoverVertical()
    {
        transform.Translate(PegarDirecaoVertical() * (vel * Time.deltaTime));
    }

    Vector2 PegarDirecaoVertical()
    {
        return eCima ? Vector2.up : Vector2.down;
    }

    void MudarDirecaoVertical()
    {
        eCima = !eCima;
    }

    // métodos para patrulha e perseguição

    void PerseguirVertical()
    {
        if (playerDistanciaY < 0 && !eCima || playerDistanciaY > 0 && eCima)
        {
            MudarDirecaoVertical();
        }
        MoverVertical();
    }


    // método de patrulha

    void Patrulhar()
    {
            tempoPatrulha += Time.deltaTime;

            if (tempoPatrulha <= duracaoPatrulha)
            {
            MoverHorizontal(); 
            }

            if (tempoPatrulha >= duracaoPatrulha)
        {
            MudarDirecaoHorizontal();
            tempoPatrulha = 0;
        }
    }


    public void DanoSentinela(int DanoBalaJogador)
    {
        VidaSentinela -= DanoBalaJogador;
        StartCoroutine(DanoSentinela());

        if (VidaSentinela < 1)
        {
            Destroy(gameObject);
        }
    }


    IEnumerator DanoSentinela()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
