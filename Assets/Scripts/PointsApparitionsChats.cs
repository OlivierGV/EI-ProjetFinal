using UnityEngine;
using UnityEngine.AI;

public class PointsApparitionsChats : MonoBehaviour
{
    [SerializeField]
    private GameObject chat;

    [SerializeField]
    private float rayonRecherche;

    [SerializeField]
    private float nombreApparition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        faireApparaitreChats();
    }

    /// <summary>
    /// Faire apparaître les chats 
    /// </summary>
    void faireApparaitreChats()
    {
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
    }
}
