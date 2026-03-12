using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using Unity.VisualScripting;
using UnityEngine;

public class DishManager : MonoBehaviour
{
    public List<int> OnNetaIdList = new();
    public int dishId;
    public int searLevel = 0;
    public bool isOnRice = false;
    public bool isOnGunkan = false;
    public bool isInDustBox = false;
    [SerializeField] GameObject putPtc;
    [SerializeField] AudioSource putNetaSound;
    Rigidbody rb;
    Collider col;
    GameObject beforeObj;
    [SerializeField] GameObject tamagoNori;

    //SetOnNeta//
    [SerializeField,Header("ÉlÉ^Ç∏ÇÍÇÃïù")] float diffPosXZ = 0.01f;
    [SerializeField] float diffPosY = 0.01f;

    //CancelBound//
    [SerializeField] float boundPow;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Neta"))
        {
            if (isOnRice)
            {
                if (OnNetaIdList.Count < 5) SetOnNeta(other.gameObject);
                else if (other.gameObject != beforeObj) CancelBound(other.gameObject);
            }
            else if (isOnGunkan)
            {
                if (other.gameObject != beforeObj) CancelBound(other.gameObject);
            }
            else
            {
                other.GetComponent<NetaController>().isBounded = true;
            }
        }
        else if (other.CompareTag("Shari"))
        {
            if (!isOnGunkan)
            {
                if (!isOnRice) SetOnRice(other.gameObject);
                else if (other.gameObject != beforeObj) CancelBound(other.gameObject);
            }
            else
            {
                if (other.gameObject != beforeObj) CancelBound(other.gameObject);
            }
        }
        else if (other.CompareTag("Gunkan"))
        {
            if (!isOnRice)
            {
                if (!isOnGunkan) SetOnGunkan(other.gameObject);
                else if (other.gameObject != beforeObj) CancelBound(other.gameObject);
            }
            else
            {
                if (other.gameObject != beforeObj) CancelBound(other.gameObject);
            }
        }
        else if (other.CompareTag("CompletedGunkan"))
        {
            if (!isOnRice)
            {
                if (!isOnGunkan) SetOnGunkan(other.gameObject);
                else if (other.gameObject != beforeObj) CancelBound(other.gameObject);
            }
            else
            {
                if (other.gameObject != beforeObj) CancelBound(other.gameObject);
            }
        }
        else if (other.CompareTag("GunkanNeta"))
        {
            other.GetComponent<NetaController>().isBounded = true;
        }
        else if (other.CompareTag("Nori"))
        {
            SetOnTamagoNori(other.gameObject);
        }
    }

    void SetOnNeta(GameObject inObj)
    {
        NetaController netaController = inObj.GetComponent<NetaController>();
        if (netaController.isReleaseClick && !netaController.isTouchDesk)
        {
            OnNetaIdList.Add(netaController.netaId);
            Instantiate(putPtc, transform.position + new Vector3(0,0.1f,0), Quaternion.identity);
            putNetaSound.Play();
            if (netaController.isSeared)
            {
                searLevel = netaController.searLevel;
            }
            rb = inObj.GetComponent<Rigidbody>();
            col = inObj.GetComponent<Collider>();
            rb.isKinematic = true;
            col.enabled = false;
            inObj.transform.position = transform.position + new Vector3(Random.Range(-diffPosXZ, diffPosXZ), 0.05f + diffPosY * (OnNetaIdList.Count - 1), Random.Range(-diffPosXZ, diffPosXZ));
            inObj.transform.rotation = netaController.startRotation;
            inObj.transform.parent = transform;
        }
    }

    void SetOnRice(GameObject inObj)
    {
        NetaController netaController = inObj.GetComponent<NetaController>();
        if (netaController.isReleaseClick && !netaController.isTouchDesk)
        {
            isOnRice = true;
            Instantiate(putPtc, transform.position + new Vector3(0, 0.1f, 0), Quaternion.identity);
            putNetaSound.Play();
            rb = inObj.GetComponent<Rigidbody>();
            col = inObj.GetComponent<Collider>();
            rb.isKinematic = true;
            col.enabled = false;
            inObj.transform.SetPositionAndRotation(transform.position + new Vector3(0, 0.05f, 0), netaController.startRotation);
            inObj.transform.parent = transform;
        }
    }
    
    void SetOnGunkan(GameObject inObj)
    {
        NetaController netaController = inObj.GetComponent<NetaController>();
        if (netaController.isReleaseClick && !netaController.isTouchDesk)
        {
            foreach(int gunkanNeta in netaController.onGunkanNetaIdList)
            {
                OnNetaIdList.Add(gunkanNeta);
            }
            isOnGunkan = true;
            Instantiate(putPtc, transform.position + new Vector3(0, 0.1f, 0), Quaternion.identity);
            putNetaSound.Play();
            rb = inObj.GetComponent<Rigidbody>();
            col = inObj.GetComponent<Collider>();
            rb.isKinematic = true;
            col.enabled = false;
            inObj.transform.SetPositionAndRotation(transform.position + new Vector3(0, 0.04f, 0), netaController.startRotation);
            inObj.transform.parent = transform;
        }
    }

    void SetOnTamagoNori(GameObject inObj)
    {
        NetaController netaController = inObj.GetComponent<NetaController>();
        if (netaController.isReleaseClick && !netaController.isTouchDesk)
        {
            if (OnNetaIdList.Count == 1)
            {
                if (OnNetaIdList[0] == -2)
                {
                    Destroy(inObj);
                    GameObject instantNori = Instantiate(tamagoNori, transform.position + new Vector3(0, 0.05f, 0), tamagoNori.transform.rotation);
                    instantNori.transform.parent = transform;
                    OnNetaIdList[0] = 2;
                }
                else
                {
                    if (inObj.gameObject != beforeObj) CancelBound(inObj.gameObject);
                }
            }
            else
            {
                if (inObj.gameObject != beforeObj) CancelBound(inObj.gameObject);
            }
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
