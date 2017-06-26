using UnityEngine;
using System.Collections;

public class Parallaxing : MonoBehaviour {

    public Transform[] backgrounds;
    private float[] parralaxScales;
    public float smoothing = 1f;

    private Transform cam;
    private Vector3 previousCampos;

    private Camera thisCamera;
    void Awake() {


        cam = Camera.main.transform;


    }
	// Use this for initialization
	void Start () {
        previousCampos = cam.position;

        parralaxScales = new float[backgrounds.Length];

        for (int i = 0; i < backgrounds.Length; i++) {
            parralaxScales[i] = backgrounds[i].position.z * -1;

        }

    }
	
	// Update is called once per frame
	void FixedUpdate() {
        if (cam == null) {
            cam = Camera.main.transform;
            previousCampos = cam.position;
        }


        for (int i = 0; i < backgrounds.Length; i++) {
            
            float parallax = (previousCampos.x - cam.position.x) * parralaxScales[i];
                float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);

            
        }

        previousCampos = cam.position;
	}
}
