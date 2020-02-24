using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Héctor Granja Cortés

public class MuroEnfrentamiento : MonoBehaviour
{

    private BoxCollider2D[] colliders;

    private bool musicaBossActivada;

    void Start()
    {
        musicaBossActivada = false;
        colliders = GetComponents<BoxCollider2D>();

    }

    //Cuando el player sobrepasa este muro invisible, el collider de este se activa no permitiendo que regrese, aparte llama a que se cambie le música
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            colliders[1].enabled = true;

            if (!musicaBossActivada)
            {
                FindObjectOfType<GameController>().SendMessage("CambiarACancionBoss");
                musicaBossActivada = true;
            }
           
        }
    }
}
