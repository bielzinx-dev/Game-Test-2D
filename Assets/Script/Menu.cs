using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public String nomeCena;
    
    
    public void Jogar()
    {
        SceneManager.LoadScene(nomeCena);
    }
    
    public void Sair()
    {
        Application.Quit();
    }

    public void Voltar()
    {
        SceneManager.LoadScene("Inicio");
    }
}
