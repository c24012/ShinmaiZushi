using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryTray : MonoBehaviour
{
    Rigidbody rb;
    GameObject beforeObj;
    [SerializeField] float boundPow;
    string beforeTag;


    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Untagged") && !other.CompareTag("TemporaryObj"))
        {
            beforeObj = other.gameObject;
            beforeTag = other.tag;
            other.tag = "TemporaryObj";
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

    public void IsLift()
    {
        beforeObj.tag = beforeTag;
    }
}
