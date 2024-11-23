using UnityEngine;

[System.Serializable]
public struct PlayerCharacterData
{
    [SerializeField] private Camera camera;
    [SerializeField] private PlayerController controller;

    public Camera Camera => camera;
    public PlayerController Controller => controller;

    public PlayerCharacterData(Camera camera, PlayerController controller)
    {
        this.camera = camera;
        this.controller = controller;
    }
}
