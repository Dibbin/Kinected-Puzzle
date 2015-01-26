#pragma strict
 
var TheSystem : Transform;
var Distance  : float;
var MaxDistance : float = 1000; 

var handOpenIntersect = false;

function Update() {
	//HandTracker.timeLeftToGrab -= Time.deltaTime;
	
  var allSliders = GameObject.FindGameObjectsWithTag("GrabTimer");
  var thisSlider = allSliders[0].GetComponent(UnityEngine.UI.Slider);
  
  thisSlider.value -= Time.deltaTime;
	
	if(thisSlider.value > 0){
	
	
 var hit : RaycastHit;
 var vectorCameraToObj = new Vector3(transform.position.x - TheSystem.position.x, transform.position.y - TheSystem.position.y, transform.position.z - TheSystem.position.z);

  if (Physics.Raycast (TheSystem.transform.position, TheSystem.transform.TransformDirection(vectorCameraToObj), hit)) {    
		//Debug.Log(hit.transform.gameObject.tag);
 		//Debug.Log("hit detected on object tag:" + hit.transform.gameObject.tag);
	 	
	    if(hit.transform.gameObject.tag == "testItem"){
//	    	handOpenIntersect = true;
//	    }else if(hit.transform.gameObject.tag == "HandClosed"){
	     	Distance = hit.distance;
	     	if (Distance < MaxDistance){
	     	//hide it
	         Destroy (GameObject.FindWithTag("testItem"));
			} else {
				handOpenIntersect = false;
	  		}
		}
	}
	
	}
	

}