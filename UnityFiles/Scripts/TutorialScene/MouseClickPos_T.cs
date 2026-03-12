using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickPos_T : MonoBehaviour
{
    [SerializeField] GameManager_T gameManager;

    [SerializeField] SearBoard searBoard;
    [SerializeField] GunkanBoard gunkanBoard;
    Ray mouseRay;

    void Update()
    {
        if (!gameManager.isOpenManual)
        {
            mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(mouseRay, out hit, 15))
                {
                    //ネタボックスの場合、各ネタを生成
                    if (hit.collider.gameObject.CompareTag("NetaBox"))
                    {
                        if (hit.collider.gameObject.TryGetComponent(out IHitClickRay Ihit))
                        {
                            Ihit.HitClickRay();
                        }
                    }
                    //皿の場合、皿を持つ
                    else if (hit.collider.gameObject.CompareTag("Dish"))
                    {
                        if (hit.collider.gameObject.TryGetComponent(out IHitClickRay Ihit))
                        {
                            Ihit.HitClickRay();
                        }
                    }
                    //皿タワーの場合、上に皿を出す
                    else if (hit.collider.gameObject.CompareTag("DishTower"))
                    {
                        if (hit.collider.gameObject.TryGetComponent(out IHitClickRay Ihit))
                        {
                            Ihit.HitClickRay();
                        }
                    }
                    //炙り台に置いたネタの場合、ネタを持つ
                    else if (hit.collider.gameObject.CompareTag("OnSearBoardNeta"))
                    {
                        if (hit.collider.gameObject.TryGetComponent(out IHitClickRay Ihit))
                        {
                            searBoard.isOnNeta = false;
                            Ihit.HitClickRay();
                        }
                    }
                    //バーナーの場合、バーナーを持つ
                    else if (hit.collider.gameObject.CompareTag("Burner"))
                    {
                        if (hit.collider.gameObject.TryGetComponent(out IHitClickRay Ihit))
                        {
                            Ihit.HitClickRay();
                        }
                    }
                    //巻き台に置いたシャリの場合、シャリを持つ
                    else if (hit.collider.gameObject.CompareTag("OnGunkanBoardShari"))
                    {
                        if (hit.collider.gameObject.TryGetComponent(out IHitClickRay Ihit))
                        {
                            gunkanBoard.isOnShari = false;
                            Ihit.HitClickRay();
                        }
                    }
                    //軍艦シャリの場合、軍艦シャリを持つ
                    else if (hit.collider.gameObject.CompareTag("OnGunkanBoardGunkan"))
                    {
                        if (hit.collider.gameObject.TryGetComponent(out IHitClickRay Ihit))
                        {
                            gunkanBoard.isOnShari = false;
                            gunkanBoard.isOnGunkan = false;
                            gunkanBoard.isOnNeta = false;
                            Ihit.HitClickRay();
                        }
                    }
                    //軍艦ネタ付きの場合、軍艦ネタ付きを持つ
                    else if (hit.collider.gameObject.CompareTag("OnGunkanBoardCompletedGunkan"))
                    {
                        if (hit.collider.gameObject.TryGetComponent(out IHitClickRay Ihit))
                        {
                            gunkanBoard.isOnShari = false;
                            gunkanBoard.isOnGunkan = false;
                            gunkanBoard.isOnNeta = false;
                            Ihit.HitClickRay();
                        }
                    }
                }
            }

        }
    }

}

