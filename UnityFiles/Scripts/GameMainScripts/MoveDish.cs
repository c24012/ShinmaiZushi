using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDish : MonoBehaviour,IHitClickRay
{
    Vector3 setPosTarget;
    [SerializeField] Rigidbody rb;

    //Update//
    public bool isLift;
    [SerializeField] float resetMoveSpeed = 0.1f;

    //TrackingMouse_var//
    Vector3 mousePos, targetPos;
    [SerializeField, Header("マウス追尾速度")] float moveSpeed = 20;

    private void Awake()
    {
        rb.isKinematic = true;
    }
    void Start()
    {
        setPosTarget = transform.position;
    }

    void Update()
    {
        if (isLift)
        {
            if (Input.GetMouseButton(0)) TrackingMouse();
            else
            {
                rb.velocity = Vector3.zero;
                isLift = false;
                rb.isKinematic = true;
            } 
        }
        else
        {
            if (transform.position != setPosTarget && rb.constraints != RigidbodyConstraints.FreezeAll)
            {
                transform.position = Vector3.MoveTowards(transform.position, setPosTarget, resetMoveSpeed * Time.deltaTime);
            }
        }
    }

    //マウスカーソルに追尾
    void TrackingMouse()
    {
        rb.isKinematic = false;
        mousePos = Input.mousePosition;
        targetPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 1.9f));
        Vector3 moveVec = targetPos - transform.position;
        rb.velocity = moveVec * moveSpeed;
    }

    public void HitClickRay()
    {
        isLift = true;
    }
}
