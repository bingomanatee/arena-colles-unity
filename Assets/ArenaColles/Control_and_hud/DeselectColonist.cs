using UnityEngine;
using System.Collections;

namespace ArenaColles
{
		public class DeselectColonist : MonoBehaviour
		{

				// Use this for initialization
				void Start ()
				{

				}
	
				// Update is called once per frame
				void Update ()
				{
	
				}
		
	
				void OnMouseUpAsButton ()
				{
						Game.GameActiveColonist = null;
				}
		}
}