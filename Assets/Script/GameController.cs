using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameController : MonoBehaviour
{
    public int TotalPontos;
    public TextMeshProUGUI textoPontos;
    public GameObject gameOver;
    public static GameController instance;

    void Start()
    {
        instance = this;
    }

    public void AtualizarPontos()
    {
        textoPontos.text = TotalPontos.ToString();
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
    }

    public void Reiniciar(String nomeCena)
    {
        SceneManager.LoadScene(nomeCena);
    }
}
