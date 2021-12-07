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
    //����Ʈ�� �ִ� npc�� giveNpcNum�� �־��ֱ����� ����
    int giveNpcNum;
    //����Ʈ �ϷḦ �����ϴ� npc�� doneNpcNum�� �־��ֱ����� ����
    int doneNpcNum;

    // npc ���� �ϳ� ������ְ�
    InteractionNpc giveNpc = null;
    InteractionNpc doneNpc = null;

    private void Awake()
    {
        questList = CSVReader.Read("questList");
    }
    private void Start()
    {
        QuestPro();     //���� ���� ������ ���ۺ��� ����Ʈ�� ������ ���߿� ��ġ�� �ű����(���ۺ��� ��������ʾ�?)
    }

    public void QuestPro()
    {
        //giveNpcNum�� ����Ʈ ���� npc��ȣ�� �־��ش�.
        giveNpcNum = (int)questList[questIndex]["giveNpc"];
        doneNpcNum = (int)questList[questIndex]["doneNpc"];

        
        
        //������ npc�迭 �߿��� npcNum�� giveNpcNum�� npc�� ������ �ִ´�
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
        
        //npc ���¸� HaveQuest(1)�� �������ش�.
        giveNpc.NpcStateSetUp(1);
        doneNpc.NpcStateSetUp(0);
        
    }

    public void QuestDone()
    {
        questdone = true;
        doneNpc.NpcStateSetUp(3);
        
    }

    public void ChangeValue()
    {
        doneNpc.NpcStateSetUp(0);
        giveNpc.NpcStateSetUp(0);

        questIndex++;
        QuestPro();

    }

    //���ľ�����, ����Ʈ �Ϸ� ���� ���ο� ����Ʈ�� �ִ� ���� ���� �����(��ũ��Ʈ)




}
