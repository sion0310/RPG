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

    List<Dictionary<string, object>> questList;        //����Ʈ ����� �����´�

    void Awake()
    {
        //������ ��ũ��Ʈ�� �����´�
        nami = CSVReader.Read("Nami");
        kaido = CSVReader.Read("Kaido");
        shanks = CSVReader.Read("Shanks");
        hencock = CSVReader.Read("Hencock");
        buggi = CSVReader.Read("Buggi");

        questList = CSVReader.Read("questList");


        //��� npc��ũ��Ʈ���� ����Ʈ�� �־��ش�
        npcList.Add(nami);
        npcList.Add(kaido);
        npcList.Add(shanks);
        npcList.Add(hencock);
        npcList.Add(buggi);

    }
    
    public Dictionary<string, object> GetDialogue(int npcNum, int dialNum)
    {
        //��ȯ���� npcNum�� ���� dialNum��° �� ���
        return npcList[npcNum][dialNum];
    }

    public string GetQuest(int questNum,string header)
    {
        return questList[questNum][header].ToString();
    }
 

}
