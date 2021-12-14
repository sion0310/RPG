using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueData : MonoBehaviour
{
    //npcList 모든 npc(스크립트들)의 묶음
    List<List<Dictionary<string, object>>> npcList =
                            new List<List<Dictionary<string, object>>>();
    
    //npc들의 스크립트를 받아 올 변수
    List<Dictionary<string, object>> nami = null;
    List<Dictionary<string, object>> kaido = null;
    List<Dictionary<string, object>> shanks = null;
    List<Dictionary<string, object>> hencock = null;
    List<Dictionary<string, object>> buggi = null;

    //퀘스트 리스트를 받아 올 변수 
    List<Dictionary<string, object>> questList;        

    void Awake()
    {
        //각각의 스크립트를 가져온다
        nami = CSVReader.Read("Nami");
        kaido = CSVReader.Read("Kaido");
        shanks = CSVReader.Read("Shanks");
        hencock = CSVReader.Read("Hencock");
        buggi = CSVReader.Read("Buggi");

        questList = CSVReader.Read("questList");


        //모든 npc스크립트들을 리스트에 넣어준다
        npcList.Add(nami);
        npcList.Add(kaido);
        npcList.Add(shanks);
        npcList.Add(hencock);
        npcList.Add(buggi);

    }

    public string GetDialogue(int npcNum, int dialNum, string header)
    {
        //반환값은 npcNum가 가진 dialNum번째 줄 대사
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

        return value;
    }



}
