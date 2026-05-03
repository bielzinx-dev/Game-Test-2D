using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using UnityEngine;


public class Inimigo : MonoBehaviour
{
    public float velocidade;
    public float tempoMovimento;
    public bool direcaoDireita = true; // Ajuste inicial no Inspector
    public bool inverteFlipSprite = false; // Use se o sprite estiver virado para a esquerda por padrão
    private Animator anim;
    private float timer;
    private SpriteRenderer spriteRenderer;
    public Vector2 direcao = Vector2.right;
    public float kikanoInimigo; // Força do pulo do inimigo
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;
    
    void Start()
    {
        // Inicializa as variáveis
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
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
        if (collision.gameObject.CompareTag("Player"))
        {
            // Verifica a direção da colisão para determinar se o jogador está pulando sobre o inimigo ou colidindo de lado
            ContactPoint2D contact = collision.contacts[0];
            // Se o jogador colidir com a parte superior do inimigo, ele é "kikado" para cima
            if (contact.normal.y < -0.5f)
            {
                // Aplica uma força para cima no jogador para simular o "kikano" e destrói o inimigo
                Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
                // Verifica se o Rigidbody2D do jogador foi encontrado antes de aplicar a força
                if (playerRb != null)
                {
                    // Aplica a força para cima no jogador
                        playerRb.velocity = new Vector2(
                        playerRb.velocity.x,
                        kikanoInimigo
                        );
                }
                // Destroi o inimigo
                anim.SetTrigger("Morte");
                velocidade = 0f; // Para o inimigo de se mover após a morte
                boxCollider.enabled = false;
                rb.isKinematic = true;
                Destroy(gameObject, 0.3f); // Destroi o inimigo após a animação de morte
            }
            else
            {
                // Se o jogador colidir de lado, é considerado um Game Over
                GameController.instance.GameOver();
                Destroy(collision.gameObject);
            }
        }
    }
}
