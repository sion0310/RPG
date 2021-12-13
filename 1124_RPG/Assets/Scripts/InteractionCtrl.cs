using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionCtrl : MonoBehaviour
{
    [SerializeField] Camera cam;
    RaycastHit hitInfo;                 //레이에 감지된 오브젝트
    public bool isContact = false;      //이펙트 중복실행을 막기위해 사용
    public bool isInteract = false;      //상호작용중인지 아닌지

    public delegate void InteractPro(GameObject hitobj);   //상호작용 델리게이트
    public InteractPro interact_pro = null;

    GameObject contactEffect;
    GameObject hitobj;
    int npcNum;

    // Update is called once per frame
    void Update()
    {
        //좌클릭 함수
        InteractProcess();
    }

    private void FixedUpdate()
    {
        //레이로 오브젝트체크
        CheckObject();
    }

    void CheckObject()
    {
        Vector3 t_mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);

        // 마우스가 올라갔을때와 아닐때 이펙트를 켜고 끄기위해 Contact와 NotContact함수를 만듬
        if(Physics.Raycast(cam.ScreenPointToRay(t_mousePos),out hitInfo, 100))
        {
            //마우스가 위에 있을때 실행될 함수
            Contact();
        }
        else
        {
            //마우스가 위에 없을때 실행될 함수
            NotContact();   
        }
    }

    void Contact()
    {
        // 태그가 "Interaction"인 오브젝트일 경우에만 함수가 실행되게 함
        if (hitInfo.transform.CompareTag("Interaction"))
        {
            //마우스가 닿아있지 않고, 상호작용중이 아닐때(대화중엔 클릭한 npc말고 이펙트 안뜸)
            if (!isContact && !isInteract)
            {
                //상호작용중으로 바꿔주고
                isContact = true;
                //닿았을때 이펙트 켜주기
                contactEffect = hitInfo.transform.Find("QuestZoneBlue").gameObject;
                contactEffect.SetActive(true);
            }
        }
    }
    void NotContact()
    {
        //상호작용중인 npc는 이펙트가 계속 켜져있게 하기 위해서 상호작용이 아닐때만 꺼줌
        if (isContact && !isInteract)
        {
            isContact = false;
            contactEffect.SetActive(false);
        }
    }


    void InteractProcess()
    {
        //마우스에 닿은npc가 상호작용 중이지 않을때 상호작용 가능
        if (isContact && !isInteract)
        {   
            //상호작용 시작할때 실행될 함수
            if (Input.GetMouseButtonDown(0))
            {
                isInteract = true;
                hitobj = hitInfo.transform.gameObject;
                interact_pro?.Invoke(hitobj);

            }
        }
        //상호작용 중일때 실행되는 함수
        if (isInteract)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                interact_pro?.Invoke(hitobj);
            }
        }
       
    }
    

    //상호작용중이지 않은것은 대화창을 강제로 나가거나 퀘스트를 수락했을때.
    public void NotInteract()
    {
        isInteract = false;
    }
    
}
