using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcManager : MonoBehaviour
{
    [SerializeField] InteractionNpc[] npcs = null;     //npc����� �����´�

    List<Dictionary<string, object>> questList;        //����Ʈ ����� �����´�

    //���� ���� ����Ʈ�� �ߴ��� ���ߴ��� üũ�ϱ� ����
    //(������ bool������ �Ϸ������� ���߿��� ����Ʈ �Ϸ� ������ ��������)
    [SerializeField] bool questdone = false;

    //����Ʈ �����Ȳ�� �Ǻ��ϱ����� �ε���
    int questIndex = 0;
    //����Ʈ�� ������ npc�� pickNpcNum�� �־��ֱ����� ����
    int pickNpcNum;
    
    private void Awake()
    {
        questList = CSVReader.Read("questList");
    }
    private void Start()
    {
        QuestPro();     //���� ���� ������ ���ۺ��� ����Ʈ�� ������ ���߿� ��ġ�� �ű����
    }

    public void QuestPro()
    {
        //pickNpcNum�� ����Ʈ ���� npc��ȣ�� �־��ش�.
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
        //npc ���¸� HaveQuest(1)�� �������ش�.
        npc.NpcStateSetUp(1);
        
    }




}
