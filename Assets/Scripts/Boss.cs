using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Héctor Granja Cortés

public class Boss : MonoBehaviour
{
    [SerializeField] List<Transform> wayPoints;
    [SerializeField] float velocidad;
    [SerializeField] Transform disparoBoss;
    [SerializeField] Transform sangrePS;


    private int saludBoss = 300;
    private byte siguientePosicion;
    private float distanciaCambio = 0.2f;
    private float velocidadBala;
    private float pausaEntreDisparos;
    private Animator anim;

    private AudioSource[] sonidosBoss;

    private void Start()
    {
        siguientePosicion = 0;
        sonidosBoss = GetComponents<AudioSource>();
        anim = gameObject.GetComponent<Animator>();
        StartCoroutine(Disparar());
    }

    //Asignación de animaciones
    private void Update()
    {
        if (transform.position.x < wayPoints[siguientePosicion].transform.position.x)
        {
            anim.Play("BossCorreDerecha");
            velocidadBala = 4;
        }
        else if (transform.position.x > wayPoints[siguientePosicion].transform.position.x)
        {
            anim.Play("BossCorreIzquierda");
            velocidadBala = -4;
        }

        //Rutas que sigue
        transform.position = Vector3.MoveTowards(transform.position, wayPoints[siguientePosicion].transform.position, velocidad * Time.deltaTime);
        if (Vector3.Distance(transform.position, wayPoints[siguientePosicion].transform.position) < distanciaCambio)
        {
            siguientePosicion++;

            if (siguientePosicion >= wayPoints.Count)
            {
                siguientePosicion = 0;
            }
        }
    }

    //Corrutina de disparo
    private IEnumerator Disparar()
    {
        pausaEntreDisparos = Random.Range(0, 1.0f);

        yield return new WaitForSeconds(pausaEntreDisparos);

        Transform disparo = Instantiate(disparoBoss, transform.position, Quaternion.identity);

        disparo.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(velocidadBala, 0, 0);

        sonidosBoss[0].Play();

        StartCoroutine(Disparar());
    }

    //Evento de control de salud y que ejecuta el fin de juego cuando muera
    private void PerderSaludBoss(Vector3 posicionImpacto)
    {
        sonidosBoss[1].Play();

        Transform sangre = Instantiate(sangrePS, posicionImpacto, Quaternion.identity);

        saludBoss -= 20;

        if (saludBoss == 0)
        {

            FindObjectOfType<GameController>().SendMessageUpwards("GanaPuntosPlayer", "Boss");
            FindObjectOfType<GameController>().SendMessage("FinJuego");
            Destroy(gameObject);
        }

        Destroy(sangre.gameObject, 1f);
    }


}
