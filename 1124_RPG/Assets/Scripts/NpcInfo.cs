using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcInfo : MonoBehaviour
{
    //npc���� �ѹ��� �ο��ϱ� ���� ����(�ν�����â���� ���� �ο�)
    [Header("Npc Number")]
    [SerializeField] int npcNum;

    //�� npc���� �������� �������� ����
    [Header("Npc State Icons")]
    [SerializeField] GameObject[] icons;

    //npc ���µ�
    public enum NpcState { normal, haveQuest, doingQuest, doneQuest }

    [Header("Npc State")]
    public NpcState npcState = NpcState.normal;

    


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


    
}
