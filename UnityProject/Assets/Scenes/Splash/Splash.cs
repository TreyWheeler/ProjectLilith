using UnityEngine;
using System.Collections;

public class Splash : MonoBehaviour
{
    MovieTexture movieTexture;
 
 
    // Use this for initialization
    void Start()
    {
        GUITexture guiTexture = GetComponent<GUITexture>();        
        movieTexture = guiTexture.texture as MovieTexture;        
        audio.clip = movieTexture.audioClip;
        
        guiTexture.pixelInset = new Rect(-Screen.width / 2, -Screen.height / 2, Screen.width, Screen.height);
        
       /* guiTexture.pixelInset.x = -Screen.width / 2;
        guiTexture.pixelInset.y = -Screen.height / 2;
        
        guiTexture.pixelInset.width = Screen.width;
        guiTexture.pixelInset.height = Screen.height;*/
    }
 
    // Update is called once per frame
    void Update()
    {    
        if(Input.GetKeyUp(KeyCode.Space))
        {      
            if(movieTexture.isPlaying)
            {
                movieTexture.Pause();
                audio.Pause();
            }
            else
            {
               
                movieTexture.Play(); 
                audio.Play();
            }
        }
    }
}
