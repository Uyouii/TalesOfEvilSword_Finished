// HTSpriteSheet v2.0 (Aout 2012)
// HTSpriteSheet.cs library is copyright (c) of Hedgehog Team
// Please send feedback or bug reports to the.hedgehog.team@gmail.com

// Release note :
// V2.0
// - Add color variation
// - Add Rotation variation
// - Add life parameter

using UnityEngine;
using System.Collections;

/// <summary>
/// HTSpriteSheet allows the creation of a particle and play an animated sprite from spritesheet.
/// </summary>
public class HTSpriteSheet : MonoBehaviour {
	
	#region enumeration
	/// <summary>
	/// The rendering mode for particles..
	/// </summary>
	public enum CameraFacingMode{ 
		/// <summary>
		/// Render the particles as billboards facing the camera with tag "MainCamera". (Default)
		/// </summary>
		BillBoard, 
		/// <summary>
		/// Render the particles as billboards always facing up along the y-Axis.
		/// </summary>
		Horizontal,
		/// <summary>
		/// Render the particles as billboards always facing up along the X-Axis.
		/// </summary>
		Vertical,
		/// <summary>
		/// The particle never facinc up the camera.
		/// </summary>
		Never 
	};
	#endregion
	
	#region public properties
	
	/// <summary>
	/// The sprite sheet material.
	/// </summary>
	public Material spriteSheetMaterial;	
	/// <summary>
	/// The number of sprtie on the spritesheet.
	/// </summary>
	public int spriteCount;
	/// <summary>
	/// The uv animation tile x.
	/// </summary>
	public int uvAnimationTileX;
	/// <summary>
	/// The uv animation tile y.
	/// </summary>
	public int uvAnimationTileY;
	/// <summary>
	/// The number of images per second to play animation
	/// </summary>
	public int framesPerSecond;
	/// <summary>
	/// The initial size of the explosion
	/// </summary>
	public Vector3 sizeStart = new Vector3(1,1,1);
	/// <summary>
	/// The size end.
	/// </summary>
	public Vector3 sizeEnd = new Vector3(1,1,1);
	/// <summary>
	/// Applied a rondom rotation on z-Axis.
	/// </summary>
	public bool randomRotation;
	/// <summary>
	/// The rotation start.
	/// </summary>
	public float rotationStart;
	/// <summary>
	/// The rotation end.
	/// </summary>
	public float rotationEnd;
	/// <summary>
	/// The is one shot animation.
	/// </summary>
	public bool isOneShot=true;
	/// <summary>
	/// The life.
	/// </summary>
	public float life=0;
	/// <summary>
	/// The billboarding mode
	/// </summary>
	public CameraFacingMode billboarding;  // Bilboardin mode
	/// <summary>
	/// The add light effect.
	/// </summary>
	public bool addLightEffect=false;
	/// <summary>
	/// The light range.
	/// </summary>
	public float lightRange;
	/// <summary>
	/// The color of the light.
	/// </summary>
	public Color lightColor;
	/// <summary>
	/// The light fade speed.
	/// </summary>
	public float lightFadeSpeed=1;
	/// <summary>
	/// The add color effect.
	/// </summary>
	public bool addColorEffect;
	/// <summary>
	/// The color start.
	/// </summary>
	public Color colorStart = new Color(1f,1f,1f,1f);
	/// <summary>
	/// The color end.
	/// </summary>
	public Color colorEnd = new Color(1f,1f,1f,1f);
	/// <summary>
	/// The fold out.
	/// </summary>
	public bool foldOut=false;
	
	// For inspector save
	public Vector3 offset;
	public float waittingTime;
	public bool copy=false;
	#endregion
	
