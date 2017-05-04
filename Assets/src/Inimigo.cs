using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Enums;
using Utils;
using Utils.dp;

public class Inimigo : MonoBehaviour {
	public string TAG;
	public float raioVisao;
	private NavMeshAgent agent ;
	public EstadoEnum estado;
	private bool PerseguicaoForcada = false;
	private bool BateuNoAlvo = false;
	private playerControlDB pc;
	private float timer;
	public float wanderTimer;

	// Use this for initialization
	void Start () {
		estado = EstadoEnum.Passeio;
		agent = GetComponent<NavMeshAgent>();
		timer = wanderTimer;
		transform.position = UtilsGeral.RandomNavSphere(transform.position, 50, -1);
		pc = GetComponent<playerControlDB> ();

		EventBus.Instance.Register (this);
	}
	void OnDestroy(){
		EventBus.Instance.Unregister (this);
	}

	private void SetEstado(EstadoEnum estado){ 
		if (this.estado != estado) {
			this.estado = estado;
			if ( this.estado == EstadoEnum.Caca)EventBus.Instance.Post (EstadoEnum.Caca);
		}
	}

	// Update is called once per frame
		void Update () {
		timer += Time.deltaTime;
		if (!PerseguirTag ()) {
			if (timer >= wanderTimer) {
				Vector3 newPos = UtilsGeral.RandomNavSphere(transform.position, 40, -1);
				agent.SetDestination(newPos);
				timer = 0;
			}
		}
		if (UtilsGeral.estahNaVisao(this.gameObject, GameObject.FindGameObjectWithTag (TAG), raioVisao)) {
			SetEstado (EstadoEnum.Caca);

		} else {
			SetEstado (EstadoEnum.Passeio);
		}
}
	[Kakaroto]
	public void AtualizaTag(EstadoEnum estado){
		this.estado = estado;
		PerseguicaoForcada = true;
	}

	private bool PerseguirTag(){
		if ((estado == EstadoEnum.Passeio) &&(!PerseguicaoForcada)) return false;
		Vector3 direcao = (GameObject.FindGameObjectWithTag("Alvo").transform.position - transform.position).normalized;
		transform.position += direcao * Time.deltaTime*10;

		Quaternion olharPara = Quaternion.LookRotation (direcao);
		direcao.y = 0.5f;
		transform.rotation = olharPara;
		return true;
	}

	void OnTriggerEnter(Collider col){
		
		if ((col.CompareTag (TAG))){
			if(pc != null) 			pc.BasicAttack ();
			if (!BateuNoAlvo) {
				BateuNoAlvo = true;
				EventBus.Instance.Post (SangueEnum.Jorrar);
				EventBus.Instance.Post (ColliderUpdate.Atualizar);
			}
		}
	}
		


	public bool GetBateuNoAlvo ()
	{
		return BateuNoAlvo;
	}

	public void Destruir(){
		DestroyImmediate (this.gameObject);
	}
}
