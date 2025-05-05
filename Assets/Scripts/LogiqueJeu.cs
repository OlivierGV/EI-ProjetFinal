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
    /// Référence aux points d'apparitions
    /// </summary>
    [SerializeField]
    private PointsApparitionsChats pointsApparitions;

    /// <summary>
    /// Nombre de chats encore en vie
    /// </summary>
    private float nombreChatsRestants = 0;

    /// <summary>
    /// Instance unique
    /// </summary>
    public static LogiqueJeu Instance { get; private set; }

    /// <summary>
    /// Variable statique pour les points finaux
    /// Permet d'y accéder dans le menu de fin
    /// </summary>
    public static int pointsFinaux = 0;

    /// <summary>
    /// Variable statique pour le tempss écoulé de la partie
    /// Permet d'y accéder dans le menu de fin
    /// </summary>
    public static float tempsDernierePartie = 0;

    /// <summary>
    /// Temps d'une partie en secondes
    /// </summary>
    [SerializeField]
    private int tempsPartie = 180;

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
        nombreChatsRestants = pointsApparitions.nombreApparition;
        DebutPartie();
    }

    /// <summary>
    /// Au début d'une partie, commencer le minuteur et réinitialiser les points finaux
    /// </summary>
    public void DebutPartie()
    {
        affichagePointsTemps.Commencer(tempsPartie);
        pointsFinaux = 0;
    }

    /// <summary>
    /// Quand une partie est finie, changer de scène
    /// </summary>
    public void partieFinie()
    {
        pointsFinaux = affichagePointsTemps.GetPoints();
        tempsDernierePartie = Mathf.Round(tempsPartie - affichagePointsTemps.GetMinuteur());
        SceneManager.LoadScene(nomScene);

    }

    /// <summary>
    /// Dire à l'affichage d'incrémenter les points
    /// </summary>
    public void incrementerPoints()
    {
        nombreChatsRestants--;
        affichagePointsTemps.IncrementerPoints();
        if (nombreChatsRestants <= 0)
        {
            partieFinie();
        }
    }
}
