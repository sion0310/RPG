using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcManager : MonoBehaviour
{
    [SerializeField] InteractionNpc[] npcs = null;     //npc����� �����´�

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

        // npc ���� �ϳ� ������ְ�
        InteractionNpc npc = null;
        //������ npc�迭 �߿��� npcNum�� pickNpcNum�� npc�� ������ �ִ´�
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
