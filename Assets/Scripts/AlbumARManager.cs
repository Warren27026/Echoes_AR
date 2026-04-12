using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class AlbumARManager : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager imageManager;
    [SerializeField] private List<AlbumData> listeAlbums; // Glisse tes ScriptableObjects ici

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
            UpdateImage(updatedImage);
        }
    }

    void UpdateImage(ARTrackedImage trackedImage)
    {
        // On récupère le script qui est sur le Prefab qui vient d'apparaître
        var handler = trackedImage.GetComponentInChildren<AlbumTrackableEventHandler>();

        if (handler != null)
        {
            // On cherche les données qui correspondent au nom de l'image détectée
            AlbumData data = listeAlbums.Find(a => a.name == trackedImage.referenceImage.name);

            if (data != null)
            {
                handler.SetupAlbum(data);
            }
        }
    }
}