using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class AlbumARManager : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager imageManager;
    [SerializeField] private List<AlbumData> listeAlbums;
    [SerializeField] private GameObject prefabAInstancier; // Ton prefab AlbumContent

    // Dictionnaire pour stocker l'unique instance par image
    private Dictionary<string, GameObject> instances = new Dictionary<string, GameObject>();

    private void OnEnable() => imageManager.trackedImagesChanged += OnChanged;
    private void OnDisable() => imageManager.trackedImagesChanged -= OnChanged;

    void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        // 1. Création unique pour les nouvelles images
        foreach (var newImage in eventArgs.added)
        {
            string imageName = newImage.referenceImage.name;

            if (!instances.ContainsKey(imageName))
            {
                // On instancie NOUS-MÊMES le prefab comme enfant de l'image détectée
                GameObject instance = Instantiate(prefabAInstancier, newImage.transform);
                instances.Add(imageName, instance);

                // Initialisation des données
                UpdateAlbumData(imageName, instance);
            }
        }

        // 2. Gestion de la visibilité pour les mises à jour
        foreach (var updatedImage in eventArgs.updated)
        {
            string imageName = updatedImage.referenceImage.name;
            if (instances.ContainsKey(imageName))
            {
                // On n'affiche l'objet que si le tracking est optimal
                bool isVisible = updatedImage.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking;
                instances[imageName].SetActive(isVisible);
            }
        }
    }

    void UpdateAlbumData(string imageName, GameObject instance)
    {
        var handler = instance.GetComponentInChildren<AlbumTrackableEventHandler>(true);
        if (handler != null)
        {
            AlbumData data = listeAlbums.Find(a => a.name == imageName);
            if (data != null)
            {
                handler.SetupAlbum(data);
                Debug.Log($"Données appliquées : {data.name}");
            }
        }
    }
}