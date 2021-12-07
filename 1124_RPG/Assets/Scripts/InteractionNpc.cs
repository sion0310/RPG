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
    int scriptNum = 0;      //��繶ġ
    int dialogueNum = 0;    //���° �� ��� 

    private void Start()
    {
        //ó���� �����ܰ� ���¸� ��� �븻�� ����
        NpcStateSetUp(0);
    }

    //npc���¿� ���¿� �´� �������� �Ѱ� ���� �Լ�
    public void NpcStateSetUp(int _npcState)
    {
        //npc���¸� �Ű����� ������ �ٲ��ش�.
        npcState = (NpcState)_npcState;
        //��� �������� ����
        icons[0].SetActive(false);
        icons[1].SetActive(false);
        icons[2].SetActive(false);
        icons[3].SetActive(false);
        //���¿� �´� �����ܸ� ���ش�.
        icons[_npcState].SetActive(true);
    }

    //���õ� npc������Ʈ�� �ѹ��� �Ѱ��ִ� �Լ�
    public int GetNum()
    {
        //npcNum�� ��ȯ�Ѵ�
        return npcNum;
    }

    public Dictionary<string, object> GetDialogue()
    {
        //���Ǿ� ���°� ����Ʈ�� ��������̰ų� ����Ʈ�� �Ϸ��� ��� ����Ʈ ��ȭ�� ����
        if (npcState == NpcState.haveQuest||npcState==NpcState.doneQuest)
        {
            //npc[npcNum]�� ���� �뺻�� scriptNum��° �뺻�� dialogueNum��° ��縦 �����´�
            Dictionary<string, object> dialogue =
                dialData.GetDialogue(npcNum, scriptNum, dialogueNum);
            //������ ������ ������ ����
            ChangeValue(dialogue);
            //������ ��� ��ȯ
            return dialogue;
        }
        if (npcState == NpcState.doingQuest || npcState == NpcState.normal)
        {
            return null;
        }
        
        //���� ����(�⺻��ȭ, ����Ʈ�� ��ȭ)
        else return null;
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
            scriptNum++;
            //���� �뺻�� ������ �뺻 �ѹ��� �ʱ�ȭ ���ش�.
            if (scriptNum >= dialData.GetScriptCount(npcNum)) scriptNum = 0; 
        }
    }

    //��ȭ�� ���������� üũ�ϴ� �Լ�
    public bool TalkDone()
    {
        //���� �������� ����� ���� �� ������ ������ ���� ��ȯ(��ȭ�� ������)
        return dialogueNum >= dialData.GetDialCount(npcNum, scriptNum);
    }

    
    
    
}
