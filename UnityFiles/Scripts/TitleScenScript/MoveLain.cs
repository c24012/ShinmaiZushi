using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveLain : MonoBehaviour
{
    public float speed;
    Vector3 startPos;

    public RectTransform lain;
    [SerializeField] int teleportX;

    void Awake()
    {
        startPos = lain.position;
    }

    void Update()
    {
        if (lain.position.x >= teleportX)
        {
            lain.position = startPos;
        }
    }
    void FixedUpdate()
    {
        lain.position += new Vector3(speed, 0, 0);
        
    }
}
