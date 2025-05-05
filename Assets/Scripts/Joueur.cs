using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;
using static OVRPlugin;

/// <summary>
/// G�rer le mouvement du joueur
/// </summary>
public class Joueur : MonoBehaviour
{
    /// <summary>
    /// Vitesse du joueur
    /// </summary>
    [SerializeField]
    int vitesse = 3;

    /// <summary>
    /// R�f�rence � l'ancre des yeux
    /// </summary>
    [SerializeField]
    GameObject ancreYeux;

    // Update is called once per frame
    void Update()
    {
        //Se d�placer selon le thumbstick gauche
        Vector2 deplacement = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        if (deplacement != Vector2.zero) {
            //Emp�cher de s'envoler
            Vector3 enAvant = ancreYeux.transform.forward;
            Vector3 aDroite = ancreYeux.transform.right;
            enAvant.y = 0;
            aDroite.y = 0;
            gameObject.transform.Translate(enAvant * vitesse * deplacement.y * Time.deltaTime, Space.World);
            gameObject.transform.Translate(aDroite * vitesse * deplacement.x * Time.deltaTime, Space.World);
        }
    }
}
