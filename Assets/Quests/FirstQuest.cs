using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstQuest : Quests
{
    
    public FirstQuest() : base("Your first Quest : " ,"You must save the Kiwi ! ")
    {
    }

    public override void QuestMalus()
    {
        throw new System.NotImplementedException();
    }

    public override void QuestRequirements()
    {
        
    }

    public override void QuestReward()
    {
        throw new System.NotImplementedException();
    }

   
}
