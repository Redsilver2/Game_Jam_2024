using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct QuestProgression
{
    private float currentProgressionValue;
    private UnityEvent<float> onProgressionValueChanged;
    private Quest ownerQuest;

    public void Init(Quest ownerQuest)
    {
        this.ownerQuest = ownerQuest;
    }

    public void Progress(float progressionValueToAdd)
    {
        if (!IsDone())
        {
            currentProgressionValue += progressionValueToAdd;

            if(currentProgressionValue > 1f)
            {
                currentProgressionValue = 1f;
            }
        }
    }

    public bool IsDone()
    {
        return currentProgressionValue == 1f;
    }

    public void AddOnProgressionValueChangedEvent(UnityAction<float> action)
    {
        onProgressionValueChanged.AddListener(action);
    }

    public void RemoveOnProgressionValueChangedEvent(UnityAction<float> action)
    {
        onProgressionValueChanged.RemoveListener(action);
    }
}
