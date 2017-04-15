using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace Utils{
public class UtilsGeral : MonoBehaviour {

	public static Vector3 RandomNavSphere (Vector3 origin, float distance, int layermask) {
		Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;

		randomDirection += origin;

		NavMeshHit navHit;

		NavMesh.SamplePosition (randomDirection, out navHit, distance, layermask);

		return navHit.position;
	}

	public static bool estahNaVisao(GameObject o, GameObject alvo, float raioVisao){
			return CalcularDistancia(o, alvo) <= raioVisao; 
	}
		public static float CalcularDistancia(GameObject o, GameObject alvo){
		Vector3 posAlvo = Vector3.ProjectOnPlane (alvo.transform.position, Vector3.up);
		Vector3 posThis = Vector3.ProjectOnPlane (o.transform.position, Vector3.up);
		return Vector3.Distance (posThis, posAlvo);
	}
		public static float CalcularDistancia(GameObject o, Vector3 alvo){
			Vector3 posThis = Vector3.ProjectOnPlane (o.transform.position, Vector3.up);
			return Vector3.Distance (posThis, alvo);
		}
}
}
