using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class SupplyPort : MonoBehaviour
{
    [SerializeField] OrderManager orderManager;
    MoveDish moveDish;
    //OnTriggerStay//
    [SerializeField] GameObject particlePrf;
    GameObject beforeObj = null;
    //DestroyAndEffect//
    List<GameObject> destroyObj = new();
    WaitForSeconds wait = new WaitForSeconds(0.02f);
    bool anim;
    GameObject dish;
    [SerializeField] Transform startTf, finishTf;
    [SerializeField] float moveSpeed;
    public void FindDishObj()
    {
        moveDish = GameObject.FindGameObjectWithTag("Dish").GetComponent<MoveDish>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Dish") && !moveDish.isLift && beforeObj != other.gameObject && orderManager.orderList.Count > 0)
        {
            beforeObj = other.gameObject;
            DishManager dishManager = other.GetComponent<DishManager>();
            //onNetaIdListÇÇ‹Ç∆ÇﬂÇƒ1Ç¬ÇÃstringÇ…
            string onNetaIdForString = null;
            dishManager.OnNetaIdList.Sort();
            foreach (int id in dishManager.OnNetaIdList)
            {
                onNetaIdForString += id + ",";
            }
            //OrderManagerÇ…éMÇ…èÊÇ¡ÇƒÇ¢ÇÈÉlÉ^Ç∆éMÇÃéÌóﬁÇëóÇÈ
            orderManager.AscertainOrder(onNetaIdForString, dishManager.dishId, dishManager.searLevel);
            DestroyAndEffect(other.gameObject);
            Instantiate(particlePrf, transform.position, particlePrf.transform.rotation);
        }
    }
    void DestroyAndEffect(GameObject @object)
    {
        destroyObj.Add(@object);
        @object.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        destroyObj.Remove(@object);
        {
            dish = @object;
            dish.tag = "Untagged";
            dish.transform.position = startTf.position;
            anim = true;
        }
    }
    void FixedUpdate()
    {
        if (anim)
        {
            dish.transform.position = Vector3.MoveTowards(dish.transform.position, finishTf.position, moveSpeed);
            if (dish.transform.position == finishTf.position)
            {
                Destroy(dish);
                dish = null;
                anim = false;
            }
        }
    }
}









