using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionNpc : MonoBehaviour
{
    [SerializeField] QuestData questData = null;

    [SerializeField] int npcNum;

    enum NpcState { normal, haveQuest, doingQuest, doneQuest }
    [SerializeField] NpcState npcState = NpcState.normal;

    int scriptNum = 0;      //대사뭉치
    int dialogueNum = 0;    //몇번째 줄 대사 
    
    public int GetNum()
    {
        return npcNum;
    }

    public Dictionary<string, object> GetDialogue()
    {
        Dictionary<string, object> dialogue =
            questData.GetDialogue(npcNum, scriptNum, dialogueNum);
        ChangeValue(dialogue);
        return dialogue;
    }

    public void ChangeValue(Dictionary<string, object> dialogue)
    {
        dialogueNum++;
        if (dialogue == null)
        {
            dialogueNum = 0;
            scriptNum++;
            npcNum = (int)dialogue["nextNpc"];

        }
    }

}
