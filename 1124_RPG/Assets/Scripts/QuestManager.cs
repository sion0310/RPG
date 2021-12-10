using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
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

    //����Ʈ�� �´� �뺻 ����� �������� ���� ����
    string giveQ;
    string doneQ;
    string header;

    // npc ���� �ϳ� ������ְ�
    InteractionNpc giveNpc = null;
    InteractionNpc doneNpc = null;

    InteractionNpc talkingNpc = null;   // ���� ��ȭ���� NPC

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

    //���ľ�����, ����Ʈ �Ϸ� ���� ���ο� ����Ʈ�� �ִ� ���� ���� �����(��ũ��Ʈ)
    

    public void SetTalkNpc(int _npcNum)
    {
        // npc ���� �ϳ� ������ְ�
        InteractionNpc npc = null;
        //������ npc�迭 �߿��� Ŭ���� npcNum�� ���� npc�� ������ �ִ´�
        foreach (InteractionNpc it in npcs)
        {
            if (it.GetNum() == _npcNum)
            {
                npc = it;
                break;
            }
        }

        // talkingNpc�� ������� npc�� �־��ش�.
        if (talkingNpc == null) talkingNpc = npc;
        // �� ���������, ���� ��ȭ�� ���������� npc�� �־��ش�.
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
