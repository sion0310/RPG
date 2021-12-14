using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    [SerializeField] NpcInfor[] npcs = null;     //npc����� �����´�

    List<Dictionary<string, object>> questList;        //����Ʈ ����� �����´�
    [SerializeField] DialogueData dialData = null;

    //����Ʈ �߻� �ݹ�
    public delegate void QuestMg();
    public QuestMg questMg = null;

    //����Ʈ �����Ȳ�� �Ǻ��ϱ����� �ε���
    int questIndex = 0;
    //����Ʈ�� �ִ� npc�� giveNpcNum�� �־��ֱ����� ����
    int giveNpcNum = 0;
    //����Ʈ �ϷḦ �����ϴ� npc�� doneNpcNum�� �־��ֱ����� ����
    int doneNpcNum = 0;

    //����Ʈ�� �´� �뺻 ����� �������� ���� ����
    string giveQ = null;
    string doneQ = null;

    // npc ���� �ϳ� ������ְ�
    NpcInfor giveNpc = null;
    NpcInfor doneNpc = null;

    NpcInfor talkingNpc = null;   // ���� ��ȭ���� NPC
    public bool talkdone;

    //���� ���� ����Ʈ�� �ߴ��� ���ߴ��� üũ�ϱ� ����
    //(������ bool������ �Ϸ������� ���߿��� ����Ʈ �Ϸ� ������ ��������)
    [SerializeField] bool questdone = false;

    [SerializeField]
    public int dialogueNum = 0;    //���° �� ��� 

    private void Awake()
    {
        questList = CSVReader.Read("questList");
    }
    private void Start()
    {
        //�����Ҷ����� ù ����Ʈ�� �����ϹǷ� �켱 �ѹ� �������ش�.
        GiveQuest();
    }

    //����Ʈ�� �ִ� �Լ�
    public void GiveQuest()
    {
        //csv���Ͽ��� ����Ʈ�� �ִ¾ֿ� ������ �� ��ȣ�� �����´�.
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

        //������ ��ȣ�� �´� �ֵ��� npc�迭���� ã�� �־��ش�.
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

        //����Ʈ�� �ִ� npc ���¸� HaveQuest(1)�� �������ش�.
        giveNpc.NpcStateSetUp(NpcInfor.NpcState.haveQuest);
    }

    //����Ʈ�� �ִ� ��ȭ ������ �Լ�
    public void AfterGiveTalk()
    {
        questMg?.Invoke();

    }
    public void AcceptQuest()
    {
        giveNpc.NpcStateSetUp(NpcInfor.NpcState.normal);
        doneNpc.NpcStateSetUp(NpcInfor.NpcState.doingQuest);
    }

    //����Ʈ ���� �޼�
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
        GiveQuest();

    }


    public void SetTalkNpc(int _npcNum)
    {
        // npc ���� �ϳ� ������ְ�
        NpcInfor npc = null;
        //������ npc�迭 �߿��� Ŭ���� npcNum�� ���� npc�� ������ �ִ´�
        foreach (NpcInfor it in npcs)
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
        if (talkingNpc != null && talkdone) talkingNpc = npc;
    }

    public string GetDialogue()
    {
        string header = null;
        talkdone = false;
        string dial = GetQuestDialogue();
        if (talkingNpc.npcState == NpcInfor.NpcState.haveQuest)
            header = questList[questIndex]["giveQ"].ToString();
        if (talkingNpc.npcState == NpcInfor.NpcState.doneQuest)
            header = questList[questIndex]["doneQ"].ToString();
        if (talkingNpc.npcState == NpcInfor.NpcState.normal)
            header = "normal";
        if (talkingNpc.npcState == NpcInfor.NpcState.doingQuest)
            header = "doing";
        if (dial== "end")
        {
            dialogueNum--;
            dial = GetQuestDialogue();
            talkdone = true;
            if (talkingNpc.npcState == NpcInfor.NpcState.haveQuest)
            {
                AfterGiveTalk();
            }
            if (talkingNpc.npcState == NpcInfor.NpcState.doneQuest)
            {
                ChangeValue();
            }
            dialogueNum = 0;

        }
        if (!talkdone)
        {
            dialogueNum++;
        }

        return dial;
    }

    public bool CheckTalkDone()
    {
        return talkdone;
    }

    public string GetQuestDialogue()
    {
        //npc[npcNum]�� ���� �뺻�� dialogueNum��° ��縦 �����´�
        string dialogue =
            dialData.GetDialogue(talkingNpc.GetNum(), dialogueNum,"give1");
        //������ ��� ��ȯ
        return dialogue;
    }


    public int GetQuestIndex()
    {
        return questIndex;
    }

}
