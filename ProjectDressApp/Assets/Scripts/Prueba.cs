using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;
using System.Text; 

[RequireComponent(typeof(Animator))]
public class Prueba : MonoBehaviour {


	public bool mirroredMovement = false;// Bool que tiene los caracteres (mirando al jugador) las acciones se vuelven a reflejar. Predeterminado false.

	public bool verticalMovement = false;// Bool que determina si el avatar se permite mover en la dirección vertical.

	protected int moveRate = 1;// Valor en el cual el avatar se moverá a través de la escena. La velocidad multiplica la velocidad de movimiento (.001f, es decir, dividiendo por 1000, el framerate de la unidad).

	public float smoothFactor = 5f;// Slerp factor liso

	public bool offsetRelativeToSensor = false;// Si el nodo de desplazamiento debe reposicionarse a las coordenadas del usuario, según lo informado por el sensor o no.

	protected Transform bodyRoot;// El nodo raíz del cuerpo

	protected GameObject offsetNode;// Una variable requerida si desea girar el modelo en el espacio.

	protected Transform[] bones;// Variable para contener todos los huesos. Se inicializará el mismo tamaño que initialRotations.

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

	void Awake()
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

		Debug.Log ("es el awake");
	}

	// Use this for initialization
	void Start () {
		Debug.Log ("es el start");
	}

	// Update is called once per frame
	void Update () {
		Debug.Log ("es el update");
		//transform.Rotate (Vector3.up * Time.deltaTime * degreesPerSecond);

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
}
