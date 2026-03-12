using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialTextManager : MonoBehaviour
{
    [SerializeField] GameObject senpaiTextPanel;
    [SerializeField] TextMeshProUGUI senpaiText;
    [SerializeField] Animator senpaiTextAnim;

    [SerializeField] GameObject questionButtonsPanel;
    [SerializeField] Animator questionButtonsAnim;

    int senpaiTextPage;
    bool isManual,isGomi, isNigiri,isAburi, isGunkan, isTamago;

    private void Awake()
    {
        senpaiText.text = "今日は先輩の僕が\n寿司の作り方を教えるよ！\n何について聞きたい？";
    }

    private void Start()
    {
        Invoke(nameof(SenpaiTextEnterAnim), 2f);
        Invoke(nameof(QuestionButtonsAnim), 3f);
    }

    public void ManualButton()
    {
        
        ButtonPressed();
        SenpaiTextEnterAnim();
        if (senpaiTextPage == 0)
        {
            isManual = true;
            senpaiText.text = "画面の左上に紙のアイコンは見える？\nそれをクリックしたらマニュアルが開けるよ\n注文の一覧と作り方をまとめてるよ\n(クリックで進む)";
        }
        else if(senpaiTextPage == 1)
        {
            senpaiText.text = "目次の文字をクリックしたら\nそのページに飛ぶことができるよ\nショートカットってやつだね\n(クリックで進む)";
        }
        else if(senpaiTextPage == 2)
        {
            FinishText();
        }
    }
    
    public void GomiButton()
    {
        
        ButtonPressed();
        SenpaiTextEnterAnim();
        if (senpaiTextPage == 0)
        {
            isGomi = true;
            senpaiText.text = "まずはAキーを押して左を向いてみよう\n左側に黒いゴミ箱は見える？\n(クリックで進む)";
        }
        else if(senpaiTextPage == 1)
        {
            senpaiText.text = "ここにお皿や具材をクリック＆ドラッグして\n廃棄にすることができるよ\n(クリックで進む)";
        }
        else if (senpaiTextPage == 2)
        {
            senpaiText.text = "間違えて注文と違うものを\n作ってしまったときに活用してね\n(クリックで進む)";
        }
        else if (senpaiTextPage == 3)
        {
            senpaiText.text = "Dキーを押して正面の向きにもどろう\n(クリックで進む)";
        }
        else if(senpaiTextPage == 4)
        {
            FinishText();
        }
    }
    
    public void NigiriButton()
    {
        
        ButtonPressed();
        SenpaiTextEnterAnim();
        if (senpaiTextPage == 0)
        {
            isNigiri = true;
            senpaiText.text = "研修では「まぐろ」を作ってみるね\nほかの握りネタも作り方は一緒だよ\n(クリックで進む)";
        }
        else if(senpaiTextPage == 1)
        {
            senpaiText.text = "まずはＳキーでしゃがんでお皿を取ろう\nお皿のタワーをクリッしてお皿を配置するよ\n握りのお皿は「白い皿」覚えてね\n(クリックで進む)";
        }
        else if (senpaiTextPage == 2)
        {
            senpaiText.text = "次はお皿にシャリをのせよう\n正面の左側にシャリ桶は見える？\nクリック＆ドラッグでお皿の上にのせてね\n(クリックで進む)";
        }
        else if (senpaiTextPage == 3)
        {
            senpaiText.text = "シャリが準備できたらネタだね\nまぐろのネタケースは左上の箱だよ\nクリック＆ドラッグでお皿の上にのせよう\n(クリックで進む)";
        }
        else if (senpaiTextPage == 4)
        {
            senpaiText.text = "最後にお客さんに提供しよう\nお皿をクリック＆ドラッグして\n右側にあるレールにのせたら提供完了！\n(クリックで進む)";
        }
        else if(senpaiTextPage == 5)
        {
            FinishText();
        }
    }
    
    public void AburiButton()
    {
        
        ButtonPressed();
        SenpaiTextEnterAnim();
        if (senpaiTextPage == 0)
        {
            isAburi = true;
            senpaiText.text = "研修では「まぐろ炙り」を作ってみるね\nほかの炙りネタも作り方は一緒だよ\n(クリックで進む)";
        }
        else if(senpaiTextPage == 1)
        {
            senpaiText.text = "まずはＳキーでしゃがんでお皿を取ろう\nお皿のタワーをクリッしてお皿を配置するよ\n炙りのお皿は「赤い皿」覚えてね\n(クリックで進む)";
        }
        else if (senpaiTextPage == 2)
        {
            senpaiText.text = "次はお皿にシャリをのせよう\n正面の左側にシャリ桶は見える？\nクリック＆ドラッグでお皿の上にのせてね\n(クリックで進む)";
        }
        else if (senpaiTextPage == 3)
        {
            senpaiText.text = "シャリが準備できたらネタを炙ろう\nまぐろをネタケースからクリックで持ちつつ\nAキーで左を向いて炙り台にのせよう\n(クリックで進む)";
        }
        else if (senpaiTextPage == 4)
        {
            senpaiText.text = "奥のガスバーナーをクリック＆ドラッグして\nこげないように炙ろう\nゲージが最大にならない程度がいいよ\n(クリックで進む)";
        }
        else if (senpaiTextPage == 5)
        {
            senpaiText.text = "炙ったネタをクリックで持ったまま\nDキーで正面を向いて用意した皿にのせてね\n(クリックで進む)";
        }
        else if (senpaiTextPage == 6)
        {
            senpaiText.text = "最後にお客さんに提供しよう\nお皿をクリック＆ドラッグして\n右側にあるレールにのせたら提供完了！\n(クリックで進む)";
        }
        else if(senpaiTextPage == 7)
        {
            FinishText();
        }
    }

    public void GunkanButton()
    {

        ButtonPressed();
        SenpaiTextEnterAnim();
        if (senpaiTextPage == 0)
        {
            isGunkan = true;
            senpaiText.text = "研修では「うに軍艦」を作ってみるね\nほかの軍艦ネタも作り方は一緒だよ\n(クリックで進む)";
        }
        else if (senpaiTextPage == 1)
        {
            senpaiText.text = "まずはＳキーでしゃがんでお皿を取ろう\nお皿のタワーをクリッしてお皿を配置するよ\n軍艦のお皿は「黒い皿」覚えてね\n(クリックで進む)";
        }
        else if (senpaiTextPage == 2)
        {
            senpaiText.text = "軍艦は巻き台の上でつくるよ\nDキーを押して右を向いてみよう\n木目の板が巻き台だよ\n(クリックで進む)";
        }
        else if (senpaiTextPage == 3)
        {
            senpaiText.text = "次は巻き台にシャリをのせよう\n左側にシャリ桶は見える？\nクリック＆ドラッグで巻き台にのせてね\n(クリックで進む)";
        }
        else if (senpaiTextPage == 4)
        {
            senpaiText.text = "シャリが準備できたら海苔を巻こう\n海苔が右側においてあるのが見える？\nクリック＆ドラッグで巻き台の上においてね\n(クリックで進む)";
        }
        else if (senpaiTextPage == 5)
        {
            senpaiText.text = "次にネタをのせよう\nうには一番左のネタケースだよ\nクリック＆ドラッグで軍艦にのせてね\n(クリックで進む)";
        }
        else if (senpaiTextPage == 6)
        {
            senpaiText.text = "完成した軍艦をお皿にのせるよ\n軍艦をクリックしたままAキーで正面を向いて\n用意したお皿にのせてね\n(クリックで進む)";
        }
        else if (senpaiTextPage == 7)
        {
            senpaiText.text = "最後にお客さんに提供しよう\nお皿をクリック＆ドラッグして\n右側にあるレールにのせたら提供完了！\n(クリックで進む)";
        }
        else if (senpaiTextPage == 8)
        {
            FinishText();
        }
    }

    public void TamagoButton()
    {

        ButtonPressed();
        SenpaiTextEnterAnim();
        if (senpaiTextPage == 0)
        {
            isTamago = true;
            senpaiText.text = "「たまご」は基本握りと一緒の作り方だけど\n最後に海苔を巻かないといけないよ\n(クリックで進む)";
        }
        else if (senpaiTextPage == 1)
        {
            senpaiText.text = "まずはＳキーでしゃがんでお皿を取ろう\nお皿のタワーをクリッしてお皿を配置するよ\n握りのお皿は「白い皿」\n(クリックで進む)";
        }
        else if (senpaiTextPage == 2)
        {
            senpaiText.text = "次はお皿にシャリをのせよう\n正面のシャリ桶から\nクリック＆ドラッグでお皿の上にのせてね\n(クリックで進む)";
        }
        else if (senpaiTextPage == 3)
        {
            senpaiText.text = "シャリが準備できたらネタだね\nたまごのネタケースは中央左の箱だよ\nクリック＆ドラッグでお皿の上にのせよう\n(クリックで進む)";
        }
        else if (senpaiTextPage == 4)
        {
            senpaiText.text = "たまごはここで海苔を巻くよ\nシャリ桶の隣の海苔をクリック＆ドラッグで\nお皿にのせるだけで巻けるよ\n(クリックで進む)";
        }
        else if (senpaiTextPage == 5)
        {
            senpaiText.text = "最後にお客さんに提供しよう\nお皿をクリック＆ドラッグして\n右側にあるレールにのせたら提供完了！\n(クリックで進む)";
        }
        else if (senpaiTextPage == 6)
        {
            FinishText();
        }
    }
    public void ExitButton()
    {
        ButtonPressed();
        SenpaiTextEnterAnim();
        senpaiText.text = "注文の出し方が分からなくなったら\nいつでも来てね";
        Invoke(nameof(ExitTutorial), 2f) ;
    }




    void ButtonPressed()
    {
        questionButtonsPanel.SetActive(false);
    }

    public void PressSenpaiText()
    {
        senpaiTextPage++;
        if (isManual) ManualButton();
        else if (isGomi) GomiButton();
        else if (isNigiri) NigiriButton();
        else if (isAburi) AburiButton();
        else if (isGunkan) GunkanButton();
        else if (isTamago) TamagoButton();
        else senpaiTextPage = 0;
    }

    void FinishText()
    {
        isManual = false;
        isNigiri = false;
        isGunkan = false;
        isTamago = false;
        senpaiText.text = "理解できた？\n他に聞きたいことはある？";
        questionButtonsPanel.SetActive(true);
        QuestionButtonsAnim();
        senpaiTextPage = 0;
    }

    void SenpaiTextEnterAnim()
    {
        senpaiTextAnim.SetTrigger("EnterTrigger");
    }

    void QuestionButtonsAnim()
    {
        questionButtonsAnim.SetTrigger("EnterTrigger");
    }

    void ExitTutorial()
    {
        SceneManager.LoadScene("TitleScene");
    }





}
