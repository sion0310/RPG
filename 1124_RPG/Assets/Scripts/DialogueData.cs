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

    void Awake()
    {
        //각각의 스크립트를 가져온다
        nami = CSVReader.Read("Nami");
        kaido = CSVReader.Read("Kaido");
        shanks = CSVReader.Read("Shanks");
        hencock = CSVReader.Read("Hencock");
        buggi = CSVReader.Read("Buggi");


        //모든 스크립트들을 리스트에 넣어준다
        npcList.Add(nami);
        npcList.Add(kaido);
        npcList.Add(shanks);
        npcList.Add(hencock);
        npcList.Add(buggi);

    }
    
    public Dictionary<string, object> GetDialogue(int npcNum, int dialNum)
    {
        //npc의 수가 npcNum보다 적으면 x
        if (npcList.Count <= npcNum) return null;
        //스크립트의 대사가 dialNum 보다 적으면 x
        if (npcList[npcNum].Count <= dialNum) return null;

        //반환값은 npcNum가 가진 dialNum번째 줄 대사
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
