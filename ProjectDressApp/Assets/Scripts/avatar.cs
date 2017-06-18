using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;
using System.Text; 


[RequireComponent(typeof(Animator))]
public class avatar : MonoBehaviour
{	
	// Bool que tiene los caracteres (mirando al jugador) las acciones se vuelven a reflejar. Predeterminado false.
	public bool mirroredMovement = false;

	// Bool que determina si el avatar se permite mover en la dirección vertical.
	public bool verticalMovement = false;

	// Valor en el cual el avatar se moverá a través de la escena. La velocidad multiplica la velocidad de movimiento (.001f, es decir, dividiendo por 1000, el framerate de la unidad).
	protected int moveRate = 1;

	// Slerp factor liso
	public float smoothFactor = 5f;

	// Si el nodo de desplazamiento debe reposicionarse a las coordenadas del usuario, según lo informado por el sensor o no.
	public bool offsetRelativeToSensor = false;


	// El nodo raíz del cuerpo
	protected Transform bodyRoot;

	// Una variable requerida si desea girar el modelo en el espacio.
	protected GameObject offsetNode;

	// Variable para contener todos los huesos. Se inicializará el mismo tamaño que initialRotations.
	protected Transform[] bones;

	// Rotaciones de los huesos cuando comienza el seguimiento de Kinect.
	protected Quaternion[] initialRotations;
	protected Quaternion[] initialLocalRotations;

	// Posición inicial y rotación de la transformada
	protected Vector3 initialPosition;
	protected Quaternion initialRotation;

	// Variables de desplazamiento de calibración para la posición de carácter.
	protected bool offsetCalibrated = false;
	protected float xOffset, yOffset, zOffset;

	// instancia privada de KinectManager
	protected KinectManager kinectManager;


	// transforma el almacenamiento en caché proporciona un aumento de rendimiento desde que Unity llama a GetComponent <Transform> () cada vez que llama a transformar
	private Transform _transformCache;
	public new Transform transform
	{
		get
		{
			if (!_transformCache) 
				_transformCache = base.transform;

			return _transformCache;
		}
	}

	public void Awake()
	{	
		// comprobar si hay doble inicio
		if(bones != null)
			return;

		// inits el conjunto de huesos
		bones = new Transform[22];

		// inits el conjunto de huesos
		initialRotations = new Quaternion[bones.Length];
		initialLocalRotations = new Quaternion[bones.Length];

		// Asigna los huesos a los puntos que las pistas de Kinect
		MapBones();

		// Obtener rotaciones de hueso iniciales
		GetInitialRotations();
	}

	// Actualiza el avatar de cada trama.
	public void UpdateAvatar(uint UserID)
	{	
		if(!transform.gameObject.activeInHierarchy) 
			return;

		// Obtener la instancia de KinectManager
		if(kinectManager == null)
		{
			kinectManager = KinectManager.Instance;
		}

		// mueve el avatar a su posición Kinect
		MoveAvatar(UserID);

		for (var boneIndex = 0; boneIndex < bones.Length; boneIndex++)
		{
			if (!bones[boneIndex]) 
				continue;

			if(boneIndex2JointMap.ContainsKey(boneIndex))
			{
				KinectWrapper.NuiSkeletonPositionIndex joint = !mirroredMovement ? boneIndex2JointMap[boneIndex] : boneIndex2MirrorJointMap[boneIndex];
				TransformBone(UserID, joint, boneIndex, !mirroredMovement);
			}
			else if(specIndex2JointMap.ContainsKey(boneIndex))
			{
				// huesos especiales (clavículas)
				List<KinectWrapper.NuiSkeletonPositionIndex> alJoints = !mirroredMovement ? specIndex2JointMap[boneIndex] : specIndex2MirrorJointMap[boneIndex];

				if(alJoints.Count >= 2)
				{
					//Vector3 baseDir = alJoints[0].ToString().EndsWith("Left") ? Vector3.left : Vector3.right;
					//TransformSpecialBone(UserID, alJoints[0], alJoints[1], boneIndex, baseDir, !mirroredMovement);
				}
			}
		}
	}

