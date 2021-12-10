using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] InteractionNpc[] npcs = null;     //npc목록을 가져온다

    List<Dictionary<string, object>> questList;        //퀘스트 목록을 가져온다

    //차후 수정 퀘스트를 했는지 안했는지 체크하기 위함
    //(지금은 bool값으로 완료하지만 나중에는 퀘스트 완료 조건을 만들어야함)
    [SerializeField] bool questdone = false;

    //퀘스트 진행상황을 판별하기위한 인덱스
    int questIndex = 0;
    //퀘스트를 주는 npc를 giveNpcNum로 넣어주기위한 변수
    int giveNpcNum;
    //퀘스트 완료를 진행하는 npc를 doneNpcNum로 넣어주기위한 변수
    int doneNpcNum;

    //퀘스트에 맞는 대본 헤더를 가져오기 위한 변수
    string giveQ;
    string doneQ;
    string header;

    // npc 변수 하나 만들어주고
    InteractionNpc giveNpc = null;
    InteractionNpc doneNpc = null;

    InteractionNpc talkingNpc = null;   // 현재 대화중인 NPC

    private void Awake()
    {
        questList = CSVReader.Read("questList");
    }
    private void Start()
    {
        QuestPro();     
    }

    public void QuestPro()
    {
        //giveNpcNum에 퀘스트 지정 npc번호를 넣어준다.
        giveNpcNum = (int)questList[questIndex]["giveNpc"];
        doneNpcNum = (int)questList[questIndex]["doneNpc"];
        
        //가져온 npc배열 중에서 npcNum가 giveNpcNum인 npc를 변수에 넣는다
        foreach (InteractionNpc it in npcs)
        {
            if (it.GetNum() == giveNpcNum)
            {
                giveNpc = it;
                continue;
            }
        }
        foreach (InteractionNpc it in npcs)
        {
            if (it.GetNum() == doneNpcNum)
            {
                doneNpc = it;
                break;
            }


        }

        //npc 상태를 HaveQuest(1)로 변경해준다.
        giveNpc.NpcStateSetUp(InteractionNpc.NpcState.haveQuest);
        header = questList[questIndex]["giveQ"].ToString();
    }

    public void AfterGiveTalk()
    {
        giveNpc.NpcStateSetUp(InteractionNpc.NpcState.normal);
        doneNpc.NpcStateSetUp(InteractionNpc.NpcState.doingQuest);
    }


    public void QuestDone()
    {
        questdone = true;
        doneNpc.NpcStateSetUp(InteractionNpc.NpcState.doneQuest);
        header = questList[questIndex]["doneQ"].ToString();

    }

    public void ChangeValue()
    {
        if (doneNpc.npcState != InteractionNpc.NpcState.doneQuest) return;

        doneNpc.NpcStateSetUp(InteractionNpc.NpcState.normal);
        giveNpc.NpcStateSetUp(InteractionNpc.NpcState.normal);

        talkingNpc = null;

        questIndex++;
        QuestPro();

    }

    //고쳐야할점, 퀘스트 완료 대사와 새로운 퀘스트를 주는 대사는 따로 만든다(스크립트)
    

    public void SetTalkNpc(int _npcNum)
    {
        // npc 변수 하나 만들어주고
        InteractionNpc npc = null;
        //가져온 npc배열 중에서 클릭한 npcNum을 가진 npc를 변수에 넣는다
        foreach (InteractionNpc it in npcs)
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
        if (talkingNpc != null && talkingNpc.CheckTalkDone()) talkingNpc = npc;
    }

    public string GetDialogue()
    {
        Dictionary<string, object> dial = talkingNpc.GetDialogue();
        return dial[header].ToString();
    }

    public bool CheckTalkDone()
    {
        return talkingNpc.CheckTalkDone();
    }

}
