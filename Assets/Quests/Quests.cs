using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Quests
{
    protected string name;
    protected string description;
    [SerializeField] protected GameObject neededVegetable;

    public Quests(string currentName,string currentDescription)
    {
        name = currentName;
        description = currentDescription;
    }

    public abstract void QuestRequirements();
    public abstract void QuestReward();

    public abstract void QuestMalus();
}
