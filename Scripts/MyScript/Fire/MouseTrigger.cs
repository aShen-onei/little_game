using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTrigger : MonoBehaviour
{
    public SignalSender OnMouseUpSignal;
    public SignalSender OnMouseDownSignal;
    public GameObject Target;
    // Start is called before the first frame update
    void Start()
    {
        if(Target == null)
        {
            Target = GameObject.Find("weapon");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Target.SendMessage("OnFire");
        }
        if(Input.GetMouseButtonUp(0))
        {
            Target.SendMessage("StopFire");
        }
    }
}
