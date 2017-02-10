Shader "Hedgehog Team/Glass"

{

 Properties
 {
    _EnvMap ("EnvMap", 2D) = "black" { TexGen SphereMap }
 }


 SubShader

 {
     SeparateSpecular On
     Tags {"Queue" = "Transparent" }
     Pass
     {
		Name "BASE"
		ZWrite on
		Blend One OneMinusSrcColor          // soft additive
		BindChannels
		{
		Bind "Vertex", vertex
		Bind "normal", normal
		}
		SetTexture [_EnvMap]
		{
			  combine texture
		}

      }

   }

}