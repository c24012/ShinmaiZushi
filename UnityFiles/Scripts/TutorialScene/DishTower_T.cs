using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishTower_T : MonoBehaviour, IHitClickRay
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject dishPrefab;
    [SerializeField] Transform dishSetPos;
    [SerializeField] SupplyPort_T supplyPort;
    public void HitClickRay()
    { 
        GameObject dish = GameObject.FindWithTag("Dish");
        if (dish == null)
        {
            Instantiate(dishPrefab, dishSetPos.position, Quaternion.identity);
            audioSource.Play();
            supplyPort.FindDishObj();
        }
    }
}
