using UnityEngine;
using System.Collections;

namespace ArenaColles
{
		public class RoverTest : MonoBehaviour
		{
				public GameObject ac;
				bool roverSpawned = false;
				// Use this for initialization
				bool roverMoved = false;

				void Start ()
				{
						
				}
	
				// Update is called once per frame
				void Update ()
				{
						if (!roverSpawned)
								SpawnRover ();
						else if (!roverMoved)
								MoveRover ();
								
				}

				void SpawnRover ()
				{
						if (!(colony && colony.hasCell))
								return;
				
						Debug.Log ("Spawning rover in RoverTest");
						if (!colony.SpawnRover ("N")) {
								IntegrationTest.Fail (gameObject, "Cannot spawn rover N");
						}
						
						
						roverSpawned = true;
				}

				Dome colony { get { return ac.gameObject.GetComponent<Game> ().colonies.Count > 0 ? ac.gameObject.GetComponent<Game> ().colonies [0] : null; } }
				
				void MoveRover ()
				{
						colony.RoversInColony [0].Rove ();
						colony.RoversInColony [0].Rove ();
						colony.RoversInColony [0].Rove ();
						colony.RoversInColony [0].Rove ();
						colony.RoversInColony [0].Rove ();
						roverMoved = true;
				}
		}
}