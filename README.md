# Ele Mag Harmony

## Requirement

MPU (board) : STM32F303K8T6 (Nucleo-32)

Driver : EleMagDriver v3.0

Band Instrument (Part) : 
 - Stepping Motor (Melody) 
 - Solenoid (Guitar)
 - Floppy Disc Drive (Base)
 - Relay (Drum)

Player : EleMagPlayer v3.0

Power : 12V 3A AC Adapter

Guitar：ElectricGuitar with Solenoid Mount

GuitarAmplifier：Basic

## Installation

### Hardware

EleMagDriver、Nucleoボード、電源、そしてバンドの構成楽器各種を準備します。

ギターにはアンプを接続し、ソレノイドマウントをつけてソレノイドを取り付けます。

ソレノイドマウントはHardware -> Guitar　内にstlファイルをアップロードしておりますので、そちらを活用してください。

EleMagDriverは基板のガーバーをアップロードしているので活用してください。v3.0ではオーディオビジュアライザー機能も搭載しています。

EleMag Driverの接続は基板に書かれている通りです。フロッピードライブ以外はxhのコネクタで接続する必要があります。

フロッピードライブは2.5mmピッチのQIのコネクタで電源を接続し、信号はFDDの接続に標準的な34ピンのMILコネクタを使用します。

### Software

Windows10以上（推奨）PCにEleMagPlayerをインストールします。

EleMagPlayerの基本動作はPlayerフォルダ内のreadmeを参照してください。

# Useage

上記の手順により、Playerでの再生が可能になります。Playerの再生方法に関してはPlayerのreadmeを参照してください。