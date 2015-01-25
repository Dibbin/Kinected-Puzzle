using UnityEngine;
using System.Collections;

public class LightFlickerPulse : MonoBehaviour {  // This name 'LightFlickerPulse' is a very convenient name, trust me!
	
	public enum UseType {Color, Intensity, Range, Off}  // The way you want to affect the light in a nice drop down menu!
	public UseType useType;  // The inspector view of the UseTypes!
	
	public enum WaveFunction {Noise, Sin, Tri, Sqr, Saw, SawInv}  // The type of function you want to use in a drop down menu!
	public WaveFunction waveFunction;  // The inspector view of the WaveFunctions!

	public bool noiseUpdatePF = true;  // Like a vsync for the rate of update, doesn't affect FPS!  **PF = Per Frame!
	public bool noiseSmoothFade;  // Use this if you slow down the rate a lot!
	public float noiseSmoothSpeed = 4;  // To smooth the noise function to what speed you like!
	
	public float timescaleMin = 0.1f;  // The capping range to stop the script at pause!
	
	public float coreBase = 0.8f;  // The strength of the light, changes from the useTypes!
	public float amplitude = 0.4f;  // The measurement of how intense the functions are going to work!
	public float phase;  // Acts like a seed and changes the lifetime of the script, doesn't do much unless you want to be specific!
	public float frequency = 0.5f;  // The measurement of time between the cycles, use this to vsync the noise rate too!
	
	public bool useOutsideLight;  // The option to enable or disable the use of an or many OutsideLight(s)
	public Light[] outsideLight;  // The array to hold all of the lights you want to affect
	
	private Color mainColor;  // Hidden variable to remember the single lights colour on the gameObject
	private float mainValueI;  // Hidden variable to remember the single lights intensity on the gameObject
	private float mainValueR;  // Hidden variable to remember the single lights range on the gameObject
	
	private Color[] mainOutColor = new Color[49];  // Hidden variables to remember the single light(s) colours on different gameObjects
	private float[] mainOutValueI = new float[49];  // Hidden variables to remember the single light(s) intensities on different gameObjects
	private float[] mainOutValueR = new float[49];  // Hidden variables to remember the single light(s) ranges on different gameObjects
	
	private Light lightS;  // Hidden variable on the single gameObjects light component!
	
	private bool error1;  // Hidden variable remembering if you had the error1 or not
	private bool error2;  // Hidden variable remembering if you had the error2 or not
	private bool assigned;  // Hidden variable to make sure you have assigned everything, almost an on/off switch to control if it works or not!
	
	private float x;  // Hidden variable used for the outcome calculation
	private float y;  // Hidden variable used for the outcome calculation
	private float z;  // Hidden variable used for the outcome calculation

	private float noiseD;  // Hidden variable keeping track of the delays timer to cap the noise WaveFunction
	private bool noiseGo;  // Hidden variable to ask it the noise function can re-randomise again
	
