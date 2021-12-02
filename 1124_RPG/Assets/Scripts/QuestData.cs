using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestData : MonoBehaviour
{
    // 리스트는 스크립트고, 딕셔너리는 다이얼로그다

    //npcList 모든 npc(스크립트들)의 묶음
    List<List<List<Dictionary<string, object>>>> npcList = new List<List<List<Dictionary<string, object>>>>();
    //npc01Talks npc01의 스크립트 묶음
    List<List<Dictionary<string, object>>> npc01Talks = new List<List<Dictionary<string, object>>>();
    List<List<Dictionary<string, object>>> npc02Talks = new List<List<Dictionary<string, object>>>();
    //talk01,talk02 스크립트 각각
    List<Dictionary<string, object>> talk01;
    List<Dictionary<string, object>> talk02;
    List<Dictionary<string, object>> talk03;
    
    void Awake()
    {
        talk01 = CSVReader.Read("dialogue01");
        talk02 = CSVReader.Read("dialogue02");
        talk03 = CSVReader.Read("dialogue03");
        
        //npc01이 해야할 모든 스크립트들을 넣어준다.
        npc01Talks.Add(talk01);
        npc01Talks.Add(talk03);
        
        npc02Talks.Add(talk02);

        //npc들을 리스트에 넣어준다
        npcList.Add(npc01Talks);
        npcList.Add(npc02Talks);
    }

    public Dictionary<string, object> GetDialogue(int npcNum, int scriptNum, int dialNum)
    {
        //npcList가 npcNum보다 적으면 x
        if (npcList.Count < npcNum) return null;
        //npcNum가 가진 스크립트수가 scriptNum 보다 적으면 x
        if (npcList[npcNum].Count < scriptNum) return null;
        //npcNum가 가진 스크립트의 대사수가 dialNum 보다 적으면 x
        if (npcList[npcNum][scriptNum].Count <= dialNum) return null;

        //반환값은 npcNum가 가진 scriptNum번째 스크립트 dialNum번째 줄 대사
        return npcList[npcNum][scriptNum][dialNum];
    }




}
