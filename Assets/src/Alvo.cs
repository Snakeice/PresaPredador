using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using Enums;
using Utils;
using Utils.dp;
using UnityEngine.AI;

public class Alvo : MonoBehaviour {
	private ThirdPersonCharacter controll;
	public EstadoEnum estadoPasseio;
	private Vector3 FutPos;
	public float raioVisao;
	[SerializeField]
	private GameObject blood;
	[SerializeField]
	private ParticleSystem ps;
	[SerializeField]
	private AudioSource grito;
	[SerializeField]
	private AudioSource fuga;
	private string TAG_PAREDE = "Parede";
	private float timer;
	public float wanderTimer;
	private bool paredeAtualizada = false;
	private Vector3 parede;


	void Start () {
		estadoPasseio = EstadoEnum.Passeio;
		controll = GetComponent<ThirdPersonCharacter> ();
		timer = wanderTimer;
		EventBus.Instance.Register (this);
		Camera.current.transform.LookAt (transform);

		
	}
	
	// Update is called once per frame
	void Update () {
		if (estadoPasseio == EstadoEnum.cacaOK || estadoPasseio == EstadoEnum.fulgaOK) {
			
		} else {
			MoverPlayer ();
		}
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

/*	[Kakaroto]
	public void AtualizaEstadoFulga(EstadoDoJogoEnum e){
		if (e != EstadoDoJogoEnum.rodando) {
			fuga.Stop ();
		}
	}*/

	public void sangrar(){
		Instantiate(blood, this.transform.position, Random.rotation); 
	}

	[Kakaroto]
	public void Sangue(SangueEnum sangue){
		ps.gameObject.SetActive (true);
		grito.Play ();
		sangrar ();
		ps.Emit (1024);
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
			fuga.Play ();
		}
		controll.Move (parede, false, false);
		return true;
	}



	void OnTriggerEnter(Collider col){
		if (col.CompareTag (TAG_PAREDE) && estadoPasseio.Equals(EstadoEnum.Fulga)) {
			Debug.Log ("Alvo bateu na parede:" + Time.frameCount);
			estadoPasseio = EstadoEnum.fulgaOK;
			EventBus.Instance.Post (estadoPasseio);
			EventBus.Instance.Post (Enums.ColliderUpdate.Atualizar);
			timer = 9999;
		}
	}



	private void findWall(GameObject alvo){
		Vector3 player = Vector3.ProjectOnPlane (alvo.transform.position, Vector3.up );
		RaycastHit[] hit; 
		float menorDistancia = 999999;
		float dist = 0;
		hit = Physics.RaycastAll (player, Vector3.forward, 9999999099); 
			if (hit != null){
				foreach(RaycastHit h in hit){
					dist = UtilsGeral.CalcularDistancia (player, h.point);
					Debug.DrawRay (player, h.point, Color.blue, 9999);
					if (dist < menorDistancia) {
						menorDistancia = dist;
						parede = h.point;
					}
				}
			}

				hit = Physics.RaycastAll (player, Vector3.back, 9999999099) ;
			if (hit != null){
				foreach(RaycastHit h in hit){
					dist = UtilsGeral.CalcularDistancia (player, h.point);
					Debug.DrawRay (player, h.point, Color.blue, 9999);
					if (dist < menorDistancia) {
						menorDistancia = dist;
						parede = h.point;
					}
				}
			}

				hit = Physics.RaycastAll (player, Vector3.left, 9999999099) ;
			if (hit != null){
				foreach(RaycastHit h in hit){
					dist = UtilsGeral.CalcularDistancia (player, h.point);
					Debug.DrawRay (player, h.point, Color.blue, 9999);
					if (dist < menorDistancia) {
						menorDistancia = dist;
						parede = h.point;
					}
				}
			}
		

				hit = Physics.RaycastAll (player, Vector3.right, 9999999099); 
			if (hit != null){
				foreach(RaycastHit h in hit){
					dist = UtilsGeral.CalcularDistancia (player, h.point);
					Debug.DrawRay (player, h.point, Color.blue, 9999);
					if (dist < menorDistancia) {
						menorDistancia = dist;
						parede = h.point;
					}
				}
			}

		if (menorDistancia == 999999)
			paredeAtualizada = false;
	}
		
}
