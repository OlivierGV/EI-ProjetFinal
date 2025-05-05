using TMPro;
using UnityEngine;

/// <summary>
/// Affichage des points finaux après une partie
/// </summary>
[RequireComponent(typeof(TextMeshProUGUI))]
public class AffichageMenuFin : MonoBehaviour
{
    /// <summary>
    /// Référence au textfield
    /// </summary>
    private TextMeshProUGUI text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.SetText("Partie finie!\n Vous avez accumulé " + LogiqueJeu.pointsFinaux + " points en " + LogiqueJeu.tempsDernierePartie + " secondes! \n Appuyez sur A pour recommencer ou X pour arrêter de jouer.");
    }
}
