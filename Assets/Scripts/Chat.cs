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
        /// Le temps entre les d�placements
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
        /// V�locit� du minou
        /// </summary>
        float velocite;

        /// <summary>
        /// �tat du chat
        /// </summary>
        bool enCourse;

        /// <summary>
        /// Si le joueur a ramass� les points sur le chat
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
        faireMourir();
        }

        // Update is called once per frame
        void Update()
        {
            timer += Time.deltaTime;

            if (timer >= tempsEntreDeplacement)
            {
                // G�n�re une nouvelle destination al�atoire sur le NavMesh
                Vector3 newPos = RandomNavMeshLocation(rayonDeplacement);
                agent.SetDestination(newPos);
                timer = 0;
                tempsEntreDeplacement = Random.Range(2f, 5f);
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
            audioSource = GetComponent<AudioSource>();
        }

        /// <summary>
        /// Appeler l'animation de mort
        /// </summary>
        void faireMourir()
        {
            StartCoroutine(Mourrir());
        }
    

        /// <summary>
        /// Gestion de la vitesse de d�placement et de l'animation
        /// </summary>
        void controleurDeplacement()
        {
            animator.SetBool("enCourse", enCourse);

            vitesse = enCourse ? vitesseCourse : vitesseMarche;
            agent.speed = vitesse;
        }

        /// <summary>
        /// Calculer la vitesse avec deux d�placements
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
        /// Sc�nario de mort
        /// </summary>
        /// <returns></returns>
        IEnumerator Mourrir()
        {
            audioSource.Play();

            yield return new WaitWhile(() => audioSource.isPlaying);

            Destroy(gameObject);
        }
    }
