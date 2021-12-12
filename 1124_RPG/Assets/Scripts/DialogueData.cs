using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueData : MonoBehaviour
{
    //npcList ��� npc(��ũ��Ʈ��)�� ����
    List<List<Dictionary<string, object>>> npcList =
                            new List<List<Dictionary<string, object>>>();
    
    //npc���� ��ũ��Ʈ�� �޾� �� ����
    List<Dictionary<string, object>> nami = null;
    List<Dictionary<string, object>> kaido = null;
    List<Dictionary<string, object>> shanks = null;
    List<Dictionary<string, object>> hencock = null;
    List<Dictionary<string, object>> buggi = null;

    void Awake()
    {
        //������ ��ũ��Ʈ�� �����´�
        nami = CSVReader.Read("Nami");
        kaido = CSVReader.Read("Kaido");
        shanks = CSVReader.Read("Shanks");
        hencock = CSVReader.Read("Hencock");
        buggi = CSVReader.Read("Buggi");


        //��� ��ũ��Ʈ���� ����Ʈ�� �־��ش�
        npcList.Add(nami);
        npcList.Add(kaido);
        npcList.Add(shanks);
        npcList.Add(hencock);
        npcList.Add(buggi);

    }
    
    public Dictionary<string, object> GetDialogue(int npcNum, int dialNum)
    {
        //npc�� ���� npcNum���� ������ x
        if (npcList.Count <= npcNum) return null;
        //��ũ��Ʈ�� ��簡 dialNum ���� ������ x
        if (npcList[npcNum].Count <= dialNum) return null;

        //��ȯ���� npcNum�� ���� dialNum��° �� ���
        return npcList[npcNum][dialNum];
    }

    public int GetDialCount(int npcNum)
    {
        return npcList[npcNum].Count;
    }

    public int GetScriptCount()
    {
        return npcList.Count;
    }

    public void Example()
    {
        int num = 2;
        string str;
        str = "Dialogue" + num.ToString();


        Debug.Log(nami[1][str].ToString());
    }

}
