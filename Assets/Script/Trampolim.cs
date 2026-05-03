using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampolim : MonoBehaviour
{
    public float forcaPulo;
    private Animator anim;

    //Inicializa as variáveis
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    //Verifica se o objeto que colidiu com o trampolim tem a tag "Player" e, se sim, chama o método GameOver do GameController e destrói o objeto do jogador
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Verifica se o objeto que colidiu com o trampolim tem a tag "Player"
        if(collision.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("Pulo");
            //Adiciona uma força para cima no objeto que colidiu com o trampolim
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, forcaPulo), ForceMode2D.Impulse);
        }
    }
}
