using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    [SerializeField] InteractionNpc[] npcs = null;     //npc����� �����´�

    List<Dictionary<string, object>> questList;        //����Ʈ ����� �����´�
    [SerializeField] DialogueData dialData = null;

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
    InteractionNpc giveNpc = null;
    InteractionNpc doneNpc = null;

    InteractionNpc talkingNpc = null;   // ���� ��ȭ���� NPC
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

        //����Ʈ�� �ִ� npc ���¸� HaveQuest(1)�� �������ش�.
        giveNpc.NpcStateSetUp(InteractionNpc.NpcState.haveQuest);
    }

    //����Ʈ�� �ִ� ��ȭ ������ �Լ�
    public void AfterGiveTalk()
    {
        questMg?.Invoke();
        giveNpc.NpcStateSetUp(InteractionNpc.NpcState.normal);
        doneNpc.NpcStateSetUp(InteractionNpc.NpcState.doingQuest);

    }

    //����Ʈ ���� �޼�
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
                AfterGiveTalk();
            }
            if(talkingNpc.npcState == InteractionNpc.NpcState.doneQuest)
            {
                ChangeValue();
            }
            
            talkdone = true;
            dialogueNum = 0;

        }
        if (!talkdone)
        {
            dialogueNum++;


            ?
        }
        return dial[header].ToString();
    }

    public bool CheckTalkDone()
    {
        return talkdone;
    }

    public Dictionary<string, object> GetQuestDialogue()
    {
        //npc[npcNum]�� ���� �뺻�� dialogueNum��° ��縦 �����´�
        Dictionary<string, object> dialogue =
            dialData.GetDialogue(talkingNpc.GetNum(), dialogueNum);
        //������ ��� ��ȯ
        return dialogue;
    }


    public int GetQuestIndex()
    {
        return questIndex;
    }

}
