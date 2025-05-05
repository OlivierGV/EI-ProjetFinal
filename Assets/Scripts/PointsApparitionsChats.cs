using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// G�rer l'apparition des chats
/// </summary>
public class PointsApparitionsChats : MonoBehaviour
{
    /// <summary>
    /// Gameobject � faire appara�tre
    /// </summary>
    [SerializeField]
    private GameObject chat;

    /// <summary>
    /// Rayon d'apparition
    /// </summary>
    [SerializeField]
    private float rayonRecherche;

    /// <summary>
    /// Combien de gameobjects
    /// </summary>
    [SerializeField]
    public float nombreApparition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        faireApparaitreChats();
    }

    /// <summary>
    /// Faire appara�tre les chats 
    /// </summary>
    void faireApparaitreChats()
    {
        /** Code g�n�r� par intelligence artificielle */
        for(int i = 0; i < nombreApparition; i++)
        {
            Vector3 directionAleatoire = Random.insideUnitCircle * rayonRecherche;
            directionAleatoire += transform.position;

            NavMeshHit hit;

            if (NavMesh.SamplePosition(directionAleatoire, out hit, rayonRecherche, NavMesh.AllAreas))
            {
                Instantiate(chat, hit.position, Quaternion.identity);
            }
        }
        /** fin de la g�n�ration */
    }
}
