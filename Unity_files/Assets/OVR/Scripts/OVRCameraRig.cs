/************************************************************************************

Copyright   :   Copyright 2014 Oculus VR, LLC. All Rights reserved.

Licensed under the Oculus VR Rift SDK License Version 3.2 (the "License");
you may not use the Oculus VR Rift SDK except in compliance with the License,
which is provided at the time of installation or download, or which
otherwise accompanies this software in either electronic or hard copy form.

You may obtain a copy of the License at

http://www.oculusvr.com/licenses/LICENSE-3.2

Unless required by applicable law or agreed to in writing, the Oculus VR SDK
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

************************************************************************************/

//#define OVR_USE_PROJ_MATRIX

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A head-tracked stereoscopic virtual reality camera rig.
/// </summary>
[ExecuteInEditMode]
public class OVRCameraRig : MonoBehaviour
{
	/// <summary>
	/// The left eye camera.
	/// </summary>
	private Camera leftEyeCamera;
	/// <summary>
	/// The right eye camera.
	/// </summary>
	private Camera rightEyeCamera;
	
	/// <summary>
	/// Always coincides with the pose of the left eye.
	/// </summary>
	public Transform leftEyeAnchor { get; private set; }
	/// <summary>
	/// Always coincides with average of the left and right eye poses.
	/// </summary>
	public Transform centerEyeAnchor { get; private set; }
	/// <summary>
	/// Always coincides with the pose of the right eye.
	/// </summary>
	public Transform rightEyeAnchor { get; private set; }
	
	private bool needsCameraConfigure;
	
	//Set ovrvision Right-eye Gap
	public Vector3 ovrvisionRightEyeGap = new Vector3(0.5f, 0.0f, 0.0f);
	
	Matrix4x4 display = Matrix4x4.zero;
	GameObject[] AllPortals;
	
	#region Unity Messages
	private void Awake()
	{
		EnsureGameObjectIntegrity();
		
		if (!Application.isPlaying)
			return;
		
		needsCameraConfigure = true;
		AllPortals = GameObject.FindGameObjectsWithTag("PortalContainer");
	}
	
	private void Start()
	{
		EnsureGameObjectIntegrity();
		
		if (!Application.isPlaying)
			return;
		
		UpdateCameras();
		UpdateAnchors();


	}
	
	#if !UNITY_ANDROID || UNITY_EDITOR
	private void LateUpdate()
		#else
		private void Update()
			#endif
	{
		EnsureGameObjectIntegrity();
		
		if (!Application.isPlaying)
			return;
		
		UpdateCameras();
		UpdateAnchors();
	}
	
	#endregion
	
	private void UpdateAnchors()
	{
		OVRPose leftEye = OVRManager.display.GetEyePose(OVREye.Left);
		OVRPose rightEye = OVRManager.display.GetEyePose(OVREye.Right);

		leftEyeAnchor.localRotation = leftEye.orientation;
		centerEyeAnchor.localRotation = leftEye.orientation; // using left eye for now
		rightEyeAnchor.localRotation = rightEye.orientation;

		GameObject.Find ("PortalContainer2").transform.localRotation = leftEyeAnchor.localRotation;

		foreach(GameObject p in AllPortals)
		{
			p.transform.localRotation = leftEye.orientation;
		}

		//Debug.Log ("Portal orientation: "+GameObject.Find("Portal1").transform.localRotation.eulerAngles);
		/*
		// Original OVR positioning 
		leftEyeAnchor.localPosition = leftEye.position;
		centerEyeAnchor.localPosition = 0.5f * (leftEye.position + rightEye.position);
		rightEyeAnchor.localPosition = rightEye.position;
		
		// Original Ovrvision positioning (stuck camera)
		leftEyeAnchor.localPosition = new Vector3 (0.0f, 0.0f, 0.0f);
		centerEyeAnchor.localPosition = new Vector3 (0.0f, 0.0f, 0.0f);
		rightEyeAnchor.localPosition = ovrvisionRightEyeGap;
		 */

		/*
		leftEyeAnchor.localPosition.x = leftEye.position.x * 6.5f;
		leftEyeAnchor.localPosition.y = leftEye.position.y * 6.5f;
		leftEyeAnchor.localPosition.z = leftEye.position.z * 10.0f;
		leftEye.position = new Vector3 (leftEyeAnchor.localPosition.x, leftEyeAnchor.localPosition.y, leftEyeAnchor.localPosition.z);


        */

		//leftEyeAnchor.localPosition = new Vector3 (leftEye.position.x * 6.5f, leftEye.position.y * 6.5f, leftEye.position.z * 20.0f);
		//rightEyeAnchor.localPosition = new Vector3 (rightEye.position.x * 6.5f, rightEye.position.y * 6.5f, rightEye.position.z * 20.0f);

		/*
		rightEyeAnchor.localPosition.x = rightEye.position.x * 6.5f;
		rightEyeAnchor.localPosition.y = rightEye.position.y * 6.5f;
		rightEyeAnchor.localPosition.z = rightEye.position.z * 10.0f;
		rightEye.position = new Vector3 (rightEyeAnchor.localPosition.x, rightEyeAnchor.localPosition.y, rightEyeAnchor.localPosition.z);
         */

		//centerEyeAnchor.localPosition = 0.5f * (leftEye.position + rightEye.position) * 6.5f;

		leftEyeAnchor.localPosition = leftEye.position * 9.5f;
		rightEyeAnchor.localPosition = new Vector3 (rightEye.position.x, rightEye.position.y, rightEye.position.z) * 9.5f;
		centerEyeAnchor.localPosition = 0.5f * (leftEye.position + rightEye.position) * 9.5f;


		//rightEyeAnchor.localPosition = new Vector3 (rightEye.position.x*6.5f, rightEye.position.y*6.5f, rightEye.position.z*9.5f) *6.5f;
		//rightEyeAnchor.localPosition = new Vector3(rightEye.position.x+0.4f, rightEye.position.y, rightEye.position.z+0.1f)*10.0f;
		//rightEyeAnchor.localPosition = new Vector3(rightEye.position.x, rightEye.position.y, rightEye.position.z);

		/*Vector4 Lpos = new Vector4 (leftEyeAnchor.localPosition.x, leftEyeAnchor.localPosition.y, leftEyeAnchor.localPosition.z, 0);
		Vector4 Cpos = new Vector4 (centerEyeAnchor.localPosition.x, centerEyeAnchor.localPosition.y, centerEyeAnchor.localPosition.z, 0);
		Vector4 Rpos = new Vector4 (rightEyeAnchor.localPosition.x, rightEyeAnchor.localPosition.y, rightEyeAnchor.localPosition.z, 0);

		display.SetRow (0, Lpos);
		display.SetRow (1, Cpos);
		display.SetRow (2, Rpos);
		Debug.Log ("Eye anchor matrix: " + display);*/

	}
	
