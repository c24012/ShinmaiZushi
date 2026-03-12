using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

public class NoriTable : MonoBehaviour
{
    List<GameObject> beforeObjList = new();
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Nori") && !beforeObjList.Contains(collision.gameObject))
        {
            NetaController netaController = collision.gameObject.GetComponent<NetaController>();
            if (netaController.isReleaseClick && !netaController.isTouchDesk)
            {
                beforeObjList.Add(collision.gameObject);
                StartCoroutine(WaitDestroy(netaController,collision.gameObject));
            }
        }
    }

    IEnumerator WaitDestroy(NetaController netaController,GameObject inObj)
    {
        IEnumerator enumerator = netaController.DestroyEffect();
        yield return enumerator;
        beforeObjList.Remove(inObj);
    }
}
