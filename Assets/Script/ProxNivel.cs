using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProxNivel : MonoBehaviour
{
    public String nomeCena;
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Verifica se o objeto que colidiu com o próximo nível tem a tag "Player"
        if(collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(nomeCena);
        }
    }
}
