using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;
using static OVRPlugin;

public class Joueur : MonoBehaviour
{
    [SerializeField]
    int vitesse = 3;

    [SerializeField]
    GameObject ancreYeux;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Se déplacer selon le thumbstick gauche
        Vector2 deplacement = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        if (deplacement != Vector2.zero) {
            //Empêcher de s'envoler
            Vector3 enAvant = ancreYeux.transform.forward;
            Vector3 aDroite = ancreYeux.transform.right;
            enAvant.y = 0;
            aDroite.y = 0;
            gameObject.transform.Translate(enAvant * vitesse * deplacement.y * Time.deltaTime, Space.World);
            gameObject.transform.Translate(aDroite * vitesse * deplacement.x * Time.deltaTime, Space.World);
        }
    }


    /**
     * 
     * 
     *         if (OVRInput.GetUp(OVRInput.Button.One))
        {
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
        }
    */
}
