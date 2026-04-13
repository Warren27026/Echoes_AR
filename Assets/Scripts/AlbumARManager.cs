using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class AlbumARManager : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager imageManager;
    [SerializeField] private List<AlbumData> listeAlbums;

    private void OnEnable() => imageManager.trackedImagesChanged += OnChanged;
    private void OnDisable() => imageManager.trackedImagesChanged -= OnChanged;

    void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var newImage in eventArgs.added)
        {
            UpdateImage(newImage);
        }
        foreach (var updatedImage in eventArgs.updated)
        {
            // On ne met à jour que si l'image est bien suivie
            if (updatedImage.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
            {
                UpdateImage(updatedImage);
            }
        }
    }

    void UpdateImage(ARTrackedImage trackedImage)
    {
        // 1. FORCER L'ACTIVATION : On active le visuel qui est attaché à l'image
        trackedImage.gameObject.SetActive(true);

        // 2. RECUPERER LE HANDLER
        var handler = trackedImage.GetComponentInChildren<AlbumTrackableEventHandler>(true); // Le 'true' permet de trouver même si c'est caché

        if (handler != null)
        {
            // 3. RECHERCHE DES DONNÉES
            AlbumData data = listeAlbums.Find(a => a.name == trackedImage.referenceImage.name);

            if (data != null)
            {
                handler.SetupAlbum(data);
                Debug.Log($"Succès : Données appliquées pour {data.name}");
            }
            else
            {
                Debug.LogWarning($"Attention : Aucune donnée trouvée pour l'image nommée '{trackedImage.referenceImage.name}'");
            }
        }
        else
        {
            Debug.LogError("Erreur : Le script AlbumTrackableEventHandler est introuvable sur le Prefab !");
        }
    }
}