using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionCtrl : MonoBehaviour
{
    public delegate void InteractPro(GameObject hitobj);   //상호작용 델리게이트
    public InteractPro interact_pro = null;

    [Header("for raycast")]
    [SerializeField] Camera cam;        
    RaycastHit hit;                     

    [Header("check state")]
    public bool isContact = false;      //이펙트 중복실행을 막기위해 사용(마우스가 닿았는지 체크)
    public bool isTalking = false;      //상호작용중인지 아닌지

    GameObject contactEffect;       //마우스가 닿을때 이펙트
    GameObject hitobj;              //레이에 감지된 오브젝트를 담기위한 변수


    void Update()
    {
        CheckObject();      //레이로 오브젝트체크
        InteractProcess();  //좌클릭 함수
    }
    
    //레이로 오브젝트를 체크하는 함수
    void CheckObject()
    {
        Vector3 t_mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);

        // 마우스가 닿으면 이펙트를 켜고 끄기위해 Contact와 NotContact함수를 만듬
        if(Physics.Raycast(cam.ScreenPointToRay(t_mousePos),out hit, 100))
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

    //마우스가 오브젝트에 닿을때 실행된다.
    void Contact()
    {
        // 태그가 "Interaction"인 오브젝트일 경우에만 함수가 실행되게 함(ex:npc)
        if (hit.transform.CompareTag("Interaction"))
        {
            //마우스가 닿아있지 않고(이펙트가 연속으로 재생되지 않기위해),
            //상호작용중이 아닐때(상호작용중인 npc는 이펙트계속 켜놓음)
            if (!isContact && !isTalking)
            {
                //마우스가 닿았다
                isContact = true;
                //닿은 물체의 이펙트 오브젝트를 변수에 넣어준다
                contactEffect = hit.transform.Find("QuestZoneBlue").gameObject;
                //이펙트를 켜준다
                contactEffect.SetActive(true);
            }
        }
    }

    //마우스가 닿지 않으면 실행되는 함수
    void NotContact()
    {
        //상호작용중인 npc는 이펙트가 계속 켜져있게 하기 위해서 상호작용이 아닐때만 꺼줌
        if (isContact && !isTalking)
        {
            //마우스가 닿지 않았다
            isContact = false;
            //이펙트 꺼줌
            contactEffect.SetActive(false);
        }
    }

    //상호작용을 할때 함수
    void InteractProcess()
    {
        //마우스에 닿은npc가 대화 중이지 않을때 대화 가능
        if (isContact && !isTalking)
        {   
            //상호작용 시작할때 실행될 함수
            if (Input.GetMouseButtonDown(0))
            {
                //상호작용중이다
                isTalking = true;
                //레이에 맞은 오브젝트를 변수에 넣어준다
                hitobj = hit.transform.gameObject;
                //상호작용 중일때 실행되는 콜백(어떤 오브젝트인지 넘겨줌)
                interact_pro?.Invoke(hitobj);
            }
        }
        //상호작용 중일때 실행되는 함수
        if (isTalking)
        {
            //대화가 실행되면 G키를 눌러 대사를 넘길 수 있다
            if (Input.GetKeyDown(KeyCode.G))
            {
                interact_pro?.Invoke(hitobj);
            }
        }
    }

}
