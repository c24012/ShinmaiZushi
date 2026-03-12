using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnerManager_T : MonoBehaviour,IHitClickRay
{
    [SerializeField] CameraController_T changeCamera;
    [SerializeField] AudioSource audioSource;
    [SerializeField] ParticleSystem firePtc;
    Vector3 startPos;
    Quaternion startRotat;
    [SerializeField] Rigidbody rb;
    [SerializeField] Collider fire;

    //Update//
    public bool isLift;
    [SerializeField] float resetMoveSpeed = 0.1f;
    bool isAudioPlaying = false;

    //TrackingMouse_var//
    Vector3 mousePos, targetPos;
    [SerializeField, Header("マウス追尾速度")] float moveSpeed = 20;
    [SerializeField, Header("マウス追尾Z軸")] float movePosZ = 1.9f;
    [SerializeField] Transform rotationObj;


    private void Awake()
    {
        rb.isKinematic = true;
        fire.enabled = false;
    }
    void Start()
    {
        startPos = transform.position;
        startRotat = transform.rotation;
    }

    void Update()
    {
        if (isLift)
        {
            if (Input.GetMouseButton(0) && changeCamera.focusNum == 1) TrackingMouse();
            else
            {
                rb.isKinematic = false;
                rb.velocity = Vector3.zero;
                fire.enabled = false;
                isLift = false;
            }
        }
        else
        {
            if (transform.position != startPos)
            {
                rb.isKinematic = true;
                rb.constraints = RigidbodyConstraints.None;
                transform.position = Vector3.MoveTowards(transform.position, startPos, resetMoveSpeed * Time.deltaTime);
            }
            else
            {
                if (transform.rotation != startRotat)
                {
                    rb.constraints = RigidbodyConstraints.FreezePosition;
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, startRotat, resetMoveSpeed * Time.deltaTime * 200);
                }
                else
                {
                    rb.isKinematic = true;
                }
            }
        }

        if (isLift)
        {
            if (!isAudioPlaying)
            {
                isAudioPlaying = true;
                firePtc.Play();
                audioSource.Play();
            }
        }
        else
        {
            if (isAudioPlaying)
            {
                isAudioPlaying = false;
                firePtc.Stop();
                audioSource.Stop();
            }
        }

    }

    //マウスカーソルに追尾
    void TrackingMouse()
    {
        rb.isKinematic = false;
        rb.constraints = RigidbodyConstraints.None;
        mousePos = Input.mousePosition;
        targetPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, movePosZ));
        Vector3 moveVec = targetPos - transform.position;
        rb.velocity = moveVec * moveSpeed;
        transform.rotation = rotationObj.rotation;
        fire.enabled = true;
    }

    public void HitClickRay()
    {
        isLift = true;
    }
}
