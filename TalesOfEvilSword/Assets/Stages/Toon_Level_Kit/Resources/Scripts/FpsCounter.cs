/******************************************************************
                        Affichage du FPS

 Attacher le script à la camera principale ou un Game object empty
*******************************************************************/

using UnityEngine;
using System.Collections;

public class FpsCounter: MonoBehaviour {

   private int frame=0;
   private double frameStartTime;
   private float fps;
   private bool bShow;
   
	
   // Initialisation au lancement
   void Start () {
   
      frameStartTime = Time.realtimeSinceStartup;
      bShow = true;
   }
   
   // Calcul a chaque mise à jour
   void Update () {
      frame++;
      if (Time.realtimeSinceStartup - frameStartTime >1)
      {
         fps=frame;
         frame=0;
         frameStartTime = Time.realtimeSinceStartup;
      }
      
      // On / Off de l'affiche en fonction de la touche F1
      if (Input.GetKeyDown(KeyCode.F1 )){
         if (bShow){
            bShow=false;
         }
         else{
            bShow=true;
         }
      }


   }
   
   // Sur l'evenement GUI on affiche
   void OnGUI () {
      if (bShow){
         GUI.Label(new Rect(0,0,200,20), "FPS : " + fps.ToString("f2"));
		 

      }
   }
}