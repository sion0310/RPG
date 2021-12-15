using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcManager : MonoBehaviour
{
    [Header("Npc List")]
    [SerializeField] NpcInfo[] npcs = null;     //npc����� �����´�


    NpcInfo giveNpc = null;            //����Ʈ�� �����ִ� npc���� ���� ������ ���� ������ �����д�.       
    NpcInfo doneNpc = null;             

    NpcInfo talkingNpc = null;         // ���� ��ȭ���� Npc�� �־��� ����


    //��ȭ���� npc�� ������ �־��ش�.���ο� ��ȭ�� ���۵ɶ����� �Լ� ȣ��
    public void SetTalkNpc(int _npcNum, bool talkdone)
    {
        // npc ���� �ϳ� ������ְ�
        NpcInfo npc = null;
        //������ npc�迭 �߿��� Ŭ���� npcNum�� ���� npc�� ������ �ִ´�
        foreach (NpcInfo it in npcs)
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


    //���°� �ٲ�� npc�� ����Ʈ�� �ִ�npc�� ����Ʈ�� �Ϸ��Ű�� npc�̹Ƿ� �� npc���� ���� ������ �־��ش�
    //���� �Լ��� ������ ���۵Ǿ�������, ���� ����Ʈ�� �Ϸ��ϸ� ȣ��ȴ�.
    public void SetNpcStateInfo(int _giveNpcNum,int _doneNpcNum)
    {
        //�Լ��� ����ɶ� ����Ʈ�� ���õ� npc���� ��ȣ�� �޾ƿͼ�
        //��ȣ�� �´� �ֵ��� npc�迭���� ã�� �־��ش�
        foreach (NpcInfo it in npcs)
        {
            if (it.GetNum() == _giveNpcNum)
            {
                giveNpc = it;
                continue;
            }
        }
        foreach (NpcInfo it in npcs)
        {
            if (it.GetNum() == _doneNpcNum)
            {
                doneNpc = it;
                break;
            }
        }

        //giveNpc ���¸� HaveQuest�� �������ش�.
        giveNpc.NpcStateSetUp(NpcInfo.NpcState.haveQuest);
    }


    //����Ʈ ������ �������� npc���� ��ȭ
    public void AcceptQuest()
    {
        giveNpc.NpcStateSetUp(NpcInfo.NpcState.normal);
        doneNpc.NpcStateSetUp(NpcInfo.NpcState.doingQuest);
    }


    //����Ʈ ���� �޼� ������ npc���� ��ȭ
    public void AchievedQuest()
    {
        doneNpc.NpcStateSetUp(NpcInfo.NpcState.doneQuest);
    }

    //����Ʈ �Ϸ� npc���� ��ȭ
    public void DoneQuest()
    {
        doneNpc.NpcStateSetUp(NpcInfo.NpcState.normal);
    }

    //��ȭ���� npc�� ���¿� ���� �ٸ� header�� ��������.
    public string GetHeader(string _giveQ, string _doneQ)
    {
        string header = null;
        if (talkingNpc.npcState == NpcInfo.NpcState.haveQuest)
            header = _giveQ;
        if (talkingNpc.npcState == NpcInfo.NpcState.doneQuest)
            header = _doneQ;
        if (talkingNpc.npcState == NpcInfo.NpcState.normal)
            header = "normal";
        if (talkingNpc.npcState == NpcInfo.NpcState.doingQuest)
            header = "doing";

        return header;
    }

    public bool OpenQuestBar()
    {
        bool open;
        if (talkingNpc.npcState == NpcInfo.NpcState.haveQuest|| talkingNpc.npcState == NpcInfo.NpcState.doneQuest)
            open = true;
        else
            open = false;
        return open;
    }

    public bool NextQuest()
    {
        bool goNext;
        if (talkingNpc.npcState == NpcInfo.NpcState.doneQuest)
            goNext = true;
        else
            goNext = false;
        return goNext;
    }
}
