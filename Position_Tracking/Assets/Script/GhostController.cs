using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostController : MonoBehaviour {

    public Transform player;
    public float sizeRandom;
    public int puntos;
    public Text txtPuntos;
	// Use this for initialization
	void Start () {
        cambiarPos();
        txtPuntos.text = puntos.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnMouseDown()
    {
        cambiarPos();
        SumarPuntos();
    }
    void SumarPuntos()
    {
        puntos++;
        txtPuntos.text = puntos.ToString();
    }
    
    void cambiarPos()
    {
        transform.position = new Vector3((Random.insideUnitSphere.x * sizeRandom), transform.position.y, (Random.insideUnitSphere.z * sizeRandom));
        Vector3 posJugador = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(posJugador);
    }
}