	#region private properties
	/// <summary>
	/// The material with the sprite speed.
	/// </summary>
	//private Material mat;
	/// <summary>
	/// The mesh.
	/// </summary>
	private Mesh mesh;
	/// <summary>
	/// The mesh render.
	/// </summary>
	private MeshRenderer meshRender;
	/// <summary>
	/// The audio source.
	/// </summary>
	private AudioSource soundEffect;
	/// <summary>
	/// The start time of the explosion
	/// </summary>
	private float startTime;
	/// <summary>
	/// The main camera.
	/// </summary>
	private Transform mainCamTransform;
	/// <summary>
	/// The effect end.
	/// </summary>
	private bool effectEnd=false;
	/// <summary>
	/// The random Z angle.
	/// </summary>
	private float randomZAngle;
	/// <summary>
	/// The color step.
	/// </summary>
	private Color colorStep;
	/// <summary>
	/// The current color.
	/// </summary>
	private Color currentColor;
	/// <summary>
	/// The size step.
	/// </summary>
	private Vector3 sizeStep;
	/// <summary>
	/// The size of the current.
	/// </summary>
	private Vector3 currentSize;
	/// <summary>
	/// The current rotation.
	/// </summary>
	private float currentRotation;
	/// <summary>
	/// The rotation step.
	/// </summary>
	private float rotationStep;
	/// <summary>
	/// The list start.
	/// </summary>
	private float lifeStart;
	/// <summary>
	/// Transform cahce.
	/// </summary>
	private Transform myTransform;
	#endregion
	
	#region MonoBehaviour methods
	
	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake(){
		
		// Creation of the particle
		CreateParticle();
		
		// We search the main camera
		mainCamTransform = Camera.main.transform;
		
		// do we have sound effect ?
		soundEffect = GetComponent<AudioSource>();
		
		// Add light
		if (addLightEffect){
			gameObject.AddComponent<Light>();
			GetComponent<Light>().color = lightColor;
			GetComponent<Light>().range = lightRange;
		}
		
		GetComponent<Renderer>().enabled = false;
		

	}
	
	// Use this for initialization
	void Start () {
	
		InitSpriteSheet();
	}
	
	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update () {
		
		bool end=false;
		
		Camera_BillboardingMode();
		
		// Calculate index
		
    	float index = (Time.time-startTime) * framesPerSecond;
		

		if (!isOneShot && life>0 && (Time.time -lifeStart)> life){
			effectEnd=true;
		}
		
		if ((index<=spriteCount || !isOneShot ) && !effectEnd ){

		   		
			if (index >= spriteCount){
				startTime = Time.time;	
				index=0;
				if (addColorEffect){
					currentColor = colorStart;
					meshRender.material.SetColor("_Color", currentColor);
				}
				currentSize = sizeStart;
				myTransform.localScale = currentSize;
				
				if (randomRotation){
					currentRotation = Random.Range(-180.0f,180.0f);
				}
				else{
					currentRotation = rotationStart;
				}
			}
			// repeat when exhausting all frames
		    index = index % (uvAnimationTileX * uvAnimationTileY);
			
			
		    // Size of every tile
		    Vector2 size = new Vector2 (1.0f / uvAnimationTileX, 1.0f / uvAnimationTileY);
		   
		    // split into horizontal and vertical index
		    float uIndex = Mathf.Floor(index % uvAnimationTileX);
		    float vIndex = Mathf.Floor(index / uvAnimationTileX);
		
		    // build offset
		    Vector2 offset = new Vector2 (uIndex * size.x , 1.0f - size.y - vIndex * size.y);

			GetComponent<Renderer>().material.SetTextureOffset ("_MainTex", offset);
			GetComponent<Renderer>().material.SetTextureScale ("_MainTex", size);

			GetComponent<Renderer>().enabled = true;
		}			
		else{
	 		effectEnd = true;
			GetComponent<Renderer>().enabled = false;
			end = true;		

			if (soundEffect){
				if (soundEffect.isPlaying){
					end = false;
				}
			}
		
			if (addLightEffect && end){
				if (GetComponent<Light>().intensity>0){
					end = false;
				}
			}
			
			if (end){
				Destroy(gameObject);	
 			}
		}
		
		// Size
		if (sizeStart != sizeEnd){
	    	myTransform.localScale += sizeStep * Time.deltaTime ;
		}
		   
		
		// Light effect
	 	if (addLightEffect && lightFadeSpeed!=0){
			GetComponent<Light>().intensity -= lightFadeSpeed*Time.deltaTime;
		}
		
		// Color Effect
		if (addColorEffect){
			currentColor = new Color(currentColor.r + colorStep.r * Time.deltaTime,currentColor.g + colorStep.g* Time.deltaTime,currentColor.b + colorStep.b* Time.deltaTime , currentColor.a + colorStep. a*Time.deltaTime);
			meshRender.material.SetColor("_TintColor", currentColor);
		}
	}
	
