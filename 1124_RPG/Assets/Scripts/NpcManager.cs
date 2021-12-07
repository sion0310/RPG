using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcManager : MonoBehaviour
{
    [SerializeField] InteractionNpc[] npcs = null;     //npc목록을 가져온다

    List<Dictionary<string, object>> questList;        //퀘스트 목록을 가져온다

    //차후 수정 퀘스트를 했는지 안했는지 체크하기 위함
    //(지금은 bool값으로 완료하지만 나중에는 퀘스트 완료 조건을 만들어야함)
    [SerializeField] bool questdone = false;

    //퀘스트 진행상황을 판별하기위한 인덱스
    int questIndex = 0;
    //퀘스트에 지정된 npc를 pickNpcNum로 넣어주기위한 변수
    int pickNpcNum;
    
    private void Awake()
    {
        questList = CSVReader.Read("questList");
    }
    private void Start()
    {
        QuestPro();     //차후 수정 지금은 시작부터 퀘스트를 넣지만 나중에 위치를 옮길것임
    }

    public void QuestPro()
    {
        //pickNpcNum에 퀘스트 지정 npc번호를 넣어준다.
        pickNpcNum = (int)questList[questIndex]["npc"];

        // npc 변수 하나 만들어주고
        InteractionNpc npc = null;
        
        //가져온 npc배열 중에서 npcNum가 pickNpcNum인 npc를 변수에 넣는다
        foreach (InteractionNpc it in npcs)
        {
            if (it.GetNum() == pickNpcNum)
            {
                npc = it;
                break;
            }
        }
        //npc 상태를 HaveQuest(1)로 변경해준다.
        npc.NpcStateSetUp(1);
        
    }




}
