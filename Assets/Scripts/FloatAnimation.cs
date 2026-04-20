using UnityEngine;

public class FloatAnimation : MonoBehaviour
{
    public float amplitude = 0.005f; // Hauteur du mouvement
    public float vitesse = 0.5f;      // Vitesse du mouvement
    private Vector3 posInitiale;

    void Start() => posInitiale = transform.localPosition;

    void Update()
    {
        // On fait varier la position Y avec un sinus pour un mouvement fluide
        float nouveauY = posInitiale.y + Mathf.Sin(Time.time * vitesse) * amplitude;
        transform.localPosition = new Vector3(posInitiale.x, nouveauY, posInitiale.z);
    }
}