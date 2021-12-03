using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueData : MonoBehaviour
{
    List<List<Dictionary<string, object>>> talkList =
                            new List<List<Dictionary<string, object>>>();

    List<Dictionary<string, object>> talk01;
    List<Dictionary<string, object>> talk02;
    List<Dictionary<string, object>> talk03;
    
    void Awake()
    {
        //각 대본들을 가져온다
        talk01 = CSVReader.Read("dialogue01");
        talk02 = CSVReader.Read("dialogue02");
        talk03 = CSVReader.Read("dialogue03");

        //가져온 대본들을 리스트에 넣는다
        talkList.Add(talk01);
        talkList.Add(talk02);
        talkList.Add(talk03);
    }
   
    public Dictionary<string, object> GetDialogue(int npcNum, int scriptNum, int dialNum)
    {
        //총 대본 수가 scriptNum보다 적으면 null
        if (talkList.Count < scriptNum) return null;
        //선택된 대본의 대사 수가 dialNum 보다 적으면 x
        if (talkList[scriptNum].Count < dialNum) return null;

        //반환값은 talkList에서 scriptNum번째 대본 dialNum번째 줄 대사
        return talkList[scriptNum][dialNum];
    }
}
