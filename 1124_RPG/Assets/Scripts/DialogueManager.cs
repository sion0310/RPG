using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [Header("Delegate scripts")]
    public InteractionCtrl interCtrl = null;
    public QuestManager questManager = null;
    public DialogueUICtrl dialUI = null;
    public DialogueData dialData = null;

    string playerName = "시온";   //나중에 인풋필드로 입력받은 값을 넣어준다

    DialogueData.Values values;

    int questIndex = 0;
    int dialIndex = 0;

    bool istalking = false;

    private void Start()
    {
        //콜백 함수들
        interCtrl.interact_pro = ShowDialogue;
        dialUI.exit_pro = ExitDial;
        dialUI.accept_pro = AcceptQuest;
        questManager.questMg = ShowQuest;

        //시작할때부터 퀘스트를 줘야함
        values = dialData.GetQuestValues(questIndex);
        questManager.GiveQuestState(values._giveNpcNum,values._doneNpcNum);
    }
    private void Update()
    {
        values = dialData.GetQuestValues(questIndex);
    }

    void ShowDialogue(GameObject hitobj)
    {
        //받아온 오브젝트의 npc넘버를 가져와 npcNum에 넣는다
        int npcNum = hitobj.GetComponent<NpcInfor>().GetNum();
        //퀘스트 매니저에 npc를 설정해주는 함수에 넣어준다
        questManager.SetTalkNpc(npcNum);

        //차후 수정: header는 퀘스트에 따라 다르게 가져오도록한다
        string header = values._giveQ;

        string dialogue = GetDialogue(npcNum, header).Replace("ㅇㅇ", playerName);
        //Ui에 띄워준다.
        dialUI.OpenDialBar(dialogue);
        
    }
    
    public string GetDialogue(int _npcNum,string _header)
    {
        //npc[npcNum]가 가진 대본중 dialogueNum번째 대사를 가져온다
        string _dialogue = dialData.GetDialogue(_npcNum, dialIndex, _header);
        if (_dialogue == "end")
        {
            _dialogue= dialData.GetDialogue(_npcNum, dialIndex-1, _header);
            dialUI.OpenQuestBar(values._questName, values._questExplan);
            return _dialogue;
        }
        dialIndex++;
        //가져온 대사 반환
        return _dialogue;
    }

    void ShowQuest()
    {
        dialUI.OpenQuestBar(values._questName, values._questExplan);
    }

    void ExitDial()
    {
        interCtrl.isTalking = false;
        dialIndex = 0;
    }

    void AcceptQuest()
    {
        interCtrl.isTalking = false;
        dialIndex = 0;
        questManager.AcceptQuest();
        
    }
}
