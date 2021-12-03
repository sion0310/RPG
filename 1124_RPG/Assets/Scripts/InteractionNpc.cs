using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionNpc : MonoBehaviour
{
    [SerializeField] QuestData questData = null;

    [SerializeField] int npcNum;

    enum NpcState { normal, haveQuest, doingQuest, doneQuest }
    [SerializeField] NpcState npcState = NpcState.normal;

    [SerializeField]
    int scriptNum = 0;      //대사뭉치
    int dialogueNum = 0;    //몇번째 줄 대사 

    int nextNpc;


    public int GetNum()
    {
        return npcNum;
    }

    public Dictionary<string, object> GetDialogue()
    {
        nextNpc = npcNum;
        Dictionary<string, object> dialogue =
            questData.GetDialogue(nextNpc, scriptNum, dialogueNum);
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
            

            nextNpc = (int)questData.GetDialogue(npcNum, scriptNum, 0)["nextNpc"];

        }
    }

    public bool TalkDone()
    {
        return dialogueNum >= questData.GetDialNum(npcNum, scriptNum);
    }
}
