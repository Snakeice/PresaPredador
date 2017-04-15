﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using Enums;
using Utils;
using Utils.dp;

public class Alvo : MonoBehaviour {
	private ThirdPersonCharacter controll;
	public EstadoEnum estadoPasseio;
	private Vector3 FutPos;
	public float raioVisao;
	private PlayerStateEnum estadoCaptura = PlayerStateEnum.Livre;

	private string TAG_PAREDE = "Parede";
	private float timer;
	public float wanderTimer;
	// Use this for initialization
	void Start () {
		estadoPasseio = EstadoEnum.Passeio;
		controll = GetComponent<ThirdPersonCharacter> ();
		timer = wanderTimer;

		
	}
	
	// Update is called once per frame
	void Update () {
		MoverPlayer ();
	}

	private void MoverPlayer(){
		if (estadoCaptura == PlayerStateEnum.Livre) {
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


	}

	private bool InimigoNaVisao(){
		foreach (GameObject go in GameObject.FindGameObjectsWithTag("Inimigo")) {
			if (UtilsGeral.estahNaVisao (this.gameObject, go, raioVisao)) {
				estadoPasseio = EstadoEnum.Fulga;
				return true;
			}
		}
		estadoPasseio = EstadoEnum.Passeio;
		return false;
		}

	private bool Fugir(){
		if (estadoPasseio == EstadoEnum.Passeio) return false;
		
		return true;
	}



	void OnTriggerEnter(Collider col){
		if (col.CompareTag (TAG_PAREDE)) {
			Debug.Log ("Alvo bateu na parede:" + Time.frameCount);
			EventBus.Instance.Post (Enums.ColliderUpdate.Atualizar);
				
		}
	}
		
}
