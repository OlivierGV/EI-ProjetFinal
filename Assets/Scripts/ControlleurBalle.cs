using UnityEngine;

/// <summary>
/// Balle de fusil
/// </summary>
public class ControlleurBalle : MonoBehaviour
{
    /// <summary>
    /// Se détruire après une collision
    /// Pour déboguage, la gestion de la collision avec une taupe est dans Taupe.cs
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            if (!collision.gameObject.CompareTag("eau"))
            {
                //La balle se détruit après une collision
                Destroy(this.gameObject);
            }
            
        }
        
    }


}
