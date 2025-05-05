using TMPro;
using UnityEngine;

/// <summary>
/// Affichage des points finaux apr�s une partie
/// </summary>
[RequireComponent(typeof(TextMeshProUGUI))]
public class AffichageMenuFin : MonoBehaviour
{
    /// <summary>
    /// R�f�rence au textfield
    /// </summary>
    private TextMeshProUGUI text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.SetText("Partie finie!\n Vous avez accumul� " + LogiqueJeu.pointsFinaux + " points en " + LogiqueJeu.tempsDernierePartie + " secondes! \n Appuyez sur A pour recommencer ou X pour arr�ter de jouer.");
    }
}
