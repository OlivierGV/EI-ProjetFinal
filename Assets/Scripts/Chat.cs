    using System.Collections;
    using Unity.VisualScripting;
    using UnityEditor.EditorTools;
    using UnityEngine;
    using UnityEngine.AI;

    public class Chat : MonoBehaviour
    {
        /// <summary>
        /// Distance max que le chat peut parcourir 
        /// </summary>
        [SerializeField]
        float rayonDeplacement = 20f;

        /// <summary>
        /// Le temps entre les déplacements
        /// </summary>
        [SerializeField]
        float tempsEntreDeplacement = 0f;

        /// <summary>
        /// NavMesh
        /// </summary>
        private NavMeshAgent agent;
    
        /// <summary>
        /// Temps du jeu
        /// </summary>
        private float timer;

        /// <summary>
        /// Temps du jeu (pour les miaulements)
        /// </summary>
        private float timerMiaulement;

        /// <summary>
        /// Temps entre les miaulements
        /// </summary>
        private int tempsCible;

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
        /// Vélocité du minou
        /// </summary>
        float velocite;

        /// <summary>
        /// État du chat
        /// </summary>
        bool enCourse;

        /// <summary>
        /// Si le joueur a ramassé les points sur le chat
        /// </summary>
        [SerializeField]
        bool pointRecupere = false;

        /// <summary>
        /// Animator du chat
        /// </summary>
        Animator animator;

        /// <summary>
        /// AudioSource du chat
        /// </summary>
        AudioSource audioSource;

        void Awake()
        {
            StartCoroutine(CalculVitesse());
            verificationDebutScript();
            miauler();
        }

        // Update is called once per frame
        void Update()
        {
            timer += Time.deltaTime;
            timerMiaulement += Time.deltaTime; 

            if (timer >= tempsEntreDeplacement)
            {
                // Génère une nouvelle destination aléatoire sur le NavMesh
                Vector3 newPos = RandomNavMeshLocation(rayonDeplacement);
                agent.SetDestination(newPos);
                timer = 0;
                tempsEntreDeplacement = Random.Range(2f, 5f);
            }

            if (timerMiaulement >= tempsCible)
            {
                timerMiaulement = 0f;
                miauler();
                audioSource.Play();
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
            audioSource = GetComponent<AudioSource>();
        }

        /// <summary>
        /// Appeler l'animation de mort
        /// </summary>
        void faireMourir()
        {
            Destroy(gameObject);
        }
    

        /// <summary>
        /// Gestion de la vitesse de déplacement et de l'animation
        /// </summary>
        void controleurDeplacement()
        {
            animator.SetBool("enCourse", enCourse);

            vitesse = enCourse ? vitesseCourse : vitesseMarche;
            agent.speed = vitesse;
        }

        void miauler()
        {
            int[] tempsRonds = { 15, 20, 30, 45, 60, 75, 90 };
            tempsCible = tempsRonds[Random.Range(0, tempsRonds.Length)];
        }

        /// <summary>
        /// Calculer la vitesse avec deux déplacements
        /// </summary>
        /// <returns></returns>
        IEnumerator CalculVitesse()
        {
            bool doitJouer = true;
            while (doitJouer)
            {
                Vector3 prevPos = transform.position;

                yield return new WaitForFixedUpdate();

                velocite = Mathf.RoundToInt(Vector3.Distance(transform.position, prevPos) / Time.fixedDeltaTime);
            }
        }

    /// <summary>
    /// Si on collisionne avec de l'eau, on s'enfuis
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            if (!collision.gameObject.CompareTag("eau"))
            {
                LogiqueJeu.Instance.incrementerPoints();
                faireMourir();
            }

        }
    }
}
