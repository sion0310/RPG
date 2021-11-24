using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionType : MonoBehaviour
{
    public bool isNpc;
    public bool isObj;

    [SerializeField] string interactionName;

    public string GetName()
    {
        return interactionName;
    }
}
