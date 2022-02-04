using UnityEngine;
using Valve.VR;

public class Sceneloader : MonoBehaviour
{
    public void LoadNewScene(string nextScene)
    {
        SteamVR_LoadLevel.Begin(nextScene);
    }
}
