#pragma strict

private var thisSlider: UnityEngine.UI.Slider;
var oxygenUsePerSecond = 3.33;
private var isGameOver = false;

function Start ()
{
  var allSliders = GameObject.FindGameObjectsWithTag("OxygenSlider");
  thisSlider = allSliders[0].GetComponent(UnityEngine.UI.Slider);
}

function Update()
{
	thisSlider.value -= Time.deltaTime * oxygenUsePerSecond;
	if (thisSlider.value == 0  && !isGameOver)
	{
		GameOver();
	}
	
}

function GameOver(){
  isGameOver = true;
//  var spriteRenderer = GetComponent(SpriteRenderer).color.a = 1.0;
 /* var gameObjects = GameObject.FindGameObjectsWithTag("GameOver");
  var gameOverSprite = gameObjects[0].GetComponent("GameOver");
  
  var hudCanvasObjects = GameObject.FindGameObjectsWithTag("HUDCanvas");
  var hudCanvas = gameObjects[0].GetComponent("HUDCanvas");
  
  //Debug.Log("asdf");*/
}