	// Establecen los huesos en sus posiciones y rotaciones iniciales
	public void ResetToInitialPosition()
	{	
		if(bones == null)
			return;

		if(offsetNode != null)
		{
			offsetNode.transform.rotation = Quaternion.identity;
		}
		else
		{
			transform.rotation = Quaternion.identity;
		}

		// Para cada hueso que se definió, vuelva a la posición inicial.
		for (int i = 0; i < bones.Length; i++)
		{
			if (bones[i] != null)
			{
				bones[i].rotation = initialRotations[i];
			}
		}

		if(bodyRoot != null)
		{
			bodyRoot.localPosition = Vector3.zero;
			bodyRoot.localRotation = Quaternion.identity;
		}

		// Restaura la posición y la rotación del offset
		if(offsetNode != null)
		{
			offsetNode.transform.position = initialPosition;
			offsetNode.transform.rotation = initialRotation;
		}
		else
		{
			transform.position = initialPosition;
			transform.rotation = initialRotation;
		}
	}

	// Se invoca en la calibración exitosa de un reproductor.
	public void SuccessfulCalibration(uint userId)
	{
		// restablece la posición de los modelos
		if(offsetNode != null)
		{
			offsetNode.transform.rotation = initialRotation;
		}

		// recalibra el desplazamiento de posición
		offsetCalibrated = false;
	}

	// Aplicar las rotaciones rastreadas por kinect a las articulaciones.
	protected void TransformBone(uint userId, KinectWrapper.NuiSkeletonPositionIndex joint, int boneIndex, bool flip)
	{
		Transform boneTransform = bones[boneIndex];
		if(boneTransform == null || kinectManager == null)
			return;

		int iJoint = (int)joint;
		if(iJoint < 0)
			return;

		// Obtener la orientación de Kinect
		Quaternion jointRotation = kinectManager.GetJointOrientation(userId, iJoint, flip);
		if(jointRotation == Quaternion.identity)
			return;

		// Transición suave a la nueva rotación
		Quaternion newRotation = Kinect2AvatarRot(jointRotation, boneIndex);

		if(smoothFactor != 0f)
			boneTransform.rotation = Quaternion.Slerp(boneTransform.rotation, newRotation, smoothFactor * Time.deltaTime);
		else
			boneTransform.rotation = newRotation;
	}

	// Aplicar las rotaciones rastreadas por kinect a una junta especial
	protected void TransformSpecialBone(uint userId, KinectWrapper.NuiSkeletonPositionIndex joint, KinectWrapper.NuiSkeletonPositionIndex jointParent, int boneIndex, Vector3 baseDir, bool flip)
	{
		Transform boneTransform = bones[boneIndex];
		if(boneTransform == null || kinectManager == null)
			return;

		if(!kinectManager.IsJointTracked(userId, (int)joint) || 
			!kinectManager.IsJointTracked(userId, (int)jointParent))
		{
			return;
		}

		Vector3 jointDir = kinectManager.GetDirectionBetweenJoints(userId, (int)jointParent, (int)joint, false, true);
		Quaternion jointRotation = jointDir != Vector3.zero ? Quaternion.FromToRotation(baseDir, jointDir) : Quaternion.identity;

		//		if(!flip)
		//		{
		//			Vector3 mirroredAngles = jointRotation.eulerAngles;
		//			mirroredAngles.y = -mirroredAngles.y;
		//			mirroredAngles.z = -mirroredAngles.z;
		//			
		//			jointRotation = Quaternion.Euler(mirroredAngles);
		//		}

		if(jointRotation != Quaternion.identity)
		{
			// Smoothly transition to the new rotation
			Quaternion newRotation = Kinect2AvatarRot(jointRotation, boneIndex);

			if(smoothFactor != 0f)
				boneTransform.rotation = Quaternion.Slerp(boneTransform.rotation, newRotation, smoothFactor * Time.deltaTime);
			else
				boneTransform.rotation = newRotation;
		}

	}

