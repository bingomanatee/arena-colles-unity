using UnityEngine;
using System.Collections;

namespace ArenaColles
{
		public class ItemSlot : MonoBehaviour
		{
				GameObject oc = null;
		
				public GameObject occupant {
						get { 
								return oc;
						}
						set {
								if (value) {
										value.transform.parent = transform;
										value.transform.position = transform.position;
										value.transform.rotation = transform.rotation;
										value.transform.localScale = Vector3.one;
								}
								oc = value;
						}
				}
				// Use this for initialization
				void Start ()
				{
	
				}
	
				// Update is called once per frame
				void Update ()
				{
	
				}
		}
}