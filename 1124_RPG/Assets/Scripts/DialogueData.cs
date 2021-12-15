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

    //����Ʈ ����Ʈ�� �޾� �� ���� 
    List<Dictionary<string, object>> questList;        

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

    public string GetDialogue(int npcNum, int dialNum, string header)
    {
        //��ȯ���� npcNum�� ���� dialNum��° �� ���
        return npcList[npcNum][dialNum][header].ToString();
    }

    public struct Values
    {
        public int _giveNpcNum;
        public int _doneNpcNum;
        
        public string _giveQ;
        public string _doneQ;
        
        public string _questName;
        public string _questExplan;

        public string _condition;

        
    }

    public Values GetQuestValues(int _questIndex)
    {
        Values value = new Values();
        value._giveNpcNum = (int)questList[_questIndex]["giveNpc"];
        value._doneNpcNum = (int)questList[_questIndex]["doneNpc"];
        value._giveQ = questList[_questIndex]["giveQ"].ToString();
        value._doneQ = questList[_questIndex]["doneQ"].ToString();
        value._questName= questList[_questIndex]["questName"].ToString();
        value._questExplan= questList[_questIndex]["explan"].ToString();
        value._condition= questList[_questIndex]["condition"].ToString();

        return value;
    }



}
