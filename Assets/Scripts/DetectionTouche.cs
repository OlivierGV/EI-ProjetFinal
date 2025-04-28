using UnityEngine;
using UnityEngine.SceneManagement;

public class DetectionTouche : MonoBehaviour
{
    /// <summary>
    /// Nom de la scène vers laquelle se diriger
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
        if(OVRInput.GetDown(OVRInput.Button.Any))
        {
            //Debug.Log("A/X button pressed");
            SceneManager.LoadScene(nomScene);
        }
    }
}
