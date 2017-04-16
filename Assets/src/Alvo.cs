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
	[SerializeField]
	private GameObject blood;
	private string TAG_PAREDE = "Parede";
	private float timer;
	public float wanderTimer;
	private bool paredeAtualizada = false;
	private Vector3 parede;


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
	//	if (estadoCaptura == PlayerStateEnum.Livre) {
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



		//}


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
			paredeAtualizada = false;
			return false;
		}
		if (!paredeAtualizada) {
			paredeAtualizada = true;
			findWall (controll.gameObject);
		}
		controll.Move (parede, false, false);
		return true;
	}



	void OnTriggerEnter(Collider col){
		if (col.CompareTag (TAG_PAREDE)) {
			Debug.Log ("Alvo bateu na parede:" + Time.frameCount);
			EventBus.Instance.Post (Enums.ColliderUpdate.Atualizar);
			timer = 9999;
			
		}
	}



	private void findWall(GameObject alvo){
		Vector3 player = Vector3.ProjectOnPlane (alvo.transform.position, Vector3.up );
		RaycastHit[] hits; 
		float menorDistancia = 999999;
		for (int i = 1; i <= 360; i += 5) { 
			Vector3 rotacao = new Vector3 (0,i, 0);

			hits = Physics.RaycastAll (player, rotacao, 8);
			if ((hits == null) || (hits.Length == 0))
				continue;
			foreach (RaycastHit hit in hits) {

				Debug.DrawLine(transform.position, hit.point, Color.green);
				if (hit.collider.CompareTag (TAG_PAREDE)) {
					float distancia = UtilsGeral.CalcularDistancia (this.gameObject, hit.point);
					if (distancia < menorDistancia) {
						menorDistancia = distancia;
						parede = hit.point;
					}
				}
			}
		}
		if (menorDistancia == 999999)
			paredeAtualizada = false;
	}
		
}