	#endregion
	
	#region private methods
	
	/// <summary>
	/// Creates the particle.
	/// </summary>
	void CreateParticle(){
		
		mesh = gameObject.AddComponent<MeshFilter>().mesh; 
		meshRender = gameObject.AddComponent<MeshRenderer>(); 		
		
		mesh.vertices = new Vector3[] { new Vector3(-0.5f,-0.5f,0f),new Vector3(-0.5f,0.5f,0f), new Vector3(0.5f,0.5f,0f), new Vector3(0.5f,-0.5f,0f) };
		mesh.triangles = new int[] {0,1,2, 2,3,0 };
		mesh.uv = new Vector2[] { new Vector2(1f,0f),  new Vector2 (1f, 1f),  new Vector2 (0f, 1f), new Vector2 (0f, 0f)};

		meshRender.castShadows = false;
		meshRender.receiveShadows = false;
		mesh.RecalculateNormals();		
		
		GetComponent<Renderer>().material= spriteSheetMaterial;
	}
	
	/// <summary>
	/// Camera_s the billboarding mode.
	/// </summary>
	void Camera_BillboardingMode(){
		
		Vector3 lookAtVector =   mainCamTransform.position-myTransform.position ;
		
		switch (billboarding){
			case CameraFacingMode.BillBoard:
				myTransform.LookAt(  mainCamTransform.position - lookAtVector); 
				myTransform.LookAt(  mainCamTransform.position ); 
				break;
			case CameraFacingMode.Horizontal:
				lookAtVector.x = lookAtVector.z =0 ;
				myTransform.LookAt(mainCamTransform.position - lookAtVector);
				break;
			case CameraFacingMode.Vertical:
				lookAtVector.y=lookAtVector.z =0;
				myTransform.LookAt(mainCamTransform.position - lookAtVector);
				break;
		}
		
		if (rotationStart!=rotationEnd){
			currentRotation+=rotationStep*Time.deltaTime;
		}
		
		//myTransform.eulerAngles = new Vector3(myTransform.eulerAngles.x,myTransform.eulerAngles.y,currentRotation);
	}
	
	#endregion
	
	#region public methos
	/// <summary>
	/// Inits the sprite sheet.
	/// </summary>
	public void InitSpriteSheet(){
		startTime = Time.time;
		lifeStart = Time.time;
		myTransform = transform;
				
		// time divider
		float divider = (float)spriteCount/(float)framesPerSecond;
			
		// size
		sizeStep = new Vector3( (sizeEnd.x - sizeStart.x)/divider, (sizeEnd.y - sizeStart.y)/divider,(sizeEnd.z - sizeStart.z)/divider);
		currentSize = sizeStart;
		myTransform.localScale = currentSize;
		
		//rotation
		rotationStep = (rotationEnd-rotationStart)/divider;
		// Random start rotation
		if (randomRotation){
			currentRotation = Random.Range(-180.0f,180.0f);
		}
		else{
			currentRotation = rotationStart;
		}
		
		// Add color effect
		if (addColorEffect){
			colorStep = new Color( (colorEnd.r - colorStart.r)/divider,(colorEnd.g - colorStart.g)/divider,(colorEnd.b - colorStart.b)/divider, (colorEnd.a - colorStart.a)/divider);
			currentColor = colorStart;
			meshRender.material.SetColor("_TintColor", currentColor);
		}		
	}
	#endregion
}
