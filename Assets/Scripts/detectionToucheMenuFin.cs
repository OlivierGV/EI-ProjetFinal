using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Détecter une touche lors du menu de fin
/// </summary>
public class detectionToucheMenuFin : MonoBehaviour
{
    /// <summary>
    /// Nom de la scène vers laquelle se diriger si on recommence le jeu.
    /// </summary>
    [SerializeField]
    private string nomScene;

    /// <summary>
    /// L'Animator attaché à l'objet qui joue l'animation
    /// </summary>
    [SerializeField]
    private Animator monAnimator;

    /// <summary>
    /// Le nom du trigger dans l'Animator
    /// </summary>
    [SerializeField]
    private string nomTriggerAnimation = "finScene";

    private bool animationLancee = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (string.IsNullOrEmpty(nomScene))
        {
            throw new System.Exception("Aucune scène indiquée");
        }

        if (monAnimator == null)
        {
            Debug.LogError("Aucun Animator assigné");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Changer de scène lorsqu'on appuie sur le bouton A
        if (!animationLancee && OVRInput.GetDown(OVRInput.Button.One))
        {
            //Debug.Log("A/X button pressed");
            SceneManager.LoadScene(nomScene);
            StartCoroutine(JouerAnimationEtChangerScene());
            animationLancee = true;
        }
        // Terminer le jeu si on appuie sur le bouton X
        if (!animationLancee && OVRInput.GetDown(OVRInput.Button.Three))
        {
            StartCoroutine(JouerAnimationEtChangerScene());
            animationLancee = true;
            //Si on est en mode éditeur
            if (UnityEditor.EditorApplication.isPlaying)
            {
                UnityEditor.EditorApplication.isPlaying = false;
            }
            else
            {
                Application.Quit();
            }
            
        }
    }

    // Coroutine pour lancer la transition
    private IEnumerator JouerAnimationEtChangerScene()
    {
        monAnimator.SetTrigger(nomTriggerAnimation);

        yield return null;

        AnimatorStateInfo info = monAnimator.GetCurrentAnimatorStateInfo(0);
        float duree = info.length;

        yield return new WaitForSeconds(duree);

        SceneManager.LoadScene(nomScene);
    }
}
