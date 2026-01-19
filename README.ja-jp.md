# Capreze

[View in English](./README.md)

ピクセル単位で正確なキャプチャを行うためのウィンドウ サイズ自動調整ツール

[![.github/workflows/trigger-on-main.yml](https://github.com/karamem0/capreze/actions/workflows/trigger-on-main.yml/badge.svg)](https://github.com/karamem0/capreze/actions/workflows/trigger-on-main.yml)
[![License](https://img.shields.io/github/license/karamem0/capreze.svg)](https://github.com/karamem0/capreze/blob/main/LICENSE)

## 機能

キャプチャ精度を高めるために、ウィンドウの表示サイズと実際の画像サイズとの差異 (オフセット) を自動検出し、指定どおりのサイズでキャプチャできるようにするツールです。マルチ モニター環境では、対象ウィンドウが表示されているモニターごとに補正を適用できます。

- ウィンドウと取得画像の境界差分 (オフセット) 自動検出
- マルチ モニター対応
- 指定ピクセル単位の正確なサイズ設定支援

## 詳細な仕組み

Capreze は、ウィンドウの見た目 (表示領域) と、実際にスクリーンショットとして取得されるピクセル領域とのずれ (以下「オフセット」) を自動で検出・補正し、指定したピクセルサイズで正確にキャプチャできるようにウィンドウサイズを調整します。

Windows 10 以降では Fluent UI の影 (ドロップシャドウ) やウィンドウ枠の描画があり、`Alt + PrintScreen` や Snipping Tool による切り取りでは影が含まれないことが多いため、取得画像が見た目よりも小さくなることがあります。また、ディスプレイの DPI スケーリングにより、影や枠の幅が変化し、必要なオフセットが異なります。

既知の補正値の目安を以下に示します。

|スケーリング|オフセット|
|--:|--:|
|100%|7px|
|125%|8px|
|150%|10px|
|200%|12px|

例えば、150% スケーリングで 800×600px の画像を取得したい場合、オフセットを考慮してウィンドウサイズを約 820×610px に設定します。

Capreze ではマルチ モニター環境でも各モニターに適した補正を適用します。

## インストール

### WinGet

コマンドラインでアプリをインストールできます。

```
winget install Capreze
```

### Vector

ページからインストーラーをダウンロードして手動でインストールしてください。

[ダウンロード](https://www.vector.co.jp/soft/winnt/art/se524149.html)

## スクリーンショット

![スクリーンショット](./assets/Screenshot.png)

## 依存関係

- [.NET 10.0](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Microsoft.ApplicationInsights.WorkerService](https://www.nuget.org/packages/Microsoft.ApplicationInsights.WorkerService/2.23.0) (2.23.0)
- [Microsoft.Extensions.Configuration.Json](https://www.nuget.org/packages/Microsoft.Extensions.Configuration.Json/10.0.1) (10.0.1)
- [Microsoft.Extensions.DependencyInjection](https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection/10.0.1) (10.0.1)
- [Microsoft.Extensions.Hosting](https://www.nuget.org/packages/Microsoft.Extensions.Hosting/10.0.1) (10.0.1)
- [Microsoft.Xaml.Behaviors.Wpf](https://www.nuget.org/packages/Microsoft.Xaml.Behaviors.Wpf/1.1.135) (1.1.135)
- [TinyMapper](https://www.nuget.org/packages/TinyMapper/3.0.3) (3.0.3)
