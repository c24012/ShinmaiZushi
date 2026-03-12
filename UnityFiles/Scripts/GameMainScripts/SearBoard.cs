using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearBoard : MonoBehaviour
{
    [SerializeField] GameObject putPtc;
    [SerializeField] AudioSource putNetaSound;
    public bool isOnNeta = false;
    Rigidbody rb;
    Collider col;
    GameObject beforeObj;
    GameObject onObj;

    //SetOnNeta//
    [SerializeField] float putPosY = 0.01f;

    //CancelBound//
    [SerializeField] float boundPow;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Neta"))
        {
            if (!isOnNeta) SetOnNeta(other.gameObject);
            else if(other.gameObject != beforeObj && other.gameObject != onObj) CancelBound(other.gameObject);
        }
        else if (other.CompareTag("Shari") || other.CompareTag("Gunkan") || other.CompareTag("Nori") || other.CompareTag("CompletedGunkan"))
        {
            if(other.gameObject != beforeObj) CancelBound(other.gameObject);
        }
            
    }

    void SetOnNeta(GameObject inObj)
    {
        NetaController netaController = inObj.GetComponent<NetaController>();
        if (netaController.isReleaseClick && !netaController.isTouchDesk)
        {
            isOnNeta = true;
            onObj = inObj;
            Instantiate(putPtc, transform.position + new Vector3(0, 0.1f, 0), Quaternion.identity);
            putNetaSound.Play();
            inObj.tag = "OnSearBoardNeta";
            rb = inObj.GetComponent<Rigidbody>();
            col = inObj.GetComponent<Collider>();
            rb.isKinematic = true;
            col.isTrigger = true;
            inObj.transform.SetPositionAndRotation(transform.position + new Vector3(0, putPosY, 0), netaController.startRotation);
        }
    }

    void CancelBound(GameObject inObj)
    {
        NetaController netaController = inObj.GetComponent<NetaController>();
        if (netaController.isReleaseClick && !netaController.isTouchDesk)
        {
            beforeObj = inObj;
            rb = inObj.GetComponent<Rigidbody>();
            rb.velocity = new Vector3(Random.Range(-boundPow, boundPow), boundPow, Random.Range(-boundPow, boundPow));
            netaController.isBounded = true;
        }
    }
}

