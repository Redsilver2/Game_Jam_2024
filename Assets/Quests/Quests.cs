using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Quests
{
    [SerializeField] private string questName;
    [SerializeField] private UnityEvent onQuestStart;
    [SerializeField] private UnityEvent onQuestEnd;

}
