using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionNpc : MonoBehaviour
{
    [SerializeField] DialogueData dialData = null;

    [SerializeField] int npcNum;
    

    public GameObject[] icons;

    public enum NpcState { normal, haveQuest, doingQuest, doneQuest }
    [SerializeField] public NpcState npcState = NpcState.normal;

    [SerializeField]
    int scriptNum = 0;      //대사뭉치
    int dialogueNum = 0;    //몇번째 줄 대사 

    private void Start()
    {
        //처음에 아이콘과 상태를 모두 노말로 시작
        NpcStateSetUp(0);
    }

    //npc상태와 상태에 맞는 아이콘을 켜고 끄는 함수
    public void NpcStateSetUp(int _npcState)
    {
        //npc상태를 매개변수 값으로 바꿔준다.
        npcState = (NpcState)_npcState;
        //모든 아이콘을 끄고
        icons[0].SetActive(false);
        icons[1].SetActive(false);
        icons[2].SetActive(false);
        icons[3].SetActive(false);
        //상태에 맞는 아이콘만 켜준다.
        icons[_npcState].SetActive(true);
    }

    //선택된 npc오브젝트의 넘버를 넘겨주는 함수
    public int GetNum()
    {
        //npcNum를 반환한다
        return npcNum;
    }

    public Dictionary<string, object> GetDialogue()
    {
        //엔피씨 상태가 퀘스트가 있을경우이거나 퀘스트를 완료한 경우 퀘스트 대화를 진행
        if (npcState == NpcState.haveQuest||npcState==NpcState.doneQuest)
        {
            //npc[npcNum]가 가진 대본중 scriptNum번째 대본의 dialogueNum번째 대사를 가져온다
            Dictionary<string, object> dialogue =
                dialData.GetDialogue(npcNum, scriptNum, dialogueNum);
            //다음에 가져올 값들을 수정
            ChangeValue(dialogue);
            //가져온 대사 반환
            return dialogue;
        }
        if (npcState == NpcState.doingQuest || npcState == NpcState.normal)
        {
            return null;
        }
        
        //차후 수정(기본대화, 퀘스트중 대화)
        else return null;
    }

    // 대화가 끝나면 처리하는 함수
    public void ChangeValue(Dictionary<string, object> dialogue)
    {
        //다음대사로 넘어간다(++)
        dialogueNum++;
        //만약 대사가 없으면
        if (dialogue == null)
        {
            //대사 넘버를 초기화 해주고
            dialogueNum = 0;
            //다음 대본으로 넘어간다
            scriptNum++;
            //다음 대본이 없으면 대본 넘버를 초기화 해준다.
            if (scriptNum >= dialData.GetScriptCount(npcNum)) scriptNum = 0; 
        }
    }

    //대화가 끝났는지를 체크하는 함수
    public bool TalkDone()
    {
        //지금 진행중인 대사의 수가 총 대사수를 넘으면 참을 반환(대화가 끝났다)
        return dialogueNum >= dialData.GetDialCount(npcNum, scriptNum);
    }

    
    
    
}
