using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using System;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class ControlleurFusil : MonoBehaviour
{
    /// <summary>
    /// Emplacement du baril du fusil
    /// </summary>
    [SerializeField]
    private Transform baril;

    /// <summary>
    /// Prefab d'une balle
    /// </summary>
    [SerializeField]
    private GameObject prefabBalle;

    /// <summary>
    /// Ligne de tir
    /// </summary>
    //[SerializeField]
    //private LineRenderer ligneTir;


    /// <summary>
    /// Vitesse de la balle
    /// </summary>
    [SerializeField]
    private float vitesseBalle = 5f;

    private AudioSource sourceAudio;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //ligneTir = GetComponent<LineRenderer>();
        //ligneTir.enabled = true;
        sourceAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Ligne pour savoir où qu'on tire
        //ligneTir.SetPosition(0, baril.position);
        //ligneTir.SetPosition(1, baril.position + (baril.transform.forward * distanceMax));

        //Si on tire la gachette, tirer le fusil
        if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
        {
            onTirer();
            if (!sourceAudio.isPlaying)
            {
                sourceAudio.Play();
            }
            
        }
        else
        {
            //Sinon on arrête le bruit
            if (sourceAudio.isPlaying)
            {
                sourceAudio.Stop();
            }
        }

    }

    /// <summary>
    /// Tirer une balle
    /// </summary>
    public void onTirer()
    {

        //Créer la balle
        GameObject balleExistante = Instantiate(prefabBalle);
        balleExistante.transform.position = baril.position;
        balleExistante.transform.rotation = baril.rotation;
        balleExistante.transform.Rotate(0, 90, 0);
        //Momentum
        balleExistante.GetComponent<Rigidbody>().AddForce(baril.forward * vitesseBalle, ForceMode.Impulse);

        //La détruire après 10 secondes
        Destroy(balleExistante, 10);

    }


}
