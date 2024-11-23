
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

sealed class CarrotController : PlayerController
{
   [Space]
   [Header("Interaction Settings")]
   [SerializeField] private float  interactionRange = 10f;
   [SerializeField] private float  pushForce = 10f;

   [Space]
   [SerializeField] private Image           interactionCrosshair;
   [SerializeField] private TextMeshProUGUI interactionNameDisplayer;


    protected override void Awake()
    {
        base.Awake();

        InteractionManager              interactionManager = ownerCameraController.GetComponent<InteractionManager>();
        if (interactionManager == null) interactionManager = ownerCameraController.AddComponent<InteractionManager>();

        interactionManager.Init(interactionRange, pushForce, interactionCrosshair, interactionNameDisplayer);

        AddOnStateChangedEvent(isEnabled =>
        {
            interactionManager.enabled = isEnabled;
        });
    }
}
