using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DetectionTouche : MonoBehaviour
{
    /// <summary>
    /// Nom de la scène vers laquelle se diriger
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
        if(!animationLancee && OVRInput.GetDown(OVRInput.Button.Any))
        {
            StartCoroutine(JouerAnimationEtChangerScene());
            animationLancee = true;
        }
    }
    
    // Coroutine pour lancer la transition
    private IEnumerator JouerAnimationEtChangerScene()
    {
        monAnimator.SetTrigger(nomTriggerAnimation);

        AnimatorStateInfo info = monAnimator.GetCurrentAnimatorStateInfo(0);
        float duree = info.length;

        yield return new WaitForSeconds(0.1f);
        yield return new WaitForSeconds(duree);

        SceneManager.LoadScene(nomScene);
    }
}
