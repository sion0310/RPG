using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    //차후 수정 퀘스트를 했는지 안했는지 체크하기 위함
    //(지금은 bool값으로 완료하지만 나중에는 퀘스트 완료 조건을 만들어야함)
    [SerializeField] bool questdone = false;

    //퀘스트 발생 콜백
    public delegate void QuestMg();
    public QuestMg questMg = null;

    [Header("Npc List")]
    [SerializeField] NpcInfor[] npcs = null;     //npc목록을 가져온다

    int questIndex = 0;                 //퀘스트 진행상황을 판별하기위한 인덱스
    
    string giveQ = null;                //퀘스트를 주는 대본 헤더 변수
    string doneQ = null;                //퀘스트를 완료하는 대본 헤더 변수

    int giveNpcNum = 0;                 //퀘스트를 주는 npc넘버 변수
    int doneNpcNum = 0;                 //퀘스트 완료를 진행하는 npc넘버 변수

    NpcInfor giveNpc = null;            //npc넘버와 맞는 npc들을 넣어줄 변수       
    NpcInfor doneNpc = null;

    NpcInfor talkingNpc = null;         // 현재 대화중인 Npc

    public bool talkdone;               //대화 종료 확인

    public int dialogueNum = 0;         //몇번째 줄 대사 

    DialogueData.Values values;
    
    //퀘스트를 주는 함수
    public void GiveQuestState(int _giveNpcNum,int _doneNpcNum)
    {
        //csv파일에서 퀘스트를 주는애와 끝내는 애 번호를 가져온다.

        giveNpcNum = _giveNpcNum;
        
        //---------------------------------
        doneNpcNum = _doneNpcNum;

        giveQ = values._giveQ;
        doneQ = values._doneQ;
        
        
        //가져온 번호에 맞는 애들을 npc배열에서 찾아 넣어준다.
        foreach (NpcInfor it in npcs)
        {
            if (it.GetNum() == giveNpcNum)
            {
                giveNpc = it;
                continue;
            }
        }
        foreach (NpcInfor it in npcs)
        {
            if (it.GetNum() == doneNpcNum)
            {
                doneNpc = it;
                break;
            }
        }

        //퀘스트를 주는 npc 상태를 HaveQuest(1)로 변경해준다.
        giveNpc.NpcStateSetUp(NpcInfor.NpcState.haveQuest);
    }

    
    public void AcceptQuest()
    {
        giveNpc.NpcStateSetUp(NpcInfor.NpcState.normal);
        doneNpc.NpcStateSetUp(NpcInfor.NpcState.doingQuest);
    }

    //퀘스트 조건 달성
    public void AchievedQuest()
    {
        questdone = true;
        doneNpc.NpcStateSetUp(NpcInfor.NpcState.doneQuest);

    }

    public void ChangeValue()
    {
        if (doneNpc.npcState != NpcInfor.NpcState.doneQuest) return;

        doneNpc.NpcStateSetUp(NpcInfor.NpcState.normal);
        giveNpc.NpcStateSetUp(NpcInfor.NpcState.normal);

        talkingNpc = null;

        questIndex++;
        GiveQuestState(values._giveNpcNum,values._doneNpcNum);

    }


    public void SetTalkNpc(int _npcNum)
    {
        // npc 변수 하나 만들어주고
        NpcInfor npc = null;
        //가져온 npc배열 중에서 클릭한 npcNum을 가진 npc를 변수에 넣는다
        foreach (NpcInfor it in npcs)
        {
            if (it.GetNum() == _npcNum)
            {
                npc = it;
                break;
            }
        }

        // talkingNpc가 비었으면 npc를 넣어준다.
        if (talkingNpc == null) talkingNpc = npc;
        // 안 비었을때엔, 이전 대화가 끝났을때만 npc를 넣어준다.
        if (talkingNpc != null && talkdone) talkingNpc = npc;
    }

    public string GetDialogue()
    {
        string header = null;
        talkdone = false;
        //Dictionary<string, object> dial = GetQuestDialogue();
        if (talkingNpc.npcState == NpcInfor.NpcState.haveQuest)
            header = values._giveQ;
        if (talkingNpc.npcState == NpcInfor.NpcState.doneQuest)
            header = values._doneQ;
        if (talkingNpc.npcState == NpcInfor.NpcState.normal)
            header = "normal";
        if (talkingNpc.npcState == NpcInfor.NpcState.doingQuest)
            header = "doing";
        //if (dial[header].ToString() == "end") 
        {
            dialogueNum--;
            talkdone = true;
            if (talkingNpc.npcState == NpcInfor.NpcState.haveQuest)
            {
               
            }
            if(talkingNpc.npcState == NpcInfor.NpcState.doneQuest)
            {
                ChangeValue();
            }
            dialogueNum = 0;

        }
        if (!talkdone)
        {
            dialogueNum++;
        }

        return header;
    }

    public bool CheckTalkDone()
    {
        return talkdone;
    }

   


    public int GetQuestIndex()
    {
        return questIndex;
    }

}
