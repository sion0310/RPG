using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionCtrl : MonoBehaviour
{
    [SerializeField] Camera cam;
    RaycastHit hitInfo;                 //레이에 감지된 오브젝트
    public bool isContact = false;      //이펙트 중복실행을 막기위해 사용
    public bool isInterat = false;      //상호작용중인지 아닌지

    public delegate void InteractPro(int npcNum);     //상호작용 델리게이트
    public InteractPro interact_pro = null;

    // Update is called once per frame
    void Update()
    {
        //좌클릭 함수
        ClickLeftBtn();
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
            if (!isContact)
            {
                isContact = true;
                //여기에 상호작용가능한 오브젝트에 마우스가 올라갔을때
                //넣고싶은 이펙트같은것을 넣는다
                //응~ 지금은 안만들거야~
            }
        }
        else
        {
            NotContact();
        }
    }
    
    void NotContact()
    {
        if (isContact)
        {
            isContact = false;
            //마우스가 내려가면 꺼줘야 하는 것들 넣기
        }
    }


    void ClickLeftBtn()
    {
        if (isContact)
        {
            //지금 마우스가 이상해서 자꾸 더블클릭됨 그래서 g키 넣은거 나중에 빼세요
            if ((Input.GetKeyDown(KeyCode.G))) //Input.GetMouseButtonDown(0))
            {
                //상호작용할때 실행될 함수
                Interact();

            }
        }
        
    }
    
    void Interact()
    {
        //상호작용 중임을 표시
        isInterat = true;
        int npcNnm = hitInfo.transform.GetComponent<InteractionNpc>().GetNum();
        //상호작용 델리게이트 실행
        interact_pro?.Invoke(npcNnm);
        Debug.Log(hitInfo.transform.name);
    }
    
}
