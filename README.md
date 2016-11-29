# UnityApp
<b>The Chase (AR + VR) Android Exergame for Compsci 715</b>

</b>Contributions:</b> Everything was done as a group effort - we often were all around a computer at once working on something together.  The fact that one person committed it isn't necessarily evidence that it was entirely done by them alone.

<b>Andrew: AceMaester</b>
- Implemented zombies and scene audio
- Processed statistics

<b>James: Jarak-Jakar</b>
- Set up the animation of the zombies
- Wrote most of user study documents

<b>Rikki: ObsessedAnalytics</b>
- Made exergame promotional video
- Imported iOS plugin (no support for this yet)

<b>Zeeshan: zsar419</b>
- Created the scenes in Unity and most of the UI
- Implemented most of game logic

<b>Instructions:</b>
- Run the exergame (scene) from main menu (to prevent unexpected crashes)
- Exergame will pause upon loading Headset mode and Hendheld mode
- This is due to the android sensor plugin (which is compatible for android smartphones only)
- You can press play to continue

<b>Build to Android</b>
- To play this exergame on android you must download the required android packages in Build settings then press "Build and Run" to run the exergame on your Android smartphone 

  
<b>Controls (PC):</b>
- For debugging, you can increment player steps manually (as if person were to walk in real life) by pressing the "+steps" button
- Move player forward and/or backward using  up and down arrow keys
- Rotate player view left and right using left and right arrow keys

<b>Controls (Mobile - Android):</b>
- Walk in real world -> pedometer will detect these steps and update the game which will allow player to escape zombies
- Zombies will always spawn behind the player

<b>Notes:</b>
- The exergame was tested on a Samsung Galaxy s7 edge smartphone using the Google Cardboard headset (which has pass-through camera)
- The exergame only works with android platforms due to the inherent "Android Sensor Plugin" which is used to access native android pedometer data from the "Health App"
- This open soursce pluging also causes the game to crash at a 50% chance - this occurs in the "Pedometer.cs" script where the android sensor is being used
- <b>Extensible:</b> To extend exergame to other platforms, one would only have to change "Pedometer.cs" so that it can detect pedometer of the desired platform (iOS)
  - Rikki has imported the iOS pedometer plugin, however it is not being utilized in this exergame
  
<b>Promotional Video</b>
https://www.youtube.com/watch?v=1DLpfCOBIvM&index=7&list=PLftD6w6cBkQP_v2_yISo37ZkOeDefEzGr

