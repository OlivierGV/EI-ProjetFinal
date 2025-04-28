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
    /// Distance maximale du tir
    /// </summary>
    [SerializeField]
    private float distanceMax = 50f;

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
        //Ligne pour savoir o� qu'on tire
        //ligneTir.SetPosition(0, baril.position);
        //ligneTir.SetPosition(1, baril.position + (baril.transform.forward * distanceMax));

        //Si on tire la gachette, tirer le fusil
        if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
        {
            onTirer();
        }
        else
        {
            //Sinon on arr�te le bruit
        }

    }

    /// <summary>
    /// Tirer une balle
    /// </summary>
    public void onTirer()
    {

        //Cr�er la balle
        GameObject balleExistante = Instantiate(prefabBalle);
        balleExistante.transform.position = baril.position;
        balleExistante.transform.rotation = baril.rotation;
        balleExistante.transform.Rotate(0, 90, 0);
        //Momentum
        balleExistante.GetComponent<Rigidbody>().AddForce(baril.forward * vitesseBalle, ForceMode.Impulse);

        //La d�truire apr�s 10 secondes
        Destroy(balleExistante, 10);

    }


}
