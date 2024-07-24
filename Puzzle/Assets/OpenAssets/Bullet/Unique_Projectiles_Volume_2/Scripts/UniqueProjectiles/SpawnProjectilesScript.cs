using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnProjectilesScript : MonoBehaviour {

    public bool useTarget;
	public bool use2D;
	public bool cameraShake;
	public Text effectName;
	public RotateToMouseScript rotateToMouse;
	public GameObject firePoint;
	public GameObject cameras;
    public GameObject target;
    public List<GameObject> VFXs = new List<GameObject> ();

	private float timeToFire = 0f;
	private GameObject effectToSpawn;
	private List<Camera> camerasList = new List<Camera> ();
	private Camera singleCamera;

	void Start () {

		if (cameras.transform.childCount > 0) {
			for (int i = 0; i < cameras.transform.childCount; i++) {
				camerasList.Add (cameras.transform.GetChild (i).gameObject.GetComponent<Camera> ());
			}
			if(camerasList.Count == 0){
				Debug.Log ("Please assign one or more Cameras in inspector");
			}
		} else {
			singleCamera = cameras.GetComponent<Camera> ();
			if (singleCamera != null)
				camerasList.Add (singleCamera);
			else
				Debug.Log ("Please assign one or more Cameras in inspector");
		}
		
        if (useTarget && target != null)
        {
            var collider = target.GetComponent<BoxCollider>();
            if (!collider)
            {
                target.AddComponent<BoxCollider>();
            }
        }
		if(VFXs.Count>0)
			effectToSpawn = VFXs[0];
		else
			Debug.Log ("Please assign one or more VFXs in inspector");
		
		if (effectName != null) effectName.text = effectToSpawn.name;

		if (camerasList.Count > 0) {
			rotateToMouse.SetCamera (camerasList [camerasList.Count - 1]);
			if(use2D)
				rotateToMouse.Set2D (true);
			rotateToMouse.StartUpdateRay ();
		}
		else
			Debug.Log ("Please assign one or more Cameras in inspector");
    }

	void Update () {
		if (Input.GetMouseButton (0) && Time.time >= timeToFire) {
			timeToFire = Time.time + 1f / effectToSpawn.GetComponent<ProjectileMoveScript>().fireRate;
			SpawnVFX ();	
		}
	}

	public void SpawnVFX () {
		GameObject vfx;

		var cameraShakeScript = cameras.GetComponent<CameraShakeSimpleScript> ();

		if (cameraShake && cameraShakeScript != null)
			cameraShakeScript.ShakeCamera ();

		if (firePoint != null) {
			vfx = Instantiate (effectToSpawn, firePoint.transform.position, Quaternion.identity);
            if (!useTarget)
            {
                if (rotateToMouse != null)
                {
                    vfx.transform.localRotation = rotateToMouse.GetRotation();
                }
                else Debug.Log("No RotateToMouseScript found on firePoint.");
            }
            else
            {
                if (target != null)
                {                    
                    vfx.GetComponent<ProjectileMoveScript>().SetTarget(target, rotateToMouse);
                    rotateToMouse.RotateToMouse(vfx, target.transform.position);                    
                }
                else
                {
                    Destroy(vfx);
                    Debug.Log("No target assigned.");
                }
            }
		}
		else
			vfx = Instantiate (effectToSpawn);		
	}
}
