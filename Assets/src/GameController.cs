using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.dp;
using Enums;

public class GameController : MonoBehaviour {

	[SerializeField]
	private GameObject alvo;
	private List<Inimigo> inimigosAtivosControll;

	void Start () {
		EventBus.Instance.Register (this);	
		inimigosAtivosControll = new List<Inimigo> ();
	}

	void OnDestroy(){
		EventBus.Instance.Unregister (this);
	}

	void Update () {
		AtualizarEstadoDoJogo ();
	}

/*	[Kakaroto]
	public void UpdateGameState(Enums.ColliderUpdate colUP){
		AtualizarEstadoDoJogo ();
	}*/
		
	private void AtualizarEstadoDoJogo(){
		bool resultado = true;
		if (alvo.GetComponent<Alvo> ().estadoPasseio != EstadoEnum.fulgaOK) {
			foreach (Inimigo inimigo in inimigosAtivosControll) {
				if (!inimigo.GetBateuNoAlvo ()) {
					resultado = false;
				}
			}
			if (!resultado) {
				EventBus.Instance.Post (EstadoDoJogoEnum.rodando);
			} else {
				EventBus.Instance.Post (EstadoDoJogoEnum.gameOver);
			}
		} else {
			EventBus.Instance.Post (EstadoDoJogoEnum.wins);
		}
	}



	[Kakaroto]
	public void UpdateActiveEnemyList(List<Inimigo> inimigos){
		inimigosAtivosControll.Clear();
		foreach(Inimigo inimigo in inimigos){
			if (inimigo.gameObject.activeSelf) {
				inimigosAtivosControll.Add (inimigo);
			}
		}
	}
}
