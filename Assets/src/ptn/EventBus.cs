using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

namespace Utils.dp{
	public class Kakaroto:Attribute{
	}

	public sealed class EventBus
	{

		private List<MonoBehaviour> _subscribers = new List<MonoBehaviour>();
		private static EventBus instance;
		private static object syncRoot = new object();
	
	
		public EventBus (){
		}

		public static  EventBus Instance {
			get {
				if (instance == null) {
					lock (syncRoot) {
						if (instance == null) {
							instance = new EventBus ();
						}
					}
				}
					return instance;
			}
		}
					

		public void Register(MonoBehaviour subscriber){
			if (!_subscribers.Contains (subscriber)) {
				_subscribers.Add (subscriber);
			}
		}


		 

		public void Unregister(MonoBehaviour subscriber){
			if (_subscribers.Contains (subscriber)) {
				_subscribers.Remove (subscriber);
			}
		}

		public void Post(object e){
            foreach(object instance in _subscribers){
				foreach(MethodInfo method in GetSubscribedMethods(instance.GetType (), e)){
					try{
						method.Invoke (instance, new object[]{e});
					}catch(TargetInvocationException){}
				}
			}
		}

		private List<MethodInfo> GetSubscribedMethods (Type type, object obj)
		{
			List<MethodInfo> subscribedMethods = new List<MethodInfo> ();
			MethodInfo[] methods = 	type.GetMethods ();
			foreach(MethodInfo info in methods){
				foreach(Attribute attr in info.GetCustomAttributes (true)){
					if (attr.GetType () == typeof(Kakaroto)) {
						var paramInfo = info.GetParameters ();
						if(paramInfo.Length == 1 && paramInfo[0].ParameterType == obj.GetType ()){
							subscribedMethods.Add (info);
						}
					}
				}
			}
			return subscribedMethods;
		}
	}

}