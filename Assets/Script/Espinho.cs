using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espinho : MonoBehaviour
{
    //Verifica se o objeto que colidiu com o espinho tem a tag "Player" e dps destroir o Player
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
