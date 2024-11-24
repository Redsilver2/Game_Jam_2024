using UnityEngine;

[System.Serializable]
public class PlayerCharacterData
{
    [SerializeField] private Camera camera;
    [SerializeField] private PlayerController controller;

    private bool isCharacterUnlocked;

    public Camera Camera => camera;
    public PlayerController Controller => controller;
    public bool IsCharacterUnlocked => isCharacterUnlocked;

    public PlayerCharacterData(Camera camera, PlayerController controller, bool isCharacterUnlocked)
    {
        this.camera = camera;
        this.controller = controller;
        this.isCharacterUnlocked = isCharacterUnlocked;
    }

    public void SetIsCharacterUnlocked(bool isCharacterUnlocked) => this.isCharacterUnlocked = isCharacterUnlocked;
}
