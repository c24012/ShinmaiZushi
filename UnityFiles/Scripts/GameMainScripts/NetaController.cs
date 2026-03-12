using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class NetaController : MonoBehaviour,IHitClickRay
{
    [SerializeField] Rigidbody rb;
    [SerializeField] Collider col;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] Animator netaAnim;
    [Header("ネタID")] public int netaId;
    [Header("炙りverネタID")] public int searedNetaId;
    [SerializeField,Header("炙り設定")] bool canSearNeta;
    public float[] searTime = new float[3];
    [SerializeField] Material[] searMaterials = new Material[3];
    [SerializeField] GameObject searLevelVer;
    [SerializeField] Image searLevelImage;
    Vector3 cameraPos;

    //軍艦用//
    [NonSerialized] public List<int> onGunkanNetaIdList = new();

    [NonSerialized]public Quaternion startRotation;

    //Update_ver//
    [NonSerialized] public bool isReleaseClick = false;
    [NonSerialized] public bool isTouchDesk = false;
    [NonSerialized] public bool isBounded = false;
    [NonSerialized] public bool isSeared = false;

    //TrackingMouse_var//
    Vector3 mousePos, targetPos;
    [SerializeField,Header("マウス追尾速度")] float moveSpeed = 20;

    //Sear//
    float searTimer = 0;
    public int searLevel = 0;


    private void Awake()
    {
        isReleaseClick = false;
        if (canSearNeta) searLevelVer.SetActive(false);
    }

    public void Start()
    {
        startRotation = transform.rotation;
        cameraPos = Camera.main.transform.position;
    }

    private void Update()
    {
        //クリック中は追尾,1度離したり床に触れたら追尾しない
        if (Input.GetMouseButton(0) && !isReleaseClick && !isTouchDesk)
        {
            TrackingMouse();
        }
        else
        {
            isReleaseClick = true;
            rb.freezeRotation = false;
        }
    }

    private void FixedUpdate()
    {
        if (canSearNeta)
        {
            searLevelVer.transform.LookAt(cameraPos);
        }
    }

    //マウスカーソルに追尾
    void TrackingMouse()
    {
        mousePos = Input.mousePosition;
        targetPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 1.9f));
        Vector3 moveVec = (targetPos - transform.position);
        rb.velocity = moveVec * moveSpeed;
    }


    private void OnCollisionEnter(Collision collision)
    {
        //机に落ちたら(触れたら)削除
        if (collision.gameObject.CompareTag("SushiDesk") || collision.gameObject.CompareTag("NetaBox"))
        {
            StartCoroutine(DestroyEffect());
            isTouchDesk = true;
        }
        else if(collision.gameObject.CompareTag("Dish") && isBounded)
        {
            StartCoroutine(DestroyEffect());
            isTouchDesk = true;
        }
        else if(collision.gameObject.CompareTag("SearBoard") && isBounded)
        {
            StartCoroutine(DestroyEffect());
            isTouchDesk = true;
        }
        else if(collision.gameObject.CompareTag("GunkanBoard") && isBounded)
        {
            StartCoroutine(DestroyEffect());
            isTouchDesk = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //burnerの火に触れたらタイマー蓄積で変化
        if (other.gameObject.CompareTag("Burner") && canSearNeta)
        {
            Sear();
        }
    }

    //削除エフェクト
    public IEnumerator DestroyEffect()
    {
        //軍艦は海苔付きのため廃棄２個分
        if (CompareTag("Gunkan"))
        {
           ScoreData.discardCount += 2;
        }
        //ネタ付きはネタの廃棄エフェクトを呼ぶ
        else if (CompareTag("CompletedGunkan"))
        {
            ScoreData.discardCount += 3;
        }
        //その他は１個分
        else
        {
            ScoreData.discardCount += 1;
        }

        netaAnim.SetTrigger("DestroyTrigger");
        yield return new WaitForSeconds(1f);
        
        Destroy(gameObject);
    }

    void Sear()
    {
        if (!searLevelVer.activeInHierarchy) { searLevelVer.SetActive(true); }
        //時間経過で見た目が変化
        searTimer += Time.deltaTime;
        if (searTimer >= searTime[0] && searLevel == 0)
        {
            meshRenderer.material = searMaterials[searLevel];
            searLevel++;
            isSeared = true;
            netaId = searedNetaId;
        }
        else if (searTimer >= searTime[1] && searLevel == 1)
        {
            meshRenderer.material = searMaterials[searLevel];
            searLevel++;
        }
        else if (searTimer >= searTime[2] && searLevel == 2)
        {
            meshRenderer.material = searMaterials[searLevel];
            searLevel++;
        }

        searLevelImage.fillAmount = searTimer / searTime[2];
    }

    public void HitClickRay()
    {
        rb.isKinematic = false;
        col.isTrigger = false;
        rb.freezeRotation = true;
        if (CompareTag("OnSearBoardNeta")) tag = "Neta";
        else if (CompareTag("OnGunkanBoardShari")) tag = "Shari";
        else if (CompareTag("OnGunkanBoardGunkan")) tag = "Gunkan";
        else if (CompareTag("OnGunkanBoardCompletedGunkan")) tag = "CompletedGunkan";
        isReleaseClick = false;
        if (canSearNeta) searLevelVer.SetActive(false);
    }

}
