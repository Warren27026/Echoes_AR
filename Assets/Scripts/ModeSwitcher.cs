using System.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ModeSwitcher : MonoBehaviour
{
    [Header("=== AR Managers ===")]
    public ARTrackedImageManager imageManager;
    public ARFaceManager faceManager;
    public ARCameraManager cameraManager;

    [Header("=== UI ===")]
    public GameObject imageTrackingUI;
    public GameObject faceTrackingUI;

    [Header("=== Timing ===")]
    public float switchDelay = 2.5f;

    void Start()
    {
        Debug.Log("ModeSwitcher démarré !");
        imageManager.enabled = true;
        faceManager.enabled = false;
        if (imageTrackingUI != null) imageTrackingUI.SetActive(true);
        if (faceTrackingUI != null) faceTrackingUI.SetActive(false);
    }

    public void SwitchToFaceTracking()
    {
        Debug.Log("### BOUTON APPUYE ###");
        StartCoroutine(ToFaceCoroutine());
    }

    public void SwitchToImageTracking()
    {
        Debug.Log("### RETOUR APPUYE ###");
        StartCoroutine(ToImageCoroutine());
    }

    private IEnumerator ToFaceCoroutine()
    {
        imageTrackingUI.SetActive(false);
        imageManager.enabled = false;

        // Changer vers caméra frontale
        cameraManager.requestedFacingDirection = CameraFacingDirection.User;

        Debug.Log("Caméra frontale demandée, attente...");
        yield return new WaitForSeconds(switchDelay);
        Debug.Log("Activation du face manager...");

        faceManager.enabled = true;
        faceTrackingUI.SetActive(true);
    }

    private IEnumerator ToImageCoroutine()
    {
        faceTrackingUI.SetActive(false);
        faceManager.enabled = false;
        ClearFaceMasks();

        cameraManager.requestedFacingDirection = CameraFacingDirection.World;

        yield return new WaitForSeconds(switchDelay);

        imageManager.enabled = true;
        imageTrackingUI.SetActive(true);
    }

    private void ClearFaceMasks()
    {
        foreach (ARFace face in faceManager.trackables)
        {
            foreach (MeshRenderer r in face.GetComponentsInChildren<MeshRenderer>())
            {
                r.enabled = false;
            }
        }
    }
}