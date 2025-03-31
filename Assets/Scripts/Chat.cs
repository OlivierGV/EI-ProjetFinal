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
    /// Vitesse de d�placement lorsqu'en marche
    /// </summary>
    [SerializeField]
    float vitesseMarche;

    /// <summary>
    /// Vitesse de d�placement lorsqu'en course
    /// </summary>
    [SerializeField]
    float vitesseCourse;

    /// <summary>
    /// Vitesse active
    /// </summary>
    float vitesse;

    /// <summary>
    /// �tat du chat
    /// </summary>
    int etatChat;

    /// <summary>
    /// Si le joueur a ramass� les points sur le chat
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
            // G�n�re une nouvelle destination al�atoire sur le NavMesh
            Vector3 newPos = RandomNavMeshLocation(rayonDeplacement);
            agent.SetDestination(newPos);
            timer = 0;
        }

        controleurDeplacement();
    }

    /// <summary>
    /// G�n�rer une localisation al�atoire sur le navmesh
    /// Code g�n�r� par intelligence artificielle
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
    /// Les v�rifications � faire lors de l'Awake
    /// </summary>
    /// <exception cref="System.Exception"></exception>
    void verificationDebutScript()
    {
        etatChat = UnityEngine.Random.Range(0, 1);

        if (vitesseMarche <= 0)
        {
            throw new System.Exception("[Chat.cs] La vitesse de marche doit �tre sup�rieur � 0");
        }
        if (vitesseCourse <= 0)
        {
            throw new System.Exception("[Chat.cs] La vitesse de course doit �tre sup�rieur � 0");
        }
        if (vitesseMarche >= vitesseCourse)
        {
            throw new System.Exception("[Chat.cs] La vitesse de marche doit �tre inf�rieur � la vitesse de course");
        }

        agent = GetComponent<NavMeshAgent>();
        timer = tempsEntreDeplacement;
        animator = GetComponent<Animator>();
    }
    

    /// <summary>
    /// Gestion de la vitesse de d�placement et de l'animation
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
