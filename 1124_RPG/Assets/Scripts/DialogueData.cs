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
        //�� �뺻���� �����´�
        talk01 = CSVReader.Read("dialogue01");
        talk02 = CSVReader.Read("dialogue02");
        talk03 = CSVReader.Read("dialogue03");

        //������ �뺻���� ����Ʈ�� �ִ´�
        talkList.Add(talk01);
        talkList.Add(talk02);
        talkList.Add(talk03);
    }
   
    public Dictionary<string, object> GetDialogue(int npcNum, int scriptNum, int dialNum)
    {
        //�� �뺻 ���� scriptNum���� ������ null
        if (talkList.Count < scriptNum) return null;
        //���õ� �뺻�� ��� ���� dialNum ���� ������ x
        if (talkList[scriptNum].Count < dialNum) return null;

        //��ȯ���� talkList���� scriptNum��° �뺻 dialNum��° �� ���
        return talkList[scriptNum][dialNum];
    }
}
