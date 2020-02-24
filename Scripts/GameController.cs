using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

//Héctor Granja Cortés

public class GameController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textoBalas;
    [SerializeField] TextMeshProUGUI textoVidas;
    [SerializeField] TextMeshProUGUI textoSalud;
    [SerializeField] TextMeshProUGUI textoPuntos;
    [SerializeField] TextMeshProUGUI textoGameOver;
    [SerializeField] Transform tituloFinJuego;
    [SerializeField] Transform sangrePS;

    private int puntos;
    private int vidas;
    private int salud;
    private int balas;
    private int nivelActual;

    private AudioSource[] sonidos;


    private void Start()
    {

        puntos = FindObjectOfType<GameStatus>().puntos;
        vidas = FindObjectOfType<GameStatus>().vidas;
        salud = FindObjectOfType<GameStatus>().salud;
        balas = FindObjectOfType<GameStatus>().balas;
        nivelActual = FindObjectOfType<GameStatus>().nivelActual;

        sonidos = GetComponents<AudioSource>();

        sonidos[5].Play();

        textoBalas.text = balas.ToString();
        textoSalud.text = salud.ToString();
        textoPuntos.text = puntos.ToString();
        textoVidas.text = vidas.ToString();

       
    }

    //Función de ganancia de puntos del player
    public void GanaPuntosPlayer(string tipoEnemigo) {

        switch (tipoEnemigo)
        {
            case "Tropa":
                puntos += 100;
                break;
            case "Boss":
                puntos += 1000;
                break;
        }

        FindObjectOfType<GameStatus>().puntos = puntos;
        textoPuntos.text = puntos.ToString();

    }

    //Función de ganancia de balas del player
    public void GanarBalasPlayer() {
        balas += 50;
        sonidos[2].Play();
        FindObjectOfType<GameStatus>().balas = balas;
        textoBalas.text = balas.ToString();
    }

    //Función de ganancia de salud del player
    public void GanarSaludPlayer()
    {
       
        salud += 100;
        sonidos[3].Play();
        FindObjectOfType<GameStatus>().salud = salud;
        textoSalud.text = salud.ToString();
    }

    //Función de actualización de balas del player
    public void DisparaPlayer()
    {
        if (balas > 0) {
            balas--;
            sonidos[0].Play();
        }
        else
        {
            sonidos[1].Play();
        }

        FindObjectOfType<GameStatus>().balas = balas;
        textoBalas.text = balas.ToString();
    }

    //Función de actualización de salud del player y llamadas a perder vida si se da el caso
    public void PerderSaludPlayer(Vector3 posicionImpacto)
    {
        sonidos[4].Play();

        Transform sangre = Instantiate(sangrePS, posicionImpacto, Quaternion.identity);

        salud -=10;

        if(salud == 0)
        {
            PerderVidaPlayer();
            salud = FindObjectOfType<GameStatus>().SaludInicial();
            textoSalud.text = salud.ToString();
        }

        FindObjectOfType<GameStatus>().salud = salud;
        textoSalud.text = salud.ToString();

        Destroy(sangre.gameObject, 1f);
    }

    //Función de actualización de vidas del player y llamada a la animación de morir y a terminar partida si se queda sin salud y vidas
    public void PerderVidaPlayer()
    { 
        vidas--;
        
        if (vidas <= -1)
        {
            FindObjectOfType<Player>().SendMessage("AnimarMorir");
            textoGameOver.text = "GAME OVER";
            TerminarPartida();
        }
        else
        {
            textoVidas.text = vidas.ToString();
        }

        FindObjectOfType<GameStatus>().vidas = vidas;
    }

    //Cambia la canción a la de pelea con Boss final
    private void CambiarACancionBoss()
    {
        sonidos[5].Stop();
        sonidos[6].Play();
    }

    //Función que frena el juego y muestra el texto de fin de juego
    private void FinJuego()
    {
        sonidos[6].Stop();
        sonidos[7].Play();
        Time.timeScale = 0f;
        Instantiate(tituloFinJuego, transform.position, Quaternion.identity);
    }

    //Función que cambia al nivel siguiente
    private void AvanzarNivel()
    {
        nivelActual++;

            FindObjectOfType<GameStatus>().nivelActual = nivelActual;
            SceneManager.LoadScene("Nivel" + nivelActual);

    }

    //Función de fin de la partida
    private void TerminarPartida()
    {
        StartCoroutine(VolverAlMenuPrincipal());
    }

    //Función que crea un slowmotion antes de volver al menú principal
    private IEnumerator VolverAlMenuPrincipal()
    {

        Time.timeScale = 0.1f;
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("Menu");
    }
}
