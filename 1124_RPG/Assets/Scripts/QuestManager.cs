using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    //���� ���� ����Ʈ�� �ߴ��� ���ߴ��� üũ�ϱ� ����
    //(������ bool������ �Ϸ������� ���߿��� ����Ʈ �Ϸ� ������ ��������)
    [SerializeField] bool questdone = false;

    //����Ʈ �߻� �ݹ�
    public delegate void QuestMg();
    public QuestMg questMg = null;

    [Header("Npc List")]
    [SerializeField] NpcInfor[] npcs = null;     //npc����� �����´�

    int questIndex = 0;                 //����Ʈ �����Ȳ�� �Ǻ��ϱ����� �ε���
    
    string giveQ = null;                //����Ʈ�� �ִ� �뺻 ��� ����
    string doneQ = null;                //����Ʈ�� �Ϸ��ϴ� �뺻 ��� ����

    int giveNpcNum = 0;                 //����Ʈ�� �ִ� npc�ѹ� ����
    int doneNpcNum = 0;                 //����Ʈ �ϷḦ �����ϴ� npc�ѹ� ����

    NpcInfor giveNpc = null;            //npc�ѹ��� �´� npc���� �־��� ����       
    NpcInfor doneNpc = null;

    NpcInfor talkingNpc = null;         // ���� ��ȭ���� Npc

    public bool talkdone;               //��ȭ ���� Ȯ��

    public int dialogueNum = 0;         //���° �� ��� 

    DialogueData.Values values;
    
    //����Ʈ�� �ִ� �Լ�
    public void GiveQuestState(int _giveNpcNum,int _doneNpcNum)
    {
        //csv���Ͽ��� ����Ʈ�� �ִ¾ֿ� ������ �� ��ȣ�� �����´�.

        giveNpcNum = _giveNpcNum;
        
        //---------------------------------
        doneNpcNum = _doneNpcNum;

        giveQ = values._giveQ;
        doneQ = values._doneQ;
        
        
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
        GiveQuestState(values._giveNpcNum,values._doneNpcNum);

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
        //Dictionary<string, object> dial = GetQuestDialogue();
        if (talkingNpc.npcState == NpcInfor.NpcState.haveQuest)
            header = values._giveQ;
        if (talkingNpc.npcState == NpcInfor.NpcState.doneQuest)
            header = values._doneQ;
        if (talkingNpc.npcState == NpcInfor.NpcState.normal)
            header = "normal";
        if (talkingNpc.npcState == NpcInfor.NpcState.doingQuest)
            header = "doing";
        //if (dial[header].ToString() == "end") 
        {
            dialogueNum--;
            talkdone = true;
            if (talkingNpc.npcState == NpcInfor.NpcState.haveQuest)
            {
               
            }
            if(talkingNpc.npcState == NpcInfor.NpcState.doneQuest)
            {
                ChangeValue();
            }
            dialogueNum = 0;

        }
        if (!talkdone)
        {
            dialogueNum++;
        }

        return header;
    }

    public bool CheckTalkDone()
    {
        return talkdone;
    }

   


    public int GetQuestIndex()
    {
        return questIndex;
    }

}
