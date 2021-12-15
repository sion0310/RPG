using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcInfo : MonoBehaviour
{
    //npc들의 넘버를 부여하기 위한 변수(인스펙터창에서 각각 부여)
    [Header("Npc Number")]
    [SerializeField] int npcNum;

    //각 npc들의 아이콘을 가져오는 변수
    [Header("Npc State Icons")]
    [SerializeField] GameObject[] icons;

    //npc 상태들
    public enum NpcState { normal, haveQuest, doingQuest, doneQuest }

    [Header("Npc State")]
    public NpcState npcState = NpcState.normal;

    


    //npc상태와 상태에 맞는 아이콘을 켜고 끄는 함수
    public void NpcStateSetUp(NpcState _npcState)
    {
        //npc상태를 매개변수 값으로 바꿔준다.
        npcState = _npcState;
        //모든 아이콘을 끄고
        icons[0].SetActive(false);
        icons[1].SetActive(false);
        icons[2].SetActive(false);
        icons[3].SetActive(false);
        //상태에 맞는 아이콘만 켜준다.
        icons[(int)_npcState].SetActive(true);
    }

    //선택된 npc오브젝트의 넘버를 넘겨주는 함수
    public int GetNum()
    {
        //npcNum를 반환한다
        return npcNum;
    }


    
}
