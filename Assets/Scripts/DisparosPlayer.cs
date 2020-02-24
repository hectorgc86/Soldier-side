using UnityEngine;

//Héctor Granja Cortés

public class DisparosPlayer : MonoBehaviour
{
    private float posicionInicial;

    void Start()
    {
        posicionInicial = transform.position.x;
    }

    void Update()
    {
        //Controlo que el disparo no sobrepase de un cierto rango de distancia
        if (transform.position.x > posicionInicial + 1.5f || transform.position.x < posicionInicial - 1.5f) {
            Destroy(gameObject);
        } 
    }

    //Evento que detecta a quien le ha dado el player con su bala y llama a que se les quite salud
    private void OnTriggerEnter2D(Collider2D other)
    {
       if(other.tag == "Enemigo")
        {
            other.SendMessageUpwards("PerderSaludEnemigo", other.transform.position);

            Destroy(gameObject);
        }

        if (other.tag == "Boss")
        {
            other.SendMessageUpwards("PerderSaludBoss", other.transform.position);

            Destroy(gameObject);
        }

    }

}
