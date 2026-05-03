using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frutas : MonoBehaviour
{
    private SpriteRenderer sp;
    private CircleCollider2D col;
    public GameObject coletado;
    public int ValorPontos; 

    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        col = GetComponent<CircleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Verifica se o objeto que colidiu com a fruta tem a tag "Player"
        if(collision.gameObject.CompareTag("Player"))
        {

            sp.enabled = false;
            col.enabled = false;
            coletado.SetActive(true);

            GameController.instance.TotalPontos += ValorPontos;
            GameController.instance.AtualizarPontos();
            Destroy(gameObject, 0.3f);
        }
    }
}
