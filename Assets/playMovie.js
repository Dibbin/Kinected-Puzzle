#pragma strict

var movTex : MovieTexture;

function Start ()
{
	renderer.material.mainTexture = movTex;
	movTex.Play();
}