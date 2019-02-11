using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;

public class VuMarkIdManager_base : MonoBehaviour {

	VuMarkManager m_VuMarkManager;
	public UnityEngine.UI.Image imageVuMark; //Imagen del VuMark

	void Start () {
		m_VuMarkManager = TrackerManager.Instance.GetStateManager ().GetVuMarkManager ();
		m_VuMarkManager.RegisterVuMarkDetectedCallback (OnVuMarkDetected);
		m_VuMarkManager.RegisterVuMarkLostCallback (OnVuMarkLost);
	}

	//Encuentra al VuMark
	public void OnVuMarkDetected (VuMarkTarget target) {
		Debug.Log ("ID: " + GetVuMarkId (target)); //Mensaje con el ID del VuMark
		Debug.Log ("Tipo: " + GetVuMarkDataType (target)); //Mensaje con el tipo de VuMark
		Debug.Log ("Descripcion: " + GetNumericVuMarkDescription (target)); //Mensaje con la descripcion del VuMark
		imageVuMark.sprite = GetVuMarkImage (target); //Imagen del VuMark
	}

	//Cuando pierde de vista al VuMark
	public void OnVuMarkLost (VuMarkTarget target) {
		Debug.Log ("Lost VuMark: " + GetVuMarkId (target)); //Mensaje que se perdio el VuMark, mostrando el ID del mismo
		imageVuMark.sprite = null; //Borra la imagen actual
	}

	//Tipo de VuMark, que se setea al crear el VuMark
	string GetVuMarkDataType (VuMarkTarget vumark) {
		switch (vumark.InstanceId.DataType) {
			case InstanceIdType.BYTES:
				return "Bytes";
			case InstanceIdType.STRING:
				return "String";
			case InstanceIdType.NUMERIC:
				return "Numeric";
		}
		return string.Empty;
	}

	//Obtiene el ID del VuMark
	string GetVuMarkId (VuMarkTarget vumark) {
		switch (vumark.InstanceId.DataType) {
			case InstanceIdType.BYTES:
				return vumark.InstanceId.HexStringValue;
			case InstanceIdType.STRING:
				return vumark.InstanceId.StringValue;
			case InstanceIdType.NUMERIC:
				return vumark.InstanceId.NumericValue.ToString ();
		}
		return string.Empty;
	}

	//Obtiene la imagen del VuMark
	Sprite GetVuMarkImage (VuMarkTarget vumark) {
		//Toma la imagen del VuMark
		var instanceImg = vumark.InstanceImage;
		if (instanceImg == null) {
			Debug.Log ("La instancia de la imagen del VuMark no existe");
			return null;
		}

		//Se crea una textura a partir de la instancia de la Imagen del VuMark
		Texture2D texture = new Texture2D (instanceImg.Width, instanceImg.Height, TextureFormat.RGBA32, false) {
			wrapMode = TextureWrapMode.Clamp
		};
		instanceImg.CopyToTexture (texture);

		//Se convierte la textura en un Sprite
		Rect rect = new Rect (0, 0, texture.width, texture.height);
		return Sprite.Create (texture, rect, new Vector2 (0.5f, 0.5f));
	}

	//Descripcion del VuMark
	string GetNumericVuMarkDescription (VuMarkTarget vumark) {
		int vuMarkIdNumeric; //Almacenara el ID del VuMark

		if (int.TryParse (GetVuMarkId (vumark), out vuMarkIdNumeric)) { //Convierte el ID del VuMark en una variable numerica
			
			//Cambia la descripcion de acuerdo al resto que se obtiene de la variable que almacena el ID del VuMark
			switch (vuMarkIdNumeric % 4) {
				case 1:
					return "Astronaut";
				case 2:
					return "Drone";
				case 3:
					return "Fissure";
				case 0:
					return "Oxygen Tank";
				default:
					return "Astronaut";
			}
		}

		return string.Empty; 
	}

}
