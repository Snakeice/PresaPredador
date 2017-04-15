using System.Collections;
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
	[SerializeField]
	private GameObject blood;
	private string TAG_PAREDE = "Parede";
	private float timer;
	public float wanderTimer;
	private bool paredeAtualizada = false;


	void Start () {
		estadoPasseio = EstadoEnum.Passeio;
		controll = GetComponent<ThirdPersonCharacter> ();
		timer = wanderTimer;


		
	}
	
	// Update is called once per frame
	void Update () {
		MoverPlayer ();
	//	sangrar ();
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
				Fugir ();
			}



		}


	}

	public void sangrar(){
		Instantiate(blood, this.transform.position, Random.rotation); 
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
		if (estadoPasseio == EstadoEnum.Passeio) {
			return false;
		}
		Vector3 paredeProxima = findWall();
		return true;
	}



	void OnTriggerEnter(Collider col){
		if (col.CompareTag (TAG_PAREDE)) {
			Debug.Log ("Alvo bateu na parede:" + Time.frameCount);
			EventBus.Instance.Post (Enums.ColliderUpdate.Atualizar);
			
		}
	}



	private Vector3 findWall(){
		Vector3 player = transform.position;
		RaycastHit hit; 
		float menorDistancia = 999999;
		Vector3 parede = new Vector3();
		for (int i = 1; i <= 360; i += 5) { 
			Vector3 rotacao = new Vector3 (0, 0, i);
			Physics.Raycast (player, rotacao, out hit);
			Debug.Log ("Ang: " + i.ToString() +  "    Obj: " + hit.collider.name + "   dist: " + UtilsGeral.CalcularDistancia (this.gameObject, hit.point).ToString());
			if (hit.collider.CompareTag (TAG_PAREDE)) {
				float distancia = UtilsGeral.CalcularDistancia (this.gameObject, hit.point);
				if (distancia < menorDistancia) {
					menorDistancia = distancia;
					parede = hit.point;
				}
			}
		}
		return parede;
	}
		
}
