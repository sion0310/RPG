using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcManager : MonoBehaviour
{
    [Header("Npc List")]
    [SerializeField] NpcInfo[] npcs = null;     //npc목록을 가져온다


    NpcInfo giveNpc = null;            //퀘스트와 연관있는 npc들의 상태 변경을 위해 변수를 만들어둔다.       
    NpcInfo doneNpc = null;             

    NpcInfo talkingNpc = null;         // 현재 대화중인 Npc를 넣어줄 변수


    //대화중인 npc를 변수에 넣어준다.새로운 대화가 시작될때마다 함수 호출
    public void SetTalkNpc(int _npcNum, bool talkdone)
    {
        // npc 변수 하나 만들어주고
        NpcInfo npc = null;
        //가져온 npc배열 중에서 클릭한 npcNum을 가진 npc를 변수에 넣는다
        foreach (NpcInfo it in npcs)
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


    //상태가 바뀌는 npc는 퀘스트를 주는npc와 퀘스트를 완료시키는 npc이므로 그 npc들을 각각 변수에 넣어준다
    //다음 함수는 게임이 시작되었을때와, 이전 퀘스트를 완료하면 호출된다.
    public void SetNpcStateInfo(int _giveNpcNum,int _doneNpcNum)
    {
        //함수가 실행될때 퀘스트와 관련된 npc들의 번호를 받아와서
        //번호와 맞는 애들을 npc배열에서 찾아 넣어준다
        foreach (NpcInfo it in npcs)
        {
            if (it.GetNum() == _giveNpcNum)
            {
                giveNpc = it;
                continue;
            }
        }
        foreach (NpcInfo it in npcs)
        {
            if (it.GetNum() == _doneNpcNum)
            {
                doneNpc = it;
                break;
            }
        }

        //giveNpc 상태를 HaveQuest로 변경해준다.
        giveNpc.NpcStateSetUp(NpcInfo.NpcState.haveQuest);
    }


    //퀘스트 수락을 눌렀을때 npc상태 변화
    public void AcceptQuest()
    {
        giveNpc.NpcStateSetUp(NpcInfo.NpcState.normal);
        doneNpc.NpcStateSetUp(NpcInfo.NpcState.doingQuest);
    }


    //퀘스트 조건 달성 했을때 npc상태 변화
    public void AchievedQuest()
    {
        doneNpc.NpcStateSetUp(NpcInfo.NpcState.doneQuest);
    }

    //퀘스트 완료 npc상태 변화
    public void DoneQuest()
    {
        doneNpc.NpcStateSetUp(NpcInfo.NpcState.normal);
    }

    //대화중인 npc의 상태에 따라 다른 header를 내보낸다.
    public string GetHeader(string _giveQ, string _doneQ)
    {
        string header = null;
        if (talkingNpc.npcState == NpcInfo.NpcState.haveQuest)
            header = _giveQ;
        if (talkingNpc.npcState == NpcInfo.NpcState.doneQuest)
            header = _doneQ;
        if (talkingNpc.npcState == NpcInfo.NpcState.normal)
            header = "normal";
        if (talkingNpc.npcState == NpcInfo.NpcState.doingQuest)
            header = "doing";

        return header;
    }

    public bool OpenQuestBar()
    {
        bool open;
        if (talkingNpc.npcState == NpcInfo.NpcState.haveQuest|| talkingNpc.npcState == NpcInfo.NpcState.doneQuest)
            open = true;
        else
            open = false;
        return open;
    }

    public bool NextQuest()
    {
        bool goNext;
        if (talkingNpc.npcState == NpcInfo.NpcState.doneQuest)
            goNext = true;
        else
            goNext = false;
        return goNext;
    }
}
