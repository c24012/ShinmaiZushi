# 「新米寿司」

## Download
* [ShinmaiZushi_v2.0](https://github.com/c24012/ShinmaiZushi/releases/tag/v2.0)

## ファイル構成  
* [UnityのScript](./UnityFiles)  
* [スクリーンショット](./ScreenShot)
* [紹介動画](./movie)

## ゲーム概要  

### ジャンル  
回転寿司屋での調理シミュレーター

### ゲームルール  
お客さんの注文通りにお寿司作って提供する寿司屋シミュレーションゲームです。
マニュアルを読みながら時間内に注文をこなしていき、どんどん早くなる注文に焦りや
混乱を感じながら楽しんでもらうのがこのゲームの目的です。
また、最終的な売り上げをランキングとして競うこともできます。

### プラットフォーム  
Windows11 キーボード＆マウス操作

### Unityバージョン  
Unity 2022.3.24f1

## 担当ブログラム 
### こだわったスクリプト
* [注文処理](./UnityFiles/Scripts/GameMainScripts/OrderManager.cs)
* [マウスクリックで材料を持つ](./UnityFiles/Scripts/GameMainScripts/MouseClickPos.cs)
* [カメラ制御](./UnityFiles/Scripts/GameMainScripts/CameraControllor.cs)
  
### 担当スクリプト一覧
* [MainScene](./UnityFiles/Scripts/GameMainScripts)
* [TutorialScene](./UnityFiles/Scripts/TutorialScene)

### 制作での工夫
* 回転寿司チェーン店でバイトした経験から、それらの経験をゲームとして楽しく遊んでもらおうという気持ちで
このゲームの制作を行いました。
* 寿司ネタの種類を知らない人でも分かりやすいようにマニュアルでの説明やチュートリアルの追加を行いました。
* なるべく操作を分かりやすく簡単にするため、材料を持った時に奥行(z軸)は動かさなくてもいいように配置を徹底したり、
難しい操作を減らして、より注文に没頭できるように意識しました。
* ゲームが単調にならないように難易度を変更できるようにしたり、料理を間違えずに提供し続けると、より注文が早くなっていく
コンボシステムの追加を行い、ゲームが終わるまで飽きずに楽しめるような工夫をしました。


## 制作概要  
### メンバー（役割）  
* 新垣 大空（プログラマー）  
* 大城 一世（プログラマー）  
* 上地 妃咲（デザイナー）  
* 嘉手苅 陽凪（デザイナー）

### 制作期間
２か月

## スクリーンショット  
![タイトル画面](./ScreenShot/title.png)  
![プレイ画面](./ScreenShot/kitchen.png)  
![リザルト画面](./ScreenShot/result.png)


