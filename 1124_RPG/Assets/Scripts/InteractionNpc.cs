using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionNpc : MonoBehaviour
{
    [SerializeField] DialogueData dialData = null;

    [SerializeField] int npcNum;

    public GameObject[] icons;

    enum NpcState { normal, haveQuest, doingQuest, doneQuest }
    [SerializeField] NpcState npcState = NpcState.normal;

    [SerializeField]
    int scriptNum = 0;      //대사뭉치
    int dialogueNum = 0;    //몇번째 줄 대사 

    private void Update()
    {
        switch (npcState)
        {
            case NpcState.normal:
                break;
        }
    }


    public int GetNum()
    {
        return npcNum;
    }

    public Dictionary<string, object> GetDialogue()
    {

        Dictionary<string, object> dialogue =
            dialData.GetDialogue(npcNum, scriptNum, dialogueNum);
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
            if (scriptNum >= dialData.GetScriptNum(npcNum)) scriptNum = 0; 
        }
    }

    public bool TalkDone()
    {
        return dialogueNum >= dialData.GetDialNum(npcNum, scriptNum);
    }
}
