using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.AI;

public class Chat : MonoBehaviour
{
    [SerializeField]
    float rayonDeplacement = 20f;

    [SerializeField]
    float tempsEntreDeplacement = 5f;

    private NavMeshAgent agent;
    
    private float timer;

    /// <summary>
    /// Vitesse de déplacement lorsqu'en marche
    /// </summary>
    [SerializeField]
    float vitesseMarche;

    /// <summary>
    /// Vitesse de déplacement lorsqu'en course
    /// </summary>
    [SerializeField]
    float vitesseCourse;

    /// <summary>
    /// Vitesse active
    /// </summary>
    float vitesse;

    /// <summary>
    /// État du chat
    /// </summary>
    int etatChat;

    /// <summary>
    /// Si le joueur a ramassé les points sur le chat
    /// </summary>
    [SerializeField]
    bool pointRecupere = false;

    /// <summary>
    /// Animator du chat
    /// </summary>
    Animator animator;

    void Awake()
    {   
        verificationDebutScript();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= tempsEntreDeplacement)
        {
            etatChat = 2;
            // Génère une nouvelle destination aléatoire sur le NavMesh
            Vector3 newPos = RandomNavMeshLocation(rayonDeplacement);
            agent.SetDestination(newPos);
            timer = 0;
        }

        controleurDeplacement();
    }

    /// <summary>
    /// Générer une localisation aléatoire sur le navmesh
    /// Code généré par intelligence artificielle
    /// </summary>
    /// <param name="radius"></param>
    /// <returns></returns>
    public Vector3 RandomNavMeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;

        randomDirection += transform.position;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, radius, -1);
        return navHit.position;
    }

    /// <summary>
    /// Les vérifications à faire lors de l'Awake
    /// </summary>
    /// <exception cref="System.Exception"></exception>
    void verificationDebutScript()
    {
        etatChat = UnityEngine.Random.Range(0, 1);

        if (vitesseMarche <= 0)
        {
            throw new System.Exception("[Chat.cs] La vitesse de marche doit être supérieur à 0");
        }
        if (vitesseCourse <= 0)
        {
            throw new System.Exception("[Chat.cs] La vitesse de course doit être supérieur à 0");
        }
        if (vitesseMarche >= vitesseCourse)
        {
            throw new System.Exception("[Chat.cs] La vitesse de marche doit être inférieur à la vitesse de course");
        }

        agent = GetComponent<NavMeshAgent>();
        timer = tempsEntreDeplacement;
        animator = GetComponent<Animator>();
    }
    

    /// <summary>
    /// Gestion de la vitesse de déplacement et de l'animation
    /// </summary>
    void controleurDeplacement()
    {
        animator.SetInteger("etat", etatChat);

        if (etatChat > 0)
        {
            vitesse = etatChat == 1 ? vitesseCourse : vitesseMarche;
        } else
        {
            vitesse = 0;
        }

        agent.speed = vitesse;
    }
}
