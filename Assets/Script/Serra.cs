using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Serra : MonoBehaviour
{
    public float velocidade;
    public float tempoMovimento;
    public bool direcaoDireita = true; // Ajuste inicial no Inspector
    public bool inverteFlipSprite = false; // Use se o sprite estiver virado para a esquerda por padrão
    private Animator anim;
    private float timer;
    private SpriteRenderer spriteRenderer;
    public Vector2 direcao = Vector2.right;
    
    void Start()
    {
        // Inicializa as variáveis
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //Move a serra para a direita ou para a esquerda dependendo da direção atual
        if(direcaoDireita)
        {
            //Move a serra para a direita
            transform.Translate(direcao * velocidade * Time.deltaTime);
            // Gira o sprite de acordo com a direção e com a configuração do Inspector
            spriteRenderer.flipX = !inverteFlipSprite;
        }
        else
        {
            //Move a serra para a esquerda
            transform.Translate(-direcao * velocidade * Time.deltaTime);
            // Gira o sprite de acordo com a direção e com a configuração do Inspector
            spriteRenderer.flipX = inverteFlipSprite;
        }

        //Incrementa o timer e verifica se é hora de mudar a direção da serra
        timer += Time.deltaTime;
        if(timer >= tempoMovimento)
        {
            //Inverte a direção da serra
            direcaoDireita = !direcaoDireita;
            timer = 0f;
        }
    }

    //Verifica se a serra colidiu com o jogador e, se sim, chama o método GameOver do GameController e destrói o objeto do jogador
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Verifica se o objeto que colidiu com o espinho tem a tag "Player"
        if(collision.gameObject.CompareTag("Player"))
        {
            GameController.instance.GameOver();
            Destroy(collision.gameObject);
        }
    }
}
