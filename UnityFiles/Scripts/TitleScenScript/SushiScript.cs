using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SushiScript : MonoBehaviour
{
    [SerializeField] Image sushi;
    [SerializeField] Sprite[] sushiSprite;
    [SerializeField] Sprite emptyDishSprite;
    public float speed;
    [SerializeField] RectTransform startPos;

    public RectTransform dish;
    [SerializeField] int teleportX;

    void Start()
    {
        sushi.sprite = sushiSprite[Random.Range(0, sushiSprite.Length)];
    }

    void Update()
    {
        
    }
    void FixedUpdate()
    {
        dish.position += new Vector3(speed, 0, 0);
        if (dish.position.x >= teleportX)
        {
            dish.position = startPos.position;
            sushi.sprite = sushiSprite[Random.Range(0, sushiSprite.Length)];
        }
    }

    public void PressButton()
    {
        sushi.sprite = emptyDishSprite;
    }
}
