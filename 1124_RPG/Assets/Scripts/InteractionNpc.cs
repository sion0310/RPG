using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionNpc : MonoBehaviour
{
    
    [SerializeField] int npcNum;
    

    public GameObject[] icons;

    public enum NpcState { normal, haveQuest, doingQuest, doneQuest }
    [SerializeField] public NpcState npcState = NpcState.normal;

    


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
