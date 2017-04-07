using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using Enums;
using Utils;

public class Alvo : MonoBehaviour {
	private ThirdPersonCharacter controll;
	public EstadoEnum estado;
	private Vector3 FutPos;
	public float raioVisao;

	private float timer;
	public float wanderTimer;
	// Use this for initialization
	void Start () {
		estado = EstadoEnum.Passeio;
		controll = GetComponent<ThirdPersonCharacter> ();
		timer = wanderTimer;

		
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer >= wanderTimer) {
			FutPos = UtilsGeral.RandomNavSphere (transform.position, 40, -1);
		timer = 0;
	}
		if (!InimigoNaVisao ()) {
			controll.Move (FutPos, false, false);
		} else {
			
		}
	}

	private bool InimigoNaVisao(){
		foreach (GameObject go in GameObject.FindGameObjectsWithTag("Inimigo")) {
			if (UtilsGeral.estahNaVisao (this.gameObject, go, raioVisao)) {
				estado = EstadoEnum.Fulga;
				return true;
			}
		}
		estado = EstadoEnum.Passeio;
		return false;
		}

	private bool Fugir(){
		if (estado == EstadoEnum.Passeio) return false;
		
		return true;
	}
		
}