	// Moves the avatar in 3D space - pulls the tracked position of the spine and applies it to root.
	// Only pulls positional, not rotational.
	protected void MoveAvatar(uint UserID)
	{
		if(bodyRoot == null || kinectManager == null)
			return;
		if(!kinectManager.IsJointTracked(UserID, (int)KinectWrapper.NuiSkeletonPositionIndex.HipCenter))
			return;

		// Obtener la posición del cuerpo y almacenarlo.
		Vector3 trans = kinectManager.GetUserPosition(UserID);

		// Si es la primera vez que movemos el avatar, establecemos el desplazamiento. De lo contrario ignorarlo.
		if (!offsetCalibrated)
		{
			offsetCalibrated = true;

			xOffset = !mirroredMovement ? trans.x * moveRate : -trans.x * moveRate;
			yOffset = trans.y * moveRate;
			zOffset = -trans.z * moveRate;

			if(offsetRelativeToSensor)
			{
				Vector3 cameraPos = Camera.main.transform.position;

				float yRelToAvatar = (offsetNode != null ? offsetNode.transform.position.y : transform.position.y) - cameraPos.y;
				Vector3 relativePos = new Vector3(trans.x * moveRate, yRelToAvatar, trans.z * moveRate);
				Vector3 offsetPos = cameraPos + relativePos;

				if(offsetNode != null)
				{
					offsetNode.transform.position = offsetPos;
				}
				else
				{
					transform.position = offsetPos;
				}
			}
		}

		// Transición suave a la nueva posición
		Vector3 targetPos = Kinect2AvatarPos(trans, verticalMovement);

		if(smoothFactor != 0f)
			bodyRoot.localPosition = Vector3.Lerp(bodyRoot.localPosition, targetPos, smoothFactor * Time.deltaTime);
		else
			bodyRoot.localPosition = targetPos;
	}

	// Si los huesos a ser mapeados han sido declarados, mapear ese hueso al modelo.
	protected virtual void MapBones()
	{
		// make OffsetNode como padre de la transformación del modelo.
		offsetNode = new GameObject(name + "Ctrl") { layer = transform.gameObject.layer, tag = transform.gameObject.tag };
		offsetNode.transform.position = transform.position;
		offsetNode.transform.rotation = transform.rotation;
		offsetNode.transform.parent = transform.parent;

		transform.parent = offsetNode.transform;
		transform.localPosition = Vector3.zero;
		transform.localRotation = Quaternion.identity;

		// toma la transformación del modelo como raíz del cuerpo
		bodyRoot = transform;

		// obtener transformaciones óseas desde el componente animador
		var animatorComponent = GetComponent<Animator>();

		for (int boneIndex = 0; boneIndex < bones.Length; boneIndex++)
		{
			if (!boneIndex2MecanimMap.ContainsKey(boneIndex)) 
				continue;

			bones[boneIndex] = animatorComponent.GetBoneTransform(boneIndex2MecanimMap[boneIndex]);
		}
	}

	// obtener transformaciones óseas desde el componente animador
	protected void GetInitialRotations()
	{
		// guardar la rotación inicial
		if(offsetNode != null)
		{
			initialPosition = offsetNode.transform.position;
			initialRotation = offsetNode.transform.rotation;

			offsetNode.transform.rotation = Quaternion.identity;
		}
		else
		{
			initialPosition = transform.position;
			initialRotation = transform.rotation;

			transform.rotation = Quaternion.identity;
		}

		for (int i = 0; i < bones.Length; i++)
		{
			if (bones[i] != null)
			{
				initialRotations[i] = bones[i].rotation; // * Quaternion.Inverse(initialRotation);
				initialLocalRotations[i] = bones[i].localRotation;
			}
		}

		// Restore the initial rotation
		if(offsetNode != null)
		{
			offsetNode.transform.rotation = initialRotation;
		}
		else
		{
			transform.rotation = initialRotation;
		}
	}

	// Convierte la rotación de la junta kinect a la rotación de la articulación del avatar, dependiendo de la rotación inicial conjunta y de la rotación de desplazamiento
	protected Quaternion Kinect2AvatarRot(Quaternion jointRotation, int boneIndex)
	{
		// Aplicar la nueva rotación.
		Quaternion newRotation = jointRotation * initialRotations[boneIndex];

		// Si se especifica un nodo de desplazamiento, combine la transformación con su
		// orientación para hacer esencialmente el esqueleto relativo al nodo
		if (offsetNode != null)
		{
			// Captura la rotación total añadiendo Euler y Euler.
			Vector3 totalRotation = newRotation.eulerAngles + offsetNode.transform.rotation.eulerAngles;
			// Coge nuestra nueva rotación.
			newRotation = Quaternion.Euler(totalRotation);
		}

		return newRotation;
	}

