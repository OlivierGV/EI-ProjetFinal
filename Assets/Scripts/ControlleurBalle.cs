using UnityEngine;

/// <summary>
/// Balle de fusil
/// </summary>
public class ControlleurBalle : MonoBehaviour
{
    /// <summary>
    /// Se d�truire apr�s une collision
    /// Pour d�boguage, la gestion de la collision avec une taupe est dans Taupe.cs
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            if (!collision.gameObject.CompareTag("eau"))
            {
                //La balle se d�truit apr�s une collision
                Destroy(this.gameObject);
            }
            
        }
        
    }


}