	private void UpdateCameras()
	{
		if (needsCameraConfigure)
		{
			leftEyeCamera = ConfigureCamera(OVREye.Left);
			rightEyeCamera = ConfigureCamera(OVREye.Right);
			
			#if !UNITY_ANDROID || UNITY_EDITOR
			
			#if OVR_USE_PROJ_MATRIX
			OVRManager.display.ForceSymmetricProj(false);
			#else
			OVRManager.display.ForceSymmetricProj(true);
			#endif
			
			needsCameraConfigure = false;
			#endif
		}
	}
	
	private void EnsureGameObjectIntegrity()
	{
		if (leftEyeAnchor == null)
			leftEyeAnchor = ConfigureEyeAnchor(OVREye.Left);
		if (centerEyeAnchor == null)
			centerEyeAnchor = ConfigureEyeAnchor(OVREye.Center);
		if (rightEyeAnchor == null)
			rightEyeAnchor = ConfigureEyeAnchor(OVREye.Right);
		
		if (leftEyeCamera == null)
		{
			leftEyeCamera = leftEyeAnchor.GetComponent<Camera>();
			if (leftEyeCamera == null)
			{
				leftEyeCamera = leftEyeAnchor.gameObject.AddComponent<Camera>();
			}
		}
		
		if (rightEyeCamera == null)
		{
			rightEyeCamera = rightEyeAnchor.GetComponent<Camera>();
			if (rightEyeCamera == null)
			{
				rightEyeCamera = rightEyeAnchor.gameObject.AddComponent<Camera>();
			}
		}
	}
	
	private Transform ConfigureEyeAnchor(OVREye eye)
	{
		string name = eye.ToString() + "EyeAnchor";
		Transform anchor = transform.Find(name);
		
		if (anchor == null)
		{
			string oldName = "Camera" + eye.ToString();
			anchor = transform.Find(oldName);
		}
		
		if (anchor == null)
			anchor = new GameObject(name).transform;
		
		anchor.parent = transform;
		anchor.localScale = Vector3.one;
		anchor.localPosition = Vector3.zero;
		anchor.localRotation = Quaternion.identity;
		
		return anchor;
	}
	
	private Camera ConfigureCamera(OVREye eye)
	{
		Transform anchor = (eye == OVREye.Left) ? leftEyeAnchor : rightEyeAnchor;
		Camera cam = anchor.GetComponent<Camera>();
		
		OVRDisplay.EyeRenderDesc eyeDesc = OVRManager.display.GetEyeRenderDesc(eye);
		
		cam.fieldOfView = eyeDesc.fov.y;
		cam.aspect = eyeDesc.resolution.x / eyeDesc.resolution.y;
		cam.rect = new Rect(0f, 0f, OVRManager.instance.virtualTextureScale, OVRManager.instance.virtualTextureScale);
		cam.targetTexture = OVRManager.display.GetEyeTexture(eye);
		
		// AA is documented to have no effect in deferred, but it causes black screens.
		if (cam.actualRenderingPath == RenderingPath.DeferredLighting)
			QualitySettings.antiAliasing = 0;
		
		#if !UNITY_ANDROID || UNITY_EDITOR
		#if OVR_USE_PROJ_MATRIX
		cam.projectionMatrix = OVRManager.display.GetProjection((int)eye, cam.nearClipPlane, cam.farClipPlane);
		#endif
		#endif
		
		return cam;
	}
}
