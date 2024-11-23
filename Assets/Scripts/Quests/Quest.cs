using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Quest
{
    [SerializeField] private string questName;

    [Space]
    [SerializeField] private UnityEvent onQuestStart;
    [SerializeField] private UnityEvent onQuestEnd;

    private bool isActif     = false;
    private bool isCompleted = false;

    public void Init()
    {

    }

    public void Start()
    {

    }

    public void End()
    {

    }
}
