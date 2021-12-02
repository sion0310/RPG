using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestData : MonoBehaviour
{
    // ����Ʈ�� ��ũ��Ʈ��, ��ųʸ��� ���̾�α״�

    //npcList ��� npc(��ũ��Ʈ��)�� ����
    List<List<List<Dictionary<string, object>>>> npcList = new List<List<List<Dictionary<string, object>>>>();
    //npc01Talks npc01�� ��ũ��Ʈ ����
    List<List<Dictionary<string, object>>> npc01Talks = new List<List<Dictionary<string, object>>>();
    List<List<Dictionary<string, object>>> npc02Talks = new List<List<Dictionary<string, object>>>();
    //talk01,talk02 ��ũ��Ʈ ����
    List<Dictionary<string, object>> talk01;
    List<Dictionary<string, object>> talk02;
    List<Dictionary<string, object>> talk03;
    
    void Awake()
    {
        talk01 = CSVReader.Read("dialogue01");
        talk02 = CSVReader.Read("dialogue02");
        talk03 = CSVReader.Read("dialogue03");
        
        //npc01�� �ؾ��� ��� ��ũ��Ʈ���� �־��ش�.
        npc01Talks.Add(talk01);
        npc01Talks.Add(talk03);
        
        npc02Talks.Add(talk02);

        //npc���� ����Ʈ�� �־��ش�
        npcList.Add(npc01Talks);
        npcList.Add(npc02Talks);
    }

    public Dictionary<string, object> GetDialogue(int npcNum, int scriptNum, int dialNum)
    {
        //npcList�� npcNum���� ������ x
        if (npcList.Count < npcNum) return null;
        //npcNum�� ���� ��ũ��Ʈ���� scriptNum ���� ������ x
        if (npcList[npcNum].Count < scriptNum) return null;
        //npcNum�� ���� ��ũ��Ʈ�� ������ dialNum ���� ������ x
        if (npcList[npcNum][scriptNum].Count <= dialNum) return null;

        //��ȯ���� npcNum�� ���� scriptNum��° ��ũ��Ʈ dialNum��° �� ���
        return npcList[npcNum][scriptNum][dialNum];
    }




}
