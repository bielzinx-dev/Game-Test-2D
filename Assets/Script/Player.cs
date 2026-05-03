using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public float velocidade;
    private Rigidbody2D rb;
    public float forcaPulo;
    public bool estaNoChao;
    public bool puloDuplo;
    private Animator anim;
    bool sopro;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Movimento();
        Pulo();
        Queda();
    }

    //Função para movimentar o player
    void Movimento()
    {
        //Cria um vetor de movimento com base na entrada do jogador (teclas A e D ou setas esquerda e direita)
        Vector3 movimento = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movimento * velocidade * Time.deltaTime;
        
        //Verifica a direção do movimento para ajustar a animação e a rotação do player
        if(Input.GetAxis("Horizontal") > 0f)
        {
            anim.SetBool("Andando", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }

        //Verifica a direção do movimento para ajustar a animação e a rotação do player
        if(Input.GetAxis("Horizontal") < 0f)
        {
            anim.SetBool("Andando", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        //Verifica se o player não está se movendo para ajustar a animação e só anda se estiver no chão
        if(Input.GetAxis("Horizontal") != 0 && estaNoChao)
        {
        anim.SetBool("Andando", true);
        }
        else
        {
            anim.SetBool("Andando", false);
        }
    }
    //Função para fazer o player pular
    void Pulo()
    {
        //Verifica se o jogador pressionou a tecla de pulo (espaço) e se o player não está sendo soprado por um ventilador
        if(Input.GetKeyDown(KeyCode.Space) && !sopro){
            if (estaNoChao)
            {
                //Se o player não estiver no chão, ele pode pular novamente (pulo duplo)
                rb.AddForce(new Vector2(0f, forcaPulo), ForceMode2D.Impulse);
                puloDuplo = true;
                
                anim.SetBool("Andando", false);
                anim.SetBool("Pulo", true);
                anim.SetBool("PuloDuplo", false);
            }
            else
            {
                //Se o player estiver no chão, ele pode pular normalmente
                if(puloDuplo)
                {
                    //Se o player já tiver pulado uma vez, ele pode pular novamente (pulo duplo)
                    rb.AddForce(new Vector2(0f, forcaPulo), ForceMode2D.Impulse);
                    puloDuplo = false;
                    
                    anim.SetBool("Andando", false);
                    anim.SetBool("PuloDuplo", true);
                    anim.SetBool("Pulo", false);
                }
            }
        }
    }

    void Queda()
    {
        if(!estaNoChao && rb.velocity.y < -0.1f)
        {
            //Se o player estiver caindo, ele pode usar uma animação de queda
            anim.SetBool("Caindo", true);
            //anim.SetBool("Pulo", false);
            //anim.SetBool("PuloDuplo", false);
            
        }
        else
        {
            anim.SetBool("Caindo", false);
        }
    }
    //Função para detectar se o player está no chão ou não
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Verifica se o player colidiu com um objeto da layer 8 (chão)
        if(collision.gameObject.layer == 8)
        {
            //Verifica se o contato com o chão é na parte inferior do player, ou seja, se o player está realmente no chão e não colidindo com uma parede ou teto
            foreach(ContactPoint2D contato in collision.contacts)
            {
                //Se o contato com o chão for na parte inferior do player, ele estará no chão
                if(contato.normal.y > 0.5f)
                {   
                
                //Se o player colidir com um objeto da layer 8 (chão), ele estará no chão
                estaNoChao = true;
                
                anim.SetBool("PuloDuplo", false);
                anim.SetBool("Pulo", false);
                break;
                }
            }
        }
    }

    //Função para detectar se o player saiu do chão ou não
    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            //Se o player sair de um objeto da layer 8 (chão), ele não estará mais no chão
            estaNoChao = false;
        }
    }

    //Função para detectar se o player entrou em contato com um ventilador ou não
    void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Ventilador"))
        {
            //Se o player entrar em contato com um ventilador, ele estará sendo soprado
            sopro = true;
        }
    }
    
    //Função para detectar se o player saiu do contato com um ventilador ou não
    void OnTriggerExit2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Ventilador"))
        {
            //Se o player sair do contato com um ventilador, ele não estará mais sendo soprado
            sopro = false;
        }
    }
}
