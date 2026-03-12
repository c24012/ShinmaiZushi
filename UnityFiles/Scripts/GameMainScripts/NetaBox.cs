using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetaBox : MonoBehaviour,IHitClickRay
{
    [SerializeField] GameObject netaPfb;
    Vector3 mousePos;

    public void HitClickRay()
    {
        mousePos = Input.mousePosition;
        Instantiate(netaPfb, Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y,1.9f)),netaPfb.transform.rotation);
    }
}
