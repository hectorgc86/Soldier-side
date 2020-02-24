using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Héctor Granja Cortés

public class Menu : MonoBehaviour
{
    //Evento que se ejecuta al pulsar el botón de Comenzar del Menu Principal, también resetea los stats generales de GameStatus
    public void EmpezarJuego() {

        FindObjectOfType<GameStatus>().ResetStats();
        SceneManager.LoadScene("Nivel1");
    }

    //Evento que se ejecuta al pulsar el botón de Salir del Menu Principal
    public void SalirJuego()
    {
        Application.Quit();
    }
}