	// Convierte la posición de Kinect a la posición del esqueleto del avatar, dependiendo de la posición inicial, reflejo y velocidad de movimiento
	protected Vector3 Kinect2AvatarPos(Vector3 jointPosition, bool bMoveVertically)
	{
		float xPos;
		float yPos;
		float zPos;

		// Si el movimiento es reflejado, invierte.
		if(!mirroredMovement)
			xPos = jointPosition.x * moveRate - xOffset;
		else
			xPos = -jointPosition.x * moveRate - xOffset;

		yPos = jointPosition.y * moveRate - yOffset;
		zPos = -jointPosition.z * moveRate - zOffset;

		// Si seguimos el movimiento vertical, actualizamos la y. De lo contrario dejarlo solo.
		Vector3 avatarJointPos = new Vector3(xPos, bMoveVertically ? yPos : 0f, zPos);

		return avatarJointPos;
	}

	// diccionarios para acelerar el procesamiento de los huesos
	// el autor de la idea fabulosa para las juntas de kinect a la cartografía de mecanim-huesos
	// junto con su implementación inicial, incluyendo el siguiente diccionario es
	// Mikhail Korchun (korchoon@gmail.com). Muchas gracias a este tipo!
	private readonly Dictionary<int, HumanBodyBones> boneIndex2MecanimMap = new Dictionary<int, HumanBodyBones>
	{
		{0, HumanBodyBones.Hips},
		{1, HumanBodyBones.Spine},
		{2, HumanBodyBones.Neck},
		{3, HumanBodyBones.Head},

		{4, HumanBodyBones.LeftShoulder},
		{5, HumanBodyBones.LeftUpperArm},
		{6, HumanBodyBones.LeftLowerArm},
		{7, HumanBodyBones.LeftHand},
		{8, HumanBodyBones.LeftIndexProximal},

		{9, HumanBodyBones.RightShoulder},
		{10, HumanBodyBones.RightUpperArm},
		{11, HumanBodyBones.RightLowerArm},
		{12, HumanBodyBones.RightHand},
		{13, HumanBodyBones.RightIndexProximal},

		{14, HumanBodyBones.LeftUpperLeg},
		{15, HumanBodyBones.LeftLowerLeg},
		{16, HumanBodyBones.LeftFoot},
		{17, HumanBodyBones.LeftToes},

		{18, HumanBodyBones.RightUpperLeg},
		{19, HumanBodyBones.RightLowerLeg},
		{20, HumanBodyBones.RightFoot},
		{21, HumanBodyBones.RightToes},
	};

	protected readonly Dictionary<int, KinectWrapper.NuiSkeletonPositionIndex> boneIndex2JointMap = new Dictionary<int, KinectWrapper.NuiSkeletonPositionIndex>
	{
		{0, KinectWrapper.NuiSkeletonPositionIndex.HipCenter},
		{1, KinectWrapper.NuiSkeletonPositionIndex.Spine},
		{2, KinectWrapper.NuiSkeletonPositionIndex.ShoulderCenter},
		{3, KinectWrapper.NuiSkeletonPositionIndex.Head},

		{5, KinectWrapper.NuiSkeletonPositionIndex.ShoulderLeft},
		{6, KinectWrapper.NuiSkeletonPositionIndex.ElbowLeft},
		{7, KinectWrapper.NuiSkeletonPositionIndex.WristLeft},
		{8, KinectWrapper.NuiSkeletonPositionIndex.HandLeft},

		{10, KinectWrapper.NuiSkeletonPositionIndex.ShoulderRight},
		{11, KinectWrapper.NuiSkeletonPositionIndex.ElbowRight},
		{12, KinectWrapper.NuiSkeletonPositionIndex.WristRight},
		{13, KinectWrapper.NuiSkeletonPositionIndex.HandRight},

		{14, KinectWrapper.NuiSkeletonPositionIndex.HipLeft},
		{15, KinectWrapper.NuiSkeletonPositionIndex.KneeLeft},
		{16, KinectWrapper.NuiSkeletonPositionIndex.AnkleLeft},
		{17, KinectWrapper.NuiSkeletonPositionIndex.FootLeft},

		{18, KinectWrapper.NuiSkeletonPositionIndex.HipRight},
		{19, KinectWrapper.NuiSkeletonPositionIndex.KneeRight},
		{20, KinectWrapper.NuiSkeletonPositionIndex.AnkleRight},
		{21, KinectWrapper.NuiSkeletonPositionIndex.FootRight},
	};

