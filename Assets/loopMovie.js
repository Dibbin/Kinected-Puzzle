#pragma strict

var movTex : MovieTexture;
var loop : boolean;

function Start ()
{
	renderer.material.mainTexture = movTex;
	movTex.Play();
	movTex.loop = true;
}