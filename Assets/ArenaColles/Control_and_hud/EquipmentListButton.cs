using UnityEngine;
using System.Collections;

namespace ArenaColles
{
		public class EquipmentListButton : MonoBehaviour
		{
				public ColonyHUDmanager manager;
				// Use this for initialization
				void Start ()
				{
	
				}
	
				// Update is called once per frame
				void Update ()
				{
	
				}
		
				void OnMouseDown ()
				{
						Debug.Log ("CB MD");
						ShowEquipment ();
				}
		
		#region colonistDisplay
		
				void ShowEquipment ()
				{
						manager.ShowEquipment ();
				}
		
		#endregion
		}
}