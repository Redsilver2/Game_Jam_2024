using UnityEngine;

sealed class PearCameraController : PlayerCameraController
{
    [SerializeField] private Transform holdItemTransform;
    
    public void HoldItem(Transform itemTransform, float positionZOffset)
    {
        itemTransform.SetParent(transform);
        itemTransform.localPosition = Vector3.forward * positionZOffset;
    }
}
