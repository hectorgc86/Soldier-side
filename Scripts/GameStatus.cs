using UnityEngine;

//Héctor Granja Cortés

public class GameStatus : MonoBehaviour
{
    //Stats por defecto del juego
    private const int PUNTOSINICIALES = 0;
    private const int VIDASINICIALES = 2;
    private const int BALASINICIALES = 50;
    private const int SALUDINICIAL = 100;

    public int SaludInicial()
    {
        return SALUDINICIAL;
    }

    public int puntos;
    public int vidas;
    public int salud;
    public int balas;
    public int nivelActual;


    //Función de inicio
    private void Awake()
    {

        int gameStatusCount = FindObjectsOfType<GameStatus>().Length;

        if (gameStatusCount > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    //Función de reseteo de lo las estadísticas
    public void ResetStats()
    {
        puntos = PUNTOSINICIALES;
        vidas = VIDASINICIALES;
        salud = SALUDINICIAL;
        balas = BALASINICIALES;
        nivelActual = 1;
    }
}
