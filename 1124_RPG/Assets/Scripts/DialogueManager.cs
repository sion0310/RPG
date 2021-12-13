using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public InteractionCtrl interCtrl = null;

    public QuestManager questManager = null;
    public DialogueUICtrl dialUI = null;

    public DialogueData dialData = null;

    string playerName = "시온";   //나중에 인풋필드로 입력받은 값을 넣어준다


    private void Start()
    {
        //상호작용 델리게이트가 실행되면 ShowDialogue함수 호출
        interCtrl.interact_pro = ShowDialogue;
        dialUI.dialUI_pro = ExitDial;
        questManager.questMg = ShowQuest;
    }

    void ShowDialogue(GameObject hitobj)
    {
        //받아온 오브젝트의 npc넘버를 가져와 npcNum에 넣는다
        int npcNum = hitobj.GetComponent<InteractionNpc>().GetNum();
        //퀘스트 매니저에 npc를 설정해주는 함수에 넣어준다
        questManager.SetTalkNpc(npcNum);
        //str값으로 대사를 받아온다
        string dialogue = questManager.GetDialogue().Replace("ㅇㅇ", playerName);
        //Ui에 띄워준다.
        dialUI.OpenDialBar(dialogue);
    }

    void ShowQuest()
    {
        int _questIndex = questManager.GetQuestIndex();
        string _questName = dialData.GetQuest(_questIndex, "questName");
        string _questExplan = dialData.GetQuest(_questIndex, "explan");
        dialUI.OpenQuestBar(_questName, _questExplan);
    }

    void ExitDial()
    {
        interCtrl.isInteract = false;
        questManager.talkdone = true;
        questManager.dialogueNum = 0;
    }
}
