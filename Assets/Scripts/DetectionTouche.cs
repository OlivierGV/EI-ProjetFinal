using UnityEngine;
using UnityEngine.SceneManagement;

public class DetectionTouche : MonoBehaviour
{
    /// <summary>
    /// Nom de la sc�ne vers laquelle se diriger
    /// </summary>
    [SerializeField]
    private string nomScene;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (string.IsNullOrEmpty(nomScene))
        {
            throw new System.Exception("Aucune sc�ne indiqu�e");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Changer de sc�ne lorsqu'on appuie sur le bouton A
        if(OVRInput.GetDown(OVRInput.Button.Any))
        {
            //Debug.Log("A/X button pressed");
            SceneManager.LoadScene(nomScene);
        }
    }
}
