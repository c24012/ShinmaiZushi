using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController_T : MonoBehaviour
{
    [SerializeField] GameManager_T gameManager;

    [SerializeField] Transform[] focusPos = new Transform[4];
    [SerializeField] Transform cameraPos;
    [SerializeField] Transform cameraBottomPos;
    [SerializeField] Transform to;
    public float rotationSpeed = 10.0f;

    float step;

    Vector3 startCameraPos;
    float verticalCameraCounter;
    [SerializeField] float verticalMoveSpeed;

    int beforePosNum = 2;

    //camera  0:bottom 1:left 2:forward 3:right
    public int focusNum = 2;

    private void Awake()
    {
        to.transform.position = focusPos[2].position;
        cameraPos.LookAt(to);
    }

    void Start()
    {
        startCameraPos = cameraPos.position;
        ChangeFocus();
    }

    void Update()
    {
        
            step = rotationSpeed * Time.deltaTime * Vector3.Angle(Camera.main.transform.forward, focusPos[focusNum].position - cameraPos.position);
            cameraPos.rotation = Quaternion.RotateTowards(cameraPos.rotation, Quaternion.LookRotation(to.position - cameraPos.position), step);


            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.S))
            {
                int beforePosNumCheck = focusNum;

                //カメラコントロールASD
                
                if (Input.GetKeyDown(KeyCode.A))
                {
                    if (focusNum > 1) focusNum--;
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    if (focusNum < 3 && focusNum != 0) focusNum++;
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    if (focusNum == 2) focusNum = 0;
                }
                else if (Input.GetKeyUp(KeyCode.S))
                {
                    if (focusNum == 0) focusNum = 2;
                }

                if (beforePosNumCheck != focusNum) beforePosNum = beforePosNumCheck;

                ChangeFocus();
            }

            if (focusNum == 0)
            {
                if (verticalCameraCounter < 1) verticalCameraCounter += Time.deltaTime * verticalMoveSpeed;
                else verticalCameraCounter = 1;
            }
            else
            {
                if (verticalCameraCounter > 0) verticalCameraCounter -= Time.deltaTime * verticalMoveSpeed;
                else verticalCameraCounter = 0;
            }
            cameraPos.position = Vector3.Lerp(startCameraPos, cameraBottomPos.position, Mathf.Clamp(verticalCameraCounter, 0, 1));
        
    }

    void ChangeFocus()
    {
        to.position = focusPos[focusNum].position;
    }


}
