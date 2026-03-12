using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Security;
using Unity.Jobs;
using UnityEngine;

public class GunkanBoard : MonoBehaviour
{
    [SerializeField] GameObject putPtc;
    [SerializeField] AudioSource putNetaSound;
    public bool isOnShari = false;
    public bool isOnNeta = false;
    Rigidbody rb;
    Collider col;
    GameObject beforeObj;
    GameObject onObj;

    //SetOnNeta//
    [SerializeField] float putPosY = 0.01f;
    NetaController netaController;
    NetaController gunkanController;

    //CancelBound//
    [SerializeField] float boundPow;

    //RollNori//
    [SerializeField] GameObject gunkanPrefab;
    public bool isOnGunkan = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Shari") || other.CompareTag("Gunkan") || other.CompareTag("CompletedGunkan"))
        {
            if (!isOnShari && !isOnGunkan) SetOnShari(other.gameObject);
            else if (other.gameObject != beforeObj && other.gameObject != onObj) CancelBound(other.gameObject);
        }
         else if (other.CompareTag("Neta") && other.gameObject != beforeObj) CancelBound(other.gameObject);
        else if (other.CompareTag("Nori"))
        {
            if (isOnShari && !isOnGunkan) RollNori(other.gameObject);
            else if (!isOnShari) CancelDestroy(other.gameObject);
            else if (isOnGunkan && other.gameObject != beforeObj) CancelBound(other.gameObject);
        }
        else if (other.CompareTag("GunkanNeta"))
        {
            if (isOnGunkan && !isOnNeta) setGunkanNeta(other.gameObject);
            else if (isOnNeta && other.gameObject != beforeObj) CancelBound(other.gameObject);
            else other.GetComponent<NetaController>().isBounded = true;
        }
    }

    void SetOnShari(GameObject inObj)
    {
        netaController = inObj.GetComponent<NetaController>();
        if (netaController.isReleaseClick && !netaController.isTouchDesk)
        {
            if (inObj.CompareTag("Shari"))
            {
                isOnShari = true;
                inObj.tag = "OnGunkanBoardShari";
            }
            else if (inObj.CompareTag("Gunkan"))
            {
                isOnGunkan = true;
                inObj.tag = "OnGunkanBoardGunkan";
                gunkanController = netaController;
            }
            else if (inObj.CompareTag("CompletedGunkan"))
            {
                isOnGunkan = true;
                inObj.tag = "OnGunkanBoardCompletedGunkan";
            }
            onObj = inObj;
            Instantiate(putPtc, transform.position + new Vector3(0, 0.1f, 0), Quaternion.identity);
            putNetaSound.Play();
            rb = inObj.GetComponent<Rigidbody>();
            col = inObj.GetComponent<Collider>();
            rb.isKinematic = true;
            col.isTrigger = true;
            inObj.transform.SetPositionAndRotation(transform.position + new Vector3(0, putPosY, 0), netaController.startRotation);
        }
    }
    void setGunkanNeta(GameObject inObj)
    {
        netaController = inObj.GetComponent<NetaController>();
        if (netaController.isReleaseClick && !netaController.isTouchDesk)
        {
            isOnNeta = true;
            Instantiate(putPtc, transform.position + new Vector3(0, 0.1f, 0), Quaternion.identity);
            putNetaSound.Play();
            onObj.tag = "OnGunkanBoardCompletedGunkan";
            inObj.tag = "GunkanInNeta";
            gunkanController.onGunkanNetaIdList.Add(netaController.netaId);
            inObj.layer = 2;
            rb = inObj.GetComponent<Rigidbody>();
            col = inObj.GetComponent<Collider>();
            rb.isKinematic = true;
            col.isTrigger = true;
            inObj.transform.SetPositionAndRotation(transform.position + new Vector3(0, putPosY, 0), netaController.startRotation);
            inObj.transform.parent = onObj.transform;
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
    
    void CancelDestroy(GameObject inObj)
    {
        NetaController netaController = inObj.GetComponent<NetaController>();
        if (netaController.isReleaseClick && !netaController.isTouchDesk)
        {
            netaController.isBounded = true;
        }
    }

    void RollNori(GameObject nori)
    {
        NetaController netaController = nori.GetComponent<NetaController>();
        if (netaController.isReleaseClick && !netaController.isTouchDesk)
        {
            isOnShari = false;
            Instantiate(putPtc, transform.position + new Vector3(0, 0.1f, 0), Quaternion.identity);
            putNetaSound.Play();
            Instantiate(gunkanPrefab, onObj.transform.position, gunkanPrefab.transform.rotation);
            Destroy(onObj);
            onObj = null;
            Destroy(nori);
        }            
    }
}
