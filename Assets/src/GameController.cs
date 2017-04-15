using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.dp;
using Enums;

public class GameController : MonoBehaviour {


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
		foreach (Inimigo inimigo in inimigosAtivosControll) {
			if (!inimigo.GetBateuNoAlvo()) {
					resultado = false;
				}
			}
			if (!resultado) {
				EventBus.Instance.Post (EstadoDoJogoEnum.rodando);
			} else {
				EventBus.Instance.Post (EstadoDoJogoEnum.gameOver);
			}
	}



	[Kakaroto]
	public void UpdateActiveEnemyList(List<Inimigo> inimigos){
		inimigosAtivosControll.Clear();
		foreach(Inimigo inimigo in inimigos){
			if (inimigo.enabled) {
				inimigosAtivosControll.Add (inimigo);
			}
		}
	}
}