	protected readonly Dictionary<int, List<KinectWrapper.NuiSkeletonPositionIndex>> specIndex2JointMap = new Dictionary<int, List<KinectWrapper.NuiSkeletonPositionIndex>>
	{
		{4, new List<KinectWrapper.NuiSkeletonPositionIndex> {KinectWrapper.NuiSkeletonPositionIndex.ShoulderLeft, KinectWrapper.NuiSkeletonPositionIndex.ShoulderCenter} },
		{9, new List<KinectWrapper.NuiSkeletonPositionIndex> {KinectWrapper.NuiSkeletonPositionIndex.ShoulderRight, KinectWrapper.NuiSkeletonPositionIndex.ShoulderCenter} },
	};

	protected readonly Dictionary<int, KinectWrapper.NuiSkeletonPositionIndex> boneIndex2MirrorJointMap = new Dictionary<int, KinectWrapper.NuiSkeletonPositionIndex>
	{
		{0, KinectWrapper.NuiSkeletonPositionIndex.HipCenter},
		{1, KinectWrapper.NuiSkeletonPositionIndex.Spine},
		{2, KinectWrapper.NuiSkeletonPositionIndex.ShoulderCenter},
		{3, KinectWrapper.NuiSkeletonPositionIndex.Head},

		{5, KinectWrapper.NuiSkeletonPositionIndex.ShoulderRight},
		{6, KinectWrapper.NuiSkeletonPositionIndex.ElbowRight},
		{7, KinectWrapper.NuiSkeletonPositionIndex.WristRight},
		{8, KinectWrapper.NuiSkeletonPositionIndex.HandRight},

		{10, KinectWrapper.NuiSkeletonPositionIndex.ShoulderLeft},
		{11, KinectWrapper.NuiSkeletonPositionIndex.ElbowLeft},
		{12, KinectWrapper.NuiSkeletonPositionIndex.WristLeft},
		{13, KinectWrapper.NuiSkeletonPositionIndex.HandLeft},

		{14, KinectWrapper.NuiSkeletonPositionIndex.HipRight},
		{15, KinectWrapper.NuiSkeletonPositionIndex.KneeRight},
		{16, KinectWrapper.NuiSkeletonPositionIndex.AnkleRight},
		{17, KinectWrapper.NuiSkeletonPositionIndex.FootRight},

		{18, KinectWrapper.NuiSkeletonPositionIndex.HipLeft},
		{19, KinectWrapper.NuiSkeletonPositionIndex.KneeLeft},
		{20, KinectWrapper.NuiSkeletonPositionIndex.AnkleLeft},
		{21, KinectWrapper.NuiSkeletonPositionIndex.FootLeft},
	};

	protected readonly Dictionary<int, List<KinectWrapper.NuiSkeletonPositionIndex>> specIndex2MirrorJointMap = new Dictionary<int, List<KinectWrapper.NuiSkeletonPositionIndex>>
	{
		{4, new List<KinectWrapper.NuiSkeletonPositionIndex> {KinectWrapper.NuiSkeletonPositionIndex.ShoulderRight, KinectWrapper.NuiSkeletonPositionIndex.ShoulderCenter} },
		{9, new List<KinectWrapper.NuiSkeletonPositionIndex> {KinectWrapper.NuiSkeletonPositionIndex.ShoulderLeft, KinectWrapper.NuiSkeletonPositionIndex.ShoulderCenter} },
	};

}

