#pragma strict
 
var TheSystem : Transform;
var Distance  : float;
var MaxDistance : float = 1000; 
var helmetGameObj: UnityEngine.GameObject;
var o2TankGameObj: UnityEngine.GameObject;
var airTankGameObj: UnityEngine.GameObject;
var batteryGameObj: UnityEngine.GameObject;
var flashlightGameObj: UnityEngine.GameObject;
var glovesGameObj: UnityEngine.GameObject;
var flashlightOnGameObj: UnityEngine.GameObject;
var spaceSuitGameObj: UnityEngine.GameObject;

var handOpenIntersect = false;

var numFlashlightItems = 0;

var numSpaceSuitItems = 0;

var keyItemsFound = 0;

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
			 	
			    if(hit.transform.gameObject.tag == "grabbable"){
		 		    Debug.Log("hit detected on object tag:" + hit.transform.gameObject.tag);
					//	    	handOpenIntersect = true;
					//	    }else if(hit.transform.gameObject.tag == "HandClosed"){
			     	Distance = hit.distance;
			     	if (Distance < MaxDistance){
			     	
			     	//determine what we just hit, and update the ui accordingly;
			     	  switch(hit.transform.gameObject.name){
			     	  	case 'spaceHelmet': 
		 		         helmetGameObj.transform.position.y += 110.0f;
						 numSpaceSuitItems++;
			     	  	break;
			     	  	case 'o2Tank': 
		 		         o2TankGameObj.transform.position.y += 110.0f;
		 		         numSpaceSuitItems++;
			     	  	break;
			     	  	case 'gloves': 
		 		         glovesGameObj.transform.position.y += 110.0f;
		 		         numSpaceSuitItems++;
			     	  	break;
			     	  	case 'flashLight': 
		 		         flashlightGameObj.transform.position.y += 110.0f;
		 		         numFlashlightItems++;
			     	  	break;
			     	  	case 'airTank': 
		 		         airTankGameObj.transform.position.y += 110.0f;
		 		         
		 		         
						var oxySliders = GameObject.FindGameObjectsWithTag("OxygenSlider");
						var oxySlider = oxySliders[0].GetComponent(UnityEngine.UI.Slider);
						
						oxySlider.value += 50;
		 		         
		 		         
			     	  	break;
			     	  	case 'battery': 
		 		         batteryGameObj.transform.position.y += 110.0f;
		 		         numFlashlightItems++;
			     	  	break;
			     	  }
			     	
			     		if(numFlashlightItems == 2)
			     		{
			     			flashlightGameObj.transform.position.y -= 110.0f;
			     			batteryGameObj.transform.position.y -= 110.0f;	
			     			flashlightOnGameObj.transform.position.y += 110.0f;
			     			numFlashlightItems = 0;
			     			keyItemsFound++;
			     		}
			     		if(numSpaceSuitItems == 3)
			     		{
			     			helmetGameObj.transform.position.y -= 110.0f;
			     			glovesGameObj.transform.position.y -= 110.0f;	
			     			o2TankGameObj.transform.position.y -= 110.0f;
			     			spaceSuitGameObj.transform.position.y += 110.0f;
			     			numSpaceSuitItems = 0;
			     			keyItemsFound++;
			     		}
			     		
			     		if(keyItemsFound == 2){
			     		  
			     		  keyItemsFound = 0;
							Application.LoadLevel ("win");
			     		  
			     		}
			     	
			     	
			     	//hide it
			         //Destroy (GameObject.FindWithTag("testItem"));
			         Destroy(hit.transform.gameObject);
					} else {
						handOpenIntersect = false;
			  		}
				}
			}
	
	}
	

}