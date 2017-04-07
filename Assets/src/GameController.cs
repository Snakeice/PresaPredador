using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.dp;
using Enums;

public class GameController : MonoBehaviour {


	private Dictionary<Inimigo, bool> inimigosAtivosControll;

	void Start () {
		inimigosAtivosControll = new Dictionary<Inimigo, bool> ();
		EventBus.Instance.Register (this);	
	}

	void OnDestroy(){
		EventBus.Instance.Unregister (this);
	}

	void Update () {

	}

	[Kakaroto]
	public void UpdateGameState(Enums.ColliderUpdate colUP){
		AtualizarEstadoDoJogo ();
	}
		
	private void AtualizarEstadoDoJogo(){
		foreach (Inimigo inimigo in inimigosAtivosControll.Keys) {
			inimigosAtivosControll [inimigo] = inimigo.GetBateuNoAlvo();
		}
		bool resultado= true;
		foreach (bool valor in inimigosAtivosControll.Values) {
			if (!valor) {
				resultado = false;
			}
		}
		if (resultado) {
			EventBus.Instance.Post(EstadoDoJogoEnum.rodando);
		} else {
			EventBus.Instance.Post(EstadoDoJogoEnum.gameOver);
		}
	}



	[Kakaroto]
	public void UpdateActiveEnemyList(List<Inimigo> inimigos){
		inimigosAtivosControll.Clear();
		foreach(Inimigo inimigo in inimigos){
			if (inimigo.enabled) {
				inimigosAtivosControll.Add (inimigo, inimigo.GetBateuNoAlvo ());
			}
		}
	}
}
