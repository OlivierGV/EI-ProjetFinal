using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Classe "maitre" qui g�re la logique de jeu : incr�mentation de points et changements de sc�nes
/// </summary>
public class LogiqueJeu : MonoBehaviour
{
    /// <summary>
    /// R�f�rence aux points et au temps restant d'une partie
    /// </summary>
    [SerializeField]
    AffichagePointsTemps affichagePointsTemps;

    /// <summary>
    /// Nom de la sc�ne vers laquelle se diriger apr�s une partie
    /// </summary>
    [SerializeField]
    private string nomScene;

    /// <summary>
    /// R�f�rence aux points d'apparitions
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
    /// Permet d'y acc�der dans le menu de fin
    /// </summary>
    public static int pointsFinaux = 0;

    /// <summary>
    /// Variable statique pour le tempss �coul� de la partie
    /// Permet d'y acc�der dans le menu de fin
    /// </summary>
    public static float tempsDernierePartie = 0;

    /// <summary>
    /// L'Animator attach� � l'objet qui joue l'animation
    /// </summary>
    [SerializeField]
    private Animator monAnimator;

    /// <summary>
    /// Le nom du trigger dans l'Animator
    /// </summary>
    [SerializeField]
    private string nomTriggerAnimation = "finScene";

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
            throw new System.Exception("Aucune sc�ne indiqu�e");
        }
        if (monAnimator == null)
        {
            Debug.LogError("Aucun Animator assign�");
        }
        nombreChatsRestants = pointsApparitions.nombreApparition;
        DebutPartie();
    }

    /// <summary>
    /// Au d�but d'une partie, commencer le minuteur et r�initialiser les points finaux
    /// </summary>
    public void DebutPartie()
    {
        affichagePointsTemps.Commencer(tempsPartie);
        pointsFinaux = 0;
    }

    /// <summary>
    /// Quand une partie est finie, changer de sc�ne
    /// </summary>
    public void partieFinie()
    {
        StartCoroutine(JouerAnimationEtChangerScene());

        pointsFinaux = affichagePointsTemps.GetPoints();
        tempsDernierePartie = Mathf.Round(tempsPartie - affichagePointsTemps.GetMinuteur());
        SceneManager.LoadScene(nomScene);

    }

    /// <summary>
    /// Dire � l'affichage d'incr�menter les points
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

    // Coroutine pour lancer la transition
    private IEnumerator JouerAnimationEtChangerScene()
    {
        monAnimator.SetTrigger(nomTriggerAnimation);

        yield return null;

        AnimatorStateInfo info = monAnimator.GetCurrentAnimatorStateInfo(0);
        float duree = info.length;

        yield return new WaitForSeconds(duree);

        SceneManager.LoadScene(nomScene);
    }
}