	public bool displayScriptErrors = true;
	
	
	void Update(){
		if(waveFunction == WaveFunction.Noise && !noiseUpdatePF){
			if(noiseD > frequency){
				noiseD = 0;
				noiseGo = true;
			}else{
				noiseD += Time.deltaTime;
				noiseGo = false;
			}
		}

		if(lightS == null && !useOutsideLight){
			lightS = gameObject.light;
		}
		if(!assigned){
			if(GetComponent<Light>() != null){
				mainColor = light.color;
				mainValueI = light.intensity;
				mainValueR = light.range;
				if(error1 && displayScriptErrors){
					Debug.Log ("FIXED ERROR REPORT: The missing Light component on the gameObject " + "'" +gameObject.name + "'" + " has been successfully re-established!", gameObject);
					error1 = false;
				}
				assigned = true;
			}else if(!error1 && displayScriptErrors && !useOutsideLight){
				Debug.LogError ("ERROR MISSING COMPONENT: The gameObject " + "'" +gameObject.name + "'" + " does not have a Light component attached to it. If you wish to use a Light from another gameObject, please select the boolean variable 'Use Outside Light' for this feature.", gameObject);
				error1 = true;
			}
			if(outsideLight.Length > 0 && outsideLight.Length < 50 && useOutsideLight){
				for(int i = 0; i < outsideLight.Length; i++){
					if(outsideLight[i] == null && displayScriptErrors && !error2){
						error2 = true;
						Debug.LogError ("ERROR EMPTY VARIABLE: The gameObject " + "'" +gameObject.name + "'" + " does not have a gameObject with a Light component attached to the variable 'Outside Light' index number " + "[ " + i + " ], to fix this either, assign a gameObject with a Light component in this space or, lower the size amount to fit your assigned lights. If you do not wish to use this feature, un-tick the boolean variable 'Use Outside Light.'", gameObject);
						Debug.LogError ("VARIABLE ADDITIONAL INFORMATION: This feature does not allow for fixing after the error has occurred due to strict limitations of the 'for' loop, you must replay with the Light component attached to the variable 'Outside Light' index number " + "[ " + i + " ], correctly.", gameObject);
					}else if(!error2){
						mainOutColor[i] = outsideLight[i].color;
						mainOutValueI[i] = outsideLight[i].intensity;
						mainOutValueR[i] = outsideLight[i].range;
					}
				}
				if(!error2){
					assigned = true;
				}
			}else if(!(outsideLight.Length < 50) && displayScriptErrors){
				Debug.LogError ("ERROR EXCEEDING ARRAY LIMITATIONS: Due to this script having to pre-load other array variables to gather all the Light components information a decision was made on how many arrays as max should be pre-defined to remove any performance issues. To fix this error you could modify this script to allocate more available spots located in the defining variables section or remove some or many Light components from the array list on the gameObject " + "'" +gameObject.name + "'. The defined Light components past the 48th index number will not work under this script.", gameObject);
			}
		}
		if(((!error1 && !useOutsideLight) || (!error2 && useOutsideLight)) && assigned && useType != UseType.Off && Time.timeScale > timescaleMin){
			CalWaveFunc();
			if(useOutsideLight){
				for(int i = 0; i < outsideLight.Length; i++){
					if(useType == UseType.Color){
						outsideLight[i].color = mainOutColor[i] * (z);
					}else if(useType == UseType.Intensity){
						outsideLight[i].intensity = mainOutValueI[i] * (z);
					}else if(useType == UseType.Range){
						outsideLight[i].range = mainOutValueR[i] * (z);
					}
					if(mainOutValueI[i] != outsideLight[i].intensity && (useType != UseType.Intensity)){
						outsideLight[i].intensity = mainOutValueI[i];
					}
					if(mainOutValueR[i] != outsideLight[i].range && (useType != UseType.Range)){
						outsideLight[i].range = mainOutValueR[i];
					}
					if(mainOutColor[i] != outsideLight[i].color && (useType != UseType.Color)){
						outsideLight[i].color = mainOutColor[i];
					}
				}
			}else{
				if(useType == UseType.Color){
					lightS.color = mainColor * (z);
				}else if(useType == UseType.Intensity){
					lightS.intensity = mainValueI * (z);
				}else if(useType == UseType.Range){
					lightS.range = mainValueR * (z);
				}
			}
		}  
		if(!useOutsideLight && !error1){
			if(mainValueI != lightS.intensity && (useType != UseType.Intensity)){
				lightS.intensity = mainValueI;
			}
			if(mainValueR != lightS.range && (useType != UseType.Range)){
				lightS.range = mainValueR;
			}
			if(mainColor != lightS.color && (useType != UseType.Color)){
				lightS.color = mainColor;
			}
		}
	}
	
	
	void CalWaveFunc(){
		
		x = (Time.time + phase)*frequency;
		x = x - Mathf.Floor(x);
		
		if(waveFunction == WaveFunction.Sin){
			y = Mathf.Sin(x*2*Mathf.PI);
		}else if(waveFunction == WaveFunction.Tri){
			if(x<0.5f){
				y=4.0f*x-1.0f;
			}else{
				y=-4.0f*x-3.0f;
			}
		}else if(waveFunction == WaveFunction.Sqr){
			if(x<0.5f){
				y=1.0f;
			}else{
				y=-1.0f;
			}
		}else if(waveFunction == WaveFunction.Saw){
			y=x;
		}else if(waveFunction == WaveFunction.SawInv){
			y=1.0f-x;
		}else if(waveFunction == WaveFunction.Noise){
			if(noiseGo || noiseUpdatePF){
				y= 1-(Random.value*2);
			}
		}
		if(noiseSmoothFade && waveFunction == WaveFunction.Noise){
			z = Mathf.Lerp(z, (y*amplitude)+coreBase, Time.deltaTime*noiseSmoothSpeed);
		}else{
			z = (y*amplitude)+coreBase;
		}
	}
}
/// This script is free to use and may be modified to your likings.
/// The team and I would be thankful to be credited for your use of this script.
/// Self distribution is not permitted with this script. 
/// Developed and distributed by Molten Metal Studios™.

