using UnityEngine;
using System;
using System.Collections;

public class StartScript : MonoBehaviour {

	public GameObject head;
	public GameObject body;
	public GameObject rightArm;
	public GameObject leftArm;
	public GameObject rightLeg;
	public GameObject leftLeg;

	private Texture2D customTexture;

	private static float originalColorValueRGB = 0.5f;
	private static float newColorValueRGB = 0.737f;

	private Color originalColor = new Color(originalColorValueRGB, originalColorValueRGB, originalColorValueRGB, 1.0F);

	private Vector3 cameraStartLocation;
	private Vector3 cameraStartRotation;

	private int currentModelShown = 0; //0 is 1.8 1 is 1.7 3 is alex
	public int myModelNumber;
    private int screenWidth = 512;
    private int screenHeight = 512;

	//private Vector3 theForwardDirection = Camera.main.transform.TransformDirection (Vector3.forward);

	public string url = "file:///edit.png";//"https://dl.dropboxusercontent.com/u/12694594/skins/Chief.png";
	// Use this for initialization
	void Start () {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        Screen.SetResolution(screenWidth, screenHeight, false);

        UpdateTextures ();
		newColorValueRGB = originalColorValueRGB;
		cameraStartLocation = Camera.main.transform.position;
		cameraStartRotation = Camera.main.transform.eulerAngles;

	}



	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.M)) {
			currentModelShown++;
			if (currentModelShown >= 3)
				currentModelShown = 0;
			body.transform.parent.gameObject.SetActive (currentModelShown == myModelNumber);

		}




		if (Input.GetKeyDown ("space"))
			UpdateTextures ();

		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			newColorValueRGB = newColorValueRGB - 0.03f;
			Color newColor = new Color(newColorValueRGB, newColorValueRGB, newColorValueRGB, 1.0F);
			Camera.main.backgroundColor = newColor;
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			newColorValueRGB = originalColorValueRGB;
			Camera.main.backgroundColor = originalColor;
		}
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			newColorValueRGB = newColorValueRGB + 0.03f;
			Color newColor = new Color(newColorValueRGB, newColorValueRGB, newColorValueRGB, 1.0F);
			Camera.main.backgroundColor = newColor;
		}
		if (Input.GetKeyDown (KeyCode.Alpha4)) {
			newColorValueRGB = newColorValueRGB + 0.03f;
			Color newColor = new Color(0.3058f, 0.7176f, 0.6352f, 1.0F);
			Camera.main.backgroundColor = newColor;
		}



        if (Input.GetKeyDown (KeyCode.F2))
        {
            DateTime date = new DateTime();
            date = DateTime.UtcNow;
            ScreenCapture.CaptureScreenshot("..\\Screenshot" + date.ToFileTimeUtc() + ".png");
        }


        //Change Fov
        /*
		if (Input.GetKey (KeyCode.Alpha4)) {
			Camera.main.fieldOfView = Camera.main.fieldOfView + 2;
			Camera.main.transform.Translate(Camera.main.transform.forward * 0.05f,Space.World);
		}
		if (Input.GetKeyDown (KeyCode.Alpha5)) {
			Camera.main.fieldOfView = originalFov;
		}
		if (Input.GetKey (KeyCode.Alpha6)) {
			Camera.main.fieldOfView = Camera.main.fieldOfView - 2;
			Camera.main.transform.Translate(Camera.main.transform.forward * -0.05f,Space.World);
		}
		*/

        if (Input.GetMouseButtonDown (2)) {
			Camera.main.transform.position = cameraStartLocation;
			Camera.main.transform.eulerAngles = cameraStartRotation;
		}

		if(Input.GetAxis("Mouse ScrollWheel") > 0)
			Camera.main.transform.Translate(Camera.main.transform.forward * 0.35f,Space.World);
		if(Input.GetAxis("Mouse ScrollWheel") < 0)
			Camera.main.transform.Translate(Camera.main.transform.forward * -0.35f,Space.World);


	}

	IEnumerator LoadTexture(GameObject bodyPart) {

		Renderer renderer = bodyPart.GetComponent<Renderer> ();
		
		// Create a texture in DXT1 format
		//renderer.material.mainTexture = new Texture2D(4, 4, TextureFormat.DXT1, false);
		
		// Start a download of the given URL
		WWW www = new WWW(url);
		
		// wait until the download is done
		yield return www;

        Texture2D test = new Texture2D(www.texture.width, www.texture.height, www.texture.format, false);
        www.LoadImageIntoTexture(test);

        // assign the downloaded image to the main texture of the object
        renderer.sharedMaterial.mainTexture = test; 
		renderer.sharedMaterial.mainTexture.filterMode = FilterMode.Point;
	}

	void UpdateTextures(){
		StartCoroutine (LoadTexture (head));
		StartCoroutine (LoadTexture (body));
		StartCoroutine (LoadTexture (rightArm));
		StartCoroutine (LoadTexture (leftArm));
		StartCoroutine (LoadTexture (rightLeg));
		StartCoroutine (LoadTexture (leftLeg));
	}

}
