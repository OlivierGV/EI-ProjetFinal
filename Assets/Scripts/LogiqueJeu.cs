using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Classe "maitre" qui gère la logique de jeu : incrémentation de points et changements de scènes
/// </summary>
public class LogiqueJeu : MonoBehaviour
{
    /// <summary>
    /// Référence aux points et au temps restant d'une partie
    /// </summary>
    [SerializeField]
    AffichagePointsTemps affichagePointsTemps;

    /// <summary>
    /// Nom de la scène vers laquelle se diriger après une partie
    /// </summary>
    [SerializeField]
    private string nomScene;

    /// <summary>
    /// Instance unique
    /// </summary>
    public static LogiqueJeu Instance { get; private set; }

    public static int pointsFinaux = 0;
    public static int tempsDernierePartie = 0;

    void Awake()
    {
        // Assure qu'il y ait qu'une instance de cette classe
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (string.IsNullOrEmpty(nomScene))
        {
            throw new System.Exception("Aucune scène indiquée");
        }
        DebutPartie();
    }

    /// <summary>
    /// Au début d'une partie, commencer le minuteur et réinitialiser les points finaux
    /// </summary>
    public void DebutPartie()
    {
        affichagePointsTemps.Commencer();
        pointsFinaux = 0;
    }

    /// <summary>
    /// Quand une partie est finie, changer de scène
    /// </summary>
    public void partieFinie(int points, int temps)
    {
        pointsFinaux = points;
        tempsDernierePartie = temps;
        SceneManager.LoadScene(nomScene);
    }

    /// <summary>
    /// Dire à l'affichage d'incrémenter les points
    /// </summary>
    public void incrementerPoints()
    {
        affichagePointsTemps.IncrementerPoints();
    }
}
