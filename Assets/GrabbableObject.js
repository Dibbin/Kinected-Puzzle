#pragma strict
 
var TheSystem : Transform;
var Distance  : float;
var MaxDistance : float = 100;  

function Update() {

 var hit : RaycastHit;
 if (Physics.Raycast (TheSystem.transform.position, TheSystem.transform.TransformDirection(Vector3.forward), hit)) {    
    
    if(hit.transform.gameObject.tag == "Sphere"){
 
     Distance = hit.distance;
     if (Distance < MaxDistance){
     
         if (Input.GetKeyDown(KeyCode.E)) {
            // show
         	renderer.enabled = true;
         	Destroy (GameObject.FindWithTag("sword2"));
         } 

        if (Input.GetKeyDown(KeyCode.Backspace)) {
        	// hide
        	renderer.enabled = false;
     	}    
	}
}
}
}