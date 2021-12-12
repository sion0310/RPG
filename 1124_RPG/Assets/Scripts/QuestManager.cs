using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    [SerializeField] InteractionNpc[] npcs = null;     //npc목록을 가져온다

    List<Dictionary<string, object>> questList;        //퀘스트 목록을 가져온다
    [SerializeField] DialogueData dialData = null;

    //------------------------------------------------
    public GameObject questUI;
    public Text questName;
    public Text questExplan;
    //------------------------------------------------------


    //퀘스트 진행상황을 판별하기위한 인덱스
    int questIndex = 0;
    //퀘스트를 주는 npc를 giveNpcNum로 넣어주기위한 변수
    int giveNpcNum = 0;
    //퀘스트 완료를 진행하는 npc를 doneNpcNum로 넣어주기위한 변수
    int doneNpcNum = 0;

    //퀘스트에 맞는 대본 헤더를 가져오기 위한 변수
    string giveQ = null;
    string doneQ = null;
    
    // npc 변수 하나 만들어주고
    InteractionNpc giveNpc = null;
    InteractionNpc doneNpc = null;

    InteractionNpc talkingNpc = null;   // 현재 대화중인 NPC
    bool talkdone;

    //차후 수정 퀘스트를 했는지 안했는지 체크하기 위함
    //(지금은 bool값으로 완료하지만 나중에는 퀘스트 완료 조건을 만들어야함)
    [SerializeField] bool questdone = false;

    [SerializeField]
    public int dialogueNum = 0;    //몇번째 줄 대사 

    private void Awake()
    {
        questList = CSVReader.Read("questList");
    }
    private void Start()
    {
        //시작할때부터 첫 퀘스트가 들어가야하므로 우선 한번 실행해준다.
        GiveQuest();     
    }

    //퀘스트를 주는 함수
    public void GiveQuest()
    {
        //csv파일에서 퀘스트를 주는애와 끝내는 애 번호를 가져온다.
        if (questList[questIndex]["doneNpc"].ToString() == "key")
        {
            doneNpc = null;
            doneQ = null;
        }
        else
        {

            doneNpcNum = (int)questList[questIndex]["doneNpc"];
            giveNpcNum = (int)questList[questIndex]["giveNpc"];

            giveQ = questList[questIndex]["giveQ"].ToString();
            doneQ = questList[questIndex]["doneQ"].ToString();
            
        }
        
        //가져온 번호에 맞는 애들을 npc배열에서 찾아 넣어준다.
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

        //퀘스트를 주는 npc 상태를 HaveQuest(1)로 변경해준다.
        giveNpc.NpcStateSetUp(InteractionNpc.NpcState.haveQuest);
    }

    //퀘스트를 주는 대화 이후의 함수
    public void AfterGiveTalk()
    {
        //---------------------------------------------------------------------
        questUI.SetActive(true);
        questName.text= questList[questIndex]["questName"].ToString();
        questExplan.text= questList[questIndex]["explan"].ToString();
        //----------------------------------------------------------------------
        giveNpc.NpcStateSetUp(InteractionNpc.NpcState.normal);
        doneNpc.NpcStateSetUp(InteractionNpc.NpcState.doingQuest);

    }

    //퀘스트 조건 달성
    public void AchievedQuest()
    {
        questdone = true;
        doneNpc.NpcStateSetUp(InteractionNpc.NpcState.doneQuest);

    }

    public void ChangeValue()
    {
        if (doneNpc.npcState != InteractionNpc.NpcState.doneQuest) return;

        doneNpc.NpcStateSetUp(InteractionNpc.NpcState.normal);
        giveNpc.NpcStateSetUp(InteractionNpc.NpcState.normal);

        talkingNpc = null;

        questIndex++;
        GiveQuest();

    }


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
        if (talkingNpc != null && talkdone) talkingNpc = npc;
    }

    public string GetDialogue()
    {
        string header = null;
        talkdone = false;
        Dictionary<string, object> dial = GetQuestDialogue();
        if (talkingNpc.npcState==InteractionNpc.NpcState.haveQuest)
            header = questList[questIndex]["giveQ"].ToString();
        if (talkingNpc.npcState==InteractionNpc.NpcState.doneQuest)
            header = questList[questIndex]["doneQ"].ToString();
        if (talkingNpc.npcState == InteractionNpc.NpcState.normal)
            header = "normal";
        if (talkingNpc.npcState == InteractionNpc.NpcState.doingQuest)
            header = "doing";
        if (dial[header].ToString() == "end") 
        {
            
            if(talkingNpc.npcState == InteractionNpc.NpcState.haveQuest)
            {
                talkdone = false;
                AfterGiveTalk();
            }
            if(talkingNpc.npcState == InteractionNpc.NpcState.doneQuest)
            {
                ChangeValue();
            }
            else
            {
                talkdone = true;
                dialogueNum = 0;
            }
        }
        return dial[header].ToString();
    }

    public bool CheckTalkDone()
    {
        return talkdone;
    }

    public Dictionary<string, object> GetQuestDialogue()
    {
        //npc[npcNum]가 가진 대본중 dialogueNum번째 대사를 가져온다
        Dictionary<string, object> dialogue =
            dialData.GetDialogue(talkingNpc.GetNum(), dialogueNum);
        dialogueNum++;
        //가져온 대사 반환
        return dialogue;
    }
   

}
