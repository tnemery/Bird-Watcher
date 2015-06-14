public class Screenshots : MonoBehaviour {
 private string fileName = "";
 private string path = "./Screenshots/"; //save screenshot here
 private string locateSS = "./BirdSong_Data/"; //loaction of screenshot
 private bool inPlayer = true; //set this to False before building a standalone
 public Texture2D[] pictures = new Texture2D[20];
 private int count = 0;
 private Texture2D _capture;
 private Vector2 _size;
 private bool scriptEnabled;
 
 void Awake(){
  if(inPlayer == true){
   locateSS = "./";
  }
  _size = new Vector2(Screen.width,Screen.height);
  _capture = new Texture2D((int)_size.x,(int)_size.y,TextureFormat.RGB24,false);
 }


 void LateUpdate() {
  if (Input.GetKeyDown (KeyCode.Insert)) { //key to take the picture
   StartCoroutine (CaptureThumbnail (_size));
   
  }
 }
 
 IEnumerator CaptureThumbnail (Vector2 size)
 {
  // creating a Texture to Render to, the size of the thumbnail
  RenderTexture myRenderTexture = new RenderTexture((int)size.x, (int)size.y, 4000);
  myRenderTexture.Create();

  
  
  // Setting the Texture to be the Active Texture to read from
  RenderTexture.active = myRenderTexture;
  
  // Creating a new Texture2D
  Texture2D myTexture2D = new Texture2D ((int)size.x, (int)size.y, TextureFormat.RGB24, false);

  //Raycast stuff
  Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
  RaycastHit hit;
  if (Physics.SphereCast(ray,1f, out hit,4000,1 << 11)){ //bird layer is 11, bitwise shift 1 << 11 ignores all layers but the bird layer
   print (hit.transform.tag);
  }

  yield return new WaitForEndOfFrame();
  
  myTexture2D.ReadPixels(new Rect(0, 0, myRenderTexture.width, myRenderTexture.height), 0, 0);
  myTexture2D.Apply();
  
  RenderTexture.active = null;
  myRenderTexture.Release();

  
  _capture = myTexture2D;
  pictures[count] = _capture;
  count++;
 }
}