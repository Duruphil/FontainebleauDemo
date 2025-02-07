using System.Collections;
using UnityEngine;


public class fps : MonoBehaviour
{
  float deltaTime = 0.0f;

  GUIStyle style;
  Rect rect;
  float msec;
  float fpsframe;
  public float worstFps = 100f;
  string text;

  void Awake()
  {
    int w = Screen.width, h = Screen.height;

    rect = new Rect(0, 0, w, h * 4 / 100);

    style = new GUIStyle();
    style.alignment = TextAnchor.UpperLeft;
    style.fontSize = h * 4 / 100;
    style.normal.textColor = Color.cyan;

    
  }



  void Update()
  {
    deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
  }
 
  void OnGUI()//소스로 GUI 표시.
  {

    msec = deltaTime * 1000.0f;
    fpsframe = 1.0f / deltaTime;  //초당 프레임 - 1초에

    if (fpsframe < worstFps)  //새로운 최저 fps가 나왔다면 worstFps 바꿔줌.
      worstFps = fpsframe;
    text = msec.ToString ("F1") + "ms (" + fpsframe.ToString ("F1") + ") //worst : " + worstFps.ToString ("F1");
    GUI.Label(rect, text, style);
  }
} 
