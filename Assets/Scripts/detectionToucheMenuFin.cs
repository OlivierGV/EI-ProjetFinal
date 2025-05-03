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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (string.IsNullOrEmpty(nomScene))
        {
            throw new System.Exception("Aucune scène indiquée");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Changer de scène lorsqu'on appuie sur le bouton A
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            //Debug.Log("A/X button pressed");
            SceneManager.LoadScene(nomScene);
        }
        // Terminer le jeu si on appuie sur le bouton X
        if (OVRInput.GetDown(OVRInput.Button.Three))
        {
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
}
