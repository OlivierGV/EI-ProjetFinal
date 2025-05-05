using TMPro;
using UnityEngine;

/// <summary>
/// Affichage des points accumulés et du temps restant à une partie
/// </summary>
[RequireComponent(typeof(TextMeshProUGUI))]
public class AffichagePointsTemps : MonoBehaviour
{
    /// <summary>
    /// Temps d'une partie en secondes
    /// </summary>
    [SerializeField]
    private int tempsPartie = 180;

    /// <summary>
    /// Le temps restant
    /// </summary>
    private float minuteur;

    /// <summary>
    /// Points cumulés
    /// </summary>
    private int points = 0;

    /// <summary>
    /// La partie est-elle en cours?
    /// </summary>
    private bool timerActif = false;


    /// <summary>
    /// Référence au textfield
    /// </summary>
    private TextMeshProUGUI text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        minuteur = tempsPartie;
    }

    // Update is called once per frame
    void Update()
    {
        //Enlever le temps écoulé depuis le dernier tout de boucle
        if (timerActif)
        {
            minuteur -= Time.deltaTime;
        }

        //Avoir du beau temps
        float minutes = Mathf.Floor(minuteur / 60);
        float secondes = Mathf.Floor(minuteur % 60);
        string minutesText = minutes.ToString();
        string secondesText;

        if (secondes < 10)
        {
            secondesText = "0" + Mathf.RoundToInt(secondes).ToString();
        }
        else
        {
            secondesText = Mathf.RoundToInt(secondes).ToString();
        }

        //S'il ne reste plus de temps, finir la partie
        if (minuteur <= 0)
        {
            timerActif = false;
            LogiqueJeu.Instance.partieFinie(points, tempsPartie);
        }
        else
        {
            //Affichage
            text.SetText("Vos points: \n" + points + "\n Temps restant:  \n" + minutesText + ":" + secondesText);
        }
    }

    /// <summary>
    /// Commencer le minuteur et le remettre à sa valeur initiale
    /// </summary>
    public void Commencer()
    {
        timerActif = true;
        minuteur = tempsPartie;
        points = 0;
    }

    /// <summary>
    /// Ajouter des points
    /// </summary>
    public void IncrementerPoints()
    {
        //On n'ajoute pas des points quand la partie est finie
        if (timerActif)
        {
            points += 50;
        }
    }

}
