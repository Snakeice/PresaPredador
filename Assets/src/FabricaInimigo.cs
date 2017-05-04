 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Utils.dp;

public class FabricaInimigo : MonoBehaviour{
	private Vector3 localDeConstrucao;
	public GameObject inimigo;

	public void cria ()	{
		Instantiate (inimigo, localDeConstrucao, Quaternion.identity);
	}


	public List<Inimigo> CriarInimigos(GameObject go, int qtdd){
		List<Inimigo> inimigos = new List<Inimigo>();
		for (int i = 0; i < qtdd; i++) {
			inimigos.Add (criaSobreObj (go));
		}

		return inimigos;

	}

	public void ManterAtivo(List<Inimigo> inimigos, int qtdd){
		System.Random rnd = new System.Random ();
		foreach (Inimigo inimigo in inimigos) {
			inimigo.gameObject.SetActive (false);
		}
		for (int i = 0; i < qtdd; i++) {
			inimigos [rnd.Next (0, inimigos.Count)].gameObject.SetActive (true);
		}
	}


	public Inimigo criaSobreObj (GameObject go)	{

		Bounds bounds = go.GetComponent<MeshFilter>().mesh.bounds;
		float minX = gameObject.transform.position.x - gameObject.transform.localScale.x * bounds.size.x * 0.5f;
		float minZ = gameObject.transform.position.z - gameObject.transform.localScale.z * bounds.size.z * 0.5f;
		Vector3 newVec = new Vector3(Random.Range (minX, -minX), 0.8f, Random.Range (minZ, -minZ));


		GameObject enemyObj = Instantiate (inimigo, newVec, Quaternion.identity);
		enemyObj.name = "Inimigo:"+ (System.Math.Ceiling( Random.value * 100000));
		return enemyObj.GetComponent<Inimigo>();
	}

	public void SetLocalDeConstrucao(Vector3 localDeConstrucao)	{
		this.localDeConstrucao = localDeConstrucao;
	}

}
