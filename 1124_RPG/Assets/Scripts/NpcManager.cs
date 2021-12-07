using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcManager : MonoBehaviour
{
    [SerializeField] InteractionNpc[] npcs = null;     //npc목록을 가져온다

    List<Dictionary<string, object>> questList;
    
    private void Awake()
    {
        questList = CSVReader.Read("questList");
    }
    private void Update()
    {
        QuestPro();
    }

    public void QuestPro()
    {
        int questIndex = 0;
        int pickNpcNum;
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

        npc.npcState = InteractionNpc.NpcState.haveQuest;
        
    }




}
