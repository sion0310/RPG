using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionCtrl : MonoBehaviour
{
    [SerializeField] Camera cam;
    RaycastHit hitInfo;                 //레이에 감지된 오브젝트
    public bool isContact = false;      //이펙트 중복실행을 막기위해 사용
    public bool isInterat = false;      //상호작용중인지 아닌지

    public delegate void InteractPro();     //상호작용 델리게이트
    public InteractPro interact_pro = null;

    // Update is called once per frame
    void Update()
    {
        ClickLeftBtn();
    }

    private void FixedUpdate()
    {
        CheckObject();
    }

    //레이로 오브젝트 체크
    void CheckObject()
    {
        Vector3 t_mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);

        // 마우스가 올라갔을때와 아닐때 이펙트를 켜고 끄기위해 Contact와 NotContact함수를 만듬
        if(Physics.Raycast(cam.ScreenPointToRay(t_mousePos),out hitInfo, 100))
        {
            Contact();
        }
        else
        {
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
        }
    }

    void ClickLeftBtn()
    {
        if (!isInterat)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (isContact)
                {
                    Interact();
                }
            }
        }
        
    }
    
    void Interact()
    {
        isInterat = true;
        
        //상호작용 델리게이트 실행
        interact_pro?.Invoke();
        Debug.Log(hitInfo.transform.name);
    }
    
}
