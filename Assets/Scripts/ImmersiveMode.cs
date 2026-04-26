using UnityEngine;

public class ImmersiveMode : MonoBehaviour
{
    void Start() => SetImmersiveMode();
    void OnApplicationFocus(bool hasFocus) { if (hasFocus) SetImmersiveMode(); }

    void SetImmersiveMode()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        using var player = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        using var activity = player.GetStatic<AndroidJavaObject>("currentActivity");
        activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
        {
            using var window = activity.Call<AndroidJavaObject>("getWindow");
            // API moderne (Android 11+)
            using var insetsController = window.Call<AndroidJavaObject>("getInsetsController");
            if (insetsController != null)
            {
                // BEHAVIOR_SHOW_TRANSIENT_BARS_BY_SWIPE = 2
                insetsController.Call("setSystemBarsBehavior", 2);
                // Type: navigationBars() = 8, statusBars() = 1
                insetsController.Call("hide", 8 | 1);
            }
        }));
#endif
    }
}