using UnityEngine;
using System.Collections;

public class HTLiquidSpriteSheet : MonoBehaviour {
	
	private Texture2D[] _diffuseTexture;
	
	public int uvAnimationTileX;
	public int uvAnimationTileY;
	public int spriteCount;
	public int framesPerSecond;
	public Vector2 textureSize;
	public Vector2 scrollSpeed;
	
	private float _startTime;
	private Vector2 currentOffset;

	
	
	// Use this for initialization
	void Start () {
	
		_diffuseTexture = new Texture2D[spriteCount];
		InitSpriteTexture();	
		_startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		
		GetComponent<Renderer>().material.SetTextureScale( "_MainTex",new Vector2(1,1));
		
		float index = (Time.time-_startTime) * framesPerSecond;
		index = index % (uvAnimationTileX * uvAnimationTileY);
		
		if (index==spriteCount){
			_startTime = Time.time;	
			index=0;
		}

		GetComponent<Renderer>().material.SetTextureScale( "_MainTex",textureSize);
		
		currentOffset += scrollSpeed * Time.deltaTime;
		GetComponent<Renderer>().material.SetTextureOffset( "_MainTex", currentOffset);
		GetComponent<Renderer>().material.SetTexture("_MainTex", _diffuseTexture[(int)index]);
		

	}
	
	public void InitSpriteTexture(){
		
		Texture2D origine = (Texture2D)GetComponent<Renderer>().material.GetTexture("_MainTex");	
		
		int xSpriteSize = GetComponent<Renderer>().material.mainTexture.width / uvAnimationTileX;
		int ySpriteSize = GetComponent<Renderer>().material.mainTexture.height / uvAnimationTileY;
		
		int i=0,x=0,y=uvAnimationTileY-1;
		while (y>=0 && i<spriteCount){
			while (x<uvAnimationTileX && i<spriteCount){
				
				Color[] couleur = origine.GetPixels( xSpriteSize*x,ySpriteSize*y, xSpriteSize,ySpriteSize);	
				_diffuseTexture[i] = new Texture2D(xSpriteSize,ySpriteSize);
				_diffuseTexture[i].SetPixels( couleur);
				_diffuseTexture[i].Apply();

				x++;
				i++;
			}
			x=0;
			y--;
		}
	}
}
