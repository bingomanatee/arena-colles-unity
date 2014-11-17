using UnityEngine;
using System.Collections;

namespace ArenaColles
{
		public class CloseColony : MonoBehaviour
		{

				// Use this for initialization
				void Start ()
				{

				}
	
				// Update is called once per frame
				void Update ()
				{
	
				}
		
	
				void OnMouseDownAsButton ()
				{
						Game.game.ActiveDome = null;
				}
		}
}