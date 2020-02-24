using UnityEngine;

//Héctor Granja Cortés

public class DisparosEnemigo : MonoBehaviour
{
    private float posicionInicial;

    void Start()
    {
        posicionInicial = transform.position.x;
    }

    void Update()
    {
        //Controlo que el disparo no sobrepase de un cierto rango de distancia
        if (transform.position.x > posicionInicial + 2.5f || transform.position.x < posicionInicial - 2.5f)
        {
            Destroy(gameObject);
        }
    }

    //Evento que llama a la perdida de vida del player, y le pasa la posición de la bala
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        { 
            FindObjectOfType<GameController>().SendMessageUpwards("PerderSaludPlayer",other.transform.position);
            Destroy(gameObject);
        }
    }

}
