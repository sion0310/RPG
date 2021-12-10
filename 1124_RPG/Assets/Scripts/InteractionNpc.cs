using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionNpc : MonoBehaviour
{
    [SerializeField] DialogueData dialData = null;

    [SerializeField] int npcNum;
    

    public GameObject[] icons;

    public enum NpcState { normal, haveQuest, doingQuest, doneQuest }
    [SerializeField] public NpcState npcState = NpcState.normal;

    [SerializeField]
    int dialogueNum = 0;    //���° �� ��� 


    //npc���¿� ���¿� �´� �������� �Ѱ� ���� �Լ�
    public void NpcStateSetUp(NpcState _npcState)
    {
        //npc���¸� �Ű����� ������ �ٲ��ش�.
        npcState = _npcState;
        //��� �������� ����
        icons[0].SetActive(false);
        icons[1].SetActive(false);
        icons[2].SetActive(false);
        icons[3].SetActive(false);
        //���¿� �´� �����ܸ� ���ش�.
        icons[(int)_npcState].SetActive(true);
    }

    //���õ� npc������Ʈ�� �ѹ��� �Ѱ��ִ� �Լ�
    public int GetNum()
    {
        //npcNum�� ��ȯ�Ѵ�
        return npcNum;
    }

    public Dictionary<string, object> GetDialogue()
    {
        //npc[npcNum]�� ���� �뺻�� dialogueNum��° ��縦 �����´�
        Dictionary<string, object> dialogue =
            dialData.GetDialogue(npcNum, dialogueNum);
        //������ ������ ������ ����
        ChangeValue(dialogue);
        //������ ��� ��ȯ
        return dialogue;
    }

    // ��ȭ�� ������ ó���ϴ� �Լ�
    public void ChangeValue(Dictionary<string, object> dialogue)
    {
        //�������� �Ѿ��(++)
        dialogueNum++;
        //���� ��簡 ������
        if (dialogue == null)
        {
            //��� �ѹ��� �ʱ�ȭ ���ְ�
            dialogueNum = 0;
            //���� �뺻���� �Ѿ��
        }
    }

    //��ȭ�� ���������� üũ�ϴ� �Լ�
    public bool CheckTalkDone()
    {
        //���� �������� ����� ���� �� ������ ������ ���� ��ȯ(��ȭ�� ������)
        return dialogueNum >= dialData.GetDialCount(npcNum);
    }
}
