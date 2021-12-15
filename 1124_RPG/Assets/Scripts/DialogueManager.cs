using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [Header("Delegate scripts")]
    public InteractionCtrl interCtrl = null;
    public DialogueUICtrl dialUI = null;
    public QuestPro questPro = null; 
    
    [Header("scripts")]
    public DialogueData dialData = null;
    public NpcManager npcMg = null;

    string playerName = "시온";   //나중에 인풋필드로 입력받은 값을 넣어준다

    //이거 이렇게 쓰는거 맞는지 선생님께 물어보기
    DialogueData.Values values;

    int questIndex = 0;
    int dialIndex = 0;

    public bool questDone;

    private void Start()
    {
        //콜백 함수들
        interCtrl.interact_pro = ShowDialogue;
        dialUI.exit_pro = ExitDial;
        dialUI.accept_pro = AcceptQuest;
        questPro.questAchieve = AchievedQuest;

        //시작부터 퀘스트 넣어주기
        SetQuestInfo();
    }
    void AchievedQuest(bool isAchieved)
    {
        if (isAchieved)
        {
            npcMg.AchievedQuest();
        }
    }

    //퀘스트가 시작될때 마다 바꿔주는것들
    void SetQuestInfo()
    {
        //퀘스트 순서에 따라 가져오는 퀘스트 데이터
        values = dialData.GetQuestValues(questIndex);
        //퀘스트 순서에 따라 바꿔주는 npc변수
        npcMg.SetNpcStateInfo(values._giveNpcNum, values._doneNpcNum);
    }

    //대화창이 열릴때 호출되는 함수
    void ShowDialogue(GameObject hitobj)
    {
        //받아온 오브젝트의 npc넘버를 가져와 npcNum에 넣는다
        int npcNum = hitobj.GetComponent<NpcInfo>().GetNum();
        //퀘스트 매니저에 대화중 npc를 설정해주는 함수에 넣어준다
        npcMg.SetTalkNpc(npcNum, interCtrl.isTalking);

        //header는 npc상태에 따라 다르게 가져온다
        string header = npcMg.GetHeader(values._giveQ, values._doneQ);
        //가져온 정보들을 넣고 대사를 가져온다
        string dialogue = GetDialogue(npcNum, header).Replace("ㅇㅇ", playerName);
        //Ui에 띄워준다.
        dialUI.OpenDialBar(dialogue);
        
    }
    
    public string GetDialogue(int _npcNum,string _header)
    {
        //_npcNum번째 스크립트의 _header에서 dialIndex번째 대사를 가져온다.
        string _dialogue = dialData.GetDialogue(_npcNum, dialIndex, _header);
        //만약 대사가 끝났으면
        if (_dialogue == "end")
        {
            //계속 마지막 대사 반환
            _dialogue= dialData.GetDialogue(_npcNum, dialIndex-1, _header);
            //대화를 마친 npc상태가 havequest이면 퀘스트창 오픈
            ShowQuest();

            return _dialogue;
        }
        dialIndex++;
        //가져온 대사 반환
        return _dialogue;
    }

    void ShowQuest()
    {
        if (npcMg.OpenQuestBar())
        {
            if (npcMg.NextQuest()) questDone = true;
            else questDone = false;
            dialUI.OpenQuestBar(values._questName, values._questExplan, questDone);
        }
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
        if (npcMg.NextQuest())
        {
            //완료npc상태 바꿔주고
            npcMg.DoneQuest();
            //다음 퀘스트로 넘어가고
            questIndex++;
            questPro.isAchieved = false;
            //퀘스트 정보들 다시 바꿔준다.
            SetQuestInfo();
        }
        else
        {
            npcMg.AcceptQuest();
            if (values._condition == "none")
            {
                questPro.isAchieved = true;
            }
        }
    }
}
