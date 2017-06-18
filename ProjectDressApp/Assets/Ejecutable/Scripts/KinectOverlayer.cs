using UnityEngine;
using System.Collections;

public class KinectOverlayer : MonoBehaviour 
{
//	public Vector3 TopLeft;
//	public Vector3 TopRight;
//	public Vector3 BottomRight;
//	public Vector3 BottomLeft;

	public GUITexture backgroundImage;
	public KinectWrapper.NuiSkeletonPositionIndex TrackedJoint = KinectWrapper.NuiSkeletonPositionIndex.HipCenter;//centro de cadera

	public KinectWrapper.NuiSkeletonPositionIndex hombroDerecho = KinectWrapper.NuiSkeletonPositionIndex.ShoulderRight; //hombro derecho
	public KinectWrapper.NuiSkeletonPositionIndex hombroIzquierdo = KinectWrapper.NuiSkeletonPositionIndex.ShoulderLeft; //hombro izquierdo
	public KinectWrapper.NuiSkeletonPositionIndex hombroCentro = KinectWrapper.NuiSkeletonPositionIndex.ShoulderCenter; //hombro centro
	public KinectWrapper.NuiSkeletonPositionIndex caderaIzquierdo = KinectWrapper.NuiSkeletonPositionIndex.HipRight; //cadera izquierda
	public KinectWrapper.NuiSkeletonPositionIndex caderaDerecho = KinectWrapper.NuiSkeletonPositionIndex.HipRight; //cadera derecho


	public GameObject OverlayObject;
	public float smoothFactor = 5f;
	
	public GUIText debugText;

	private float distanceToCamera = 10f;

	private float val = 0.5f;
	void Start()
	{
		if(OverlayObject)
		{
			distanceToCamera = (OverlayObject.transform.position - Camera.main.transform.position).magnitude;
		}
	}
	
	void Update() 
	{
		KinectManager manager = KinectManager.Instance;
		
		if(manager && manager.IsInitialized())
		{
			//backgroundImage.renderer.material.mainTexture = manager.GetUsersClrTex();
			if(backgroundImage && (backgroundImage.texture == null))
			{
				backgroundImage.texture = manager.GetUsersClrTex();
			}
			
//			Vector3 vRight = BottomRight - BottomLeft;
//			Vector3 vUp = TopLeft - BottomLeft;
			
			int iJointIndex = (int)TrackedJoint;
			int ihombroDerecho = (int)hombroDerecho;
			int ihombroIzquierdo = (int)hombroIzquierdo;
			int ihombroCentro = (int)hombroCentro;
			int icaderaIzquierdo = (int)caderaIzquierdo;
			int icaderaDerecho = (int)caderaDerecho;
			
			if(manager.IsUserDetected())
			{
				uint userId = manager.GetPlayer1ID();
				
				if(manager.IsJointTracked(userId, iJointIndex))
				{
					Vector3 posJoint = manager.GetRawSkeletonJointPos(userId, iJointIndex);

					Vector3 posHombroDerecho = manager.GetRawSkeletonJointPos(userId, ihombroDerecho);
					Vector3 posHombroIzquierdo = manager.GetRawSkeletonJointPos(userId, ihombroIzquierdo);
					Vector3 posHombroCentro = manager.GetRawSkeletonJointPos(userId, ihombroCentro);
					Vector3 posCaderaIzquierdo = manager.GetRawSkeletonJointPos(userId, icaderaIzquierdo);
					Vector3 posCaderaDerecho = manager.GetRawSkeletonJointPos(userId, icaderaDerecho);

					Vector3 ejex = posHombroDerecho - posHombroIzquierdo;
					Vector3 ejey = posHombroDerecho - posCaderaDerecho;

					//Debug.Log("distancia en x" + ejex);
					//Debug.Log("distancia en y" + ejey);

					if(posJoint != Vector3.zero)
					{
						// 3d position to depth
						Vector2 posDepth = manager.GetDepthMapPosForJointPos(posJoint);
						
						// depth pos to color pos
						Vector2 posColor = manager.GetColorMapPosForDepthPos(posDepth);
						
						float scaleX = (float)posColor.x / KinectWrapper.Constants.ColorImageWidth;
						float scaleY = 1.0f - (float)posColor.y / KinectWrapper.Constants.ColorImageHeight;
						
//						Vector3 localPos = new Vector3(scaleX * 10f - 5f, 0f, scaleY * 10f - 5f); // 5f is 1/2 of 10f - size of the plane
//						Vector3 vPosOverlay = backgroundImage.transform.TransformPoint(localPos);
						//Vector3 vPosOverlay = BottomLeft + ((vRight * scaleX) + (vUp * scaleY));

						if(debugText)
						{
							debugText.GetComponent<GUIText>().text = "Tracked user ID: " + userId;  // new Vector2(scaleX, scaleY).ToString();
						}
						
						if(OverlayObject)
						{
							Vector3 vPosOverlay = Camera.main.ViewportToWorldPoint(new Vector3(scaleX, scaleY, distanceToCamera));
							vPosOverlay.y = vPosOverlay.y + 1f;
							OverlayObject.transform.position = Vector3.Lerp(OverlayObject.transform.position, vPosOverlay, smoothFactor * Time.deltaTime);

								

							OverlayObject.transform.localScale = new Vector3 (Mathf.Abs(ejex.x)*18, Mathf.Abs(ejey.y)*18, 2);
							//Debug.Log("scala" + OverlayObject.transform.localScale);
						}
					}
				}
				
			}
			
		}
	}
}
