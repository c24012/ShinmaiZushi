using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustBox : MonoBehaviour
{
    [SerializeField] AudioSource dustSound;
    List<GameObject> destroyObj = new();
    WaitForSeconds wait = new WaitForSeconds(0.02f);

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Dish") || other.CompareTag("Shari") || other.CompareTag("Gunkan") || other.CompareTag("Neta") || other.CompareTag("GunkanNeta") || other.CompareTag("CompletedGunkan") || other.CompareTag("Nori")) 
        {
            if (!Input.GetMouseButton(0))
            {
                if (!destroyObj.Contains(other.gameObject))
                {
                    if (other.CompareTag("Dish"))
                    {
                        if (!other.GetComponent<DishManager>().isInDustBox)
                        {
                            other.GetComponent<DishManager>().isInDustBox = true;
                            OnDishObjCounter(other.gameObject);
                        }
                    }  
                    StartCoroutine(DestroyAndEffect(other.gameObject));
                }
            }
        }
    }

    IEnumerator DestroyAndEffect(GameObject @object)
    {
        destroyObj.Add(@object);
        dustSound.Play();
        @object.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        for (float i = 1; i > 0; i -= 0.1f)
        {
            @object.gameObject.transform.localScale *= i;
            yield return wait;
        }
        destroyObj.Remove(@object);
        Destroy(@object);
    }

    void OnDishObjCounter(GameObject inObj)
    {
        DishManager dishManager = inObj.GetComponent<DishManager>();
        if (dishManager.isOnRice)
        {
            ScoreData.discardCount += dishManager.OnNetaIdList.Count + 1;
        }
        else if (dishManager.isOnGunkan)
        {
            ScoreData.discardCount += dishManager.OnNetaIdList.Count + 2;
        }
    }
}
