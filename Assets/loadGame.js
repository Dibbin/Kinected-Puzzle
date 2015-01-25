#pragma strict

var timeToWait = 10.0; //seconds

function Start () {

}

function Update () {
	timeToWait -= Time.deltaTime;
	if (timeToWait <= 0)
	{
		Application.LoadLevel ("mars-outside");
	}
}

