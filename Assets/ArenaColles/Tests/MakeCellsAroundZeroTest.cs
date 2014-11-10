using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityTest.IntegrationTestRunner;

namespace ArenaColles
{
		public class MakeCellsAroundZeroTest : MonoBehaviour
		{
				public GameObject markerTemplate;
				public TerraGen terraGen;
				public GameObject centerTemplate;
				bool drawn = false;

				struct Target
				{
						public int i;
						public int j;
						public bool found;

						public Target (int i_, int j_, bool found_ = false)
						{
								i = i_;
								j = j_;
								found = found_;
						}

						public bool Equals (Cell c)
						{
								return c.i == i && c.j == j;
						}
				}

				// Use this for initialization
				void Start ()
				{
					
				}
	
				// Update is called once per frame
				void Update ()
				{
						if (!drawn)
								Draw ();
				}

				void Draw ()
				{
					
						DrawCells (0, 0, new Target[] {
							new Target (-10, -9),
							new Target (-9, -10)
        				});

						DrawCells (TerraGen.X_SIZE, TerraGen.Z_SIZE, new Target[] {
				new Target (0, 1),
				new Target (0, -1),
				new Target (1, 0),
				new Target (-1, 0),
				new Target (-1, 1),
				new Target (-1, -1)
			});

						drawn = true;
				}

				void DrawCells (int i, int j, Target[] targets)
				{
						Cell cell = terraGen.GetCell (i, j);
						cell.revealed = true;
						Instantiate (centerTemplate, cell.transform.position, Quaternion.identity);

						Dictionary<string, Cell> neighbors = cell.Neighbors;

						int hits = 0;
						foreach (string s in neighbors.Keys) {
								Cell neighbor = neighbors [s];
								neighbor.revealed = true;
								Debug.Log ("Neighbor: " + neighbor.ToString ());
								GameObject flag = ((GameObject)Instantiate (markerTemplate, neighbor.transform.position, Quaternion.identity));
								flag.name = s;
								flag.GetComponent<Flag> ().Cell = neighbor;

								bool found = false;
								Target foundTarget;

								foreach (Target target in targets) {
										if (!found && (!target.found) && target.Equals (neighbor)) {
												found = true;
												foundTarget = target;
												++hits;
										}
								}

								if (found)
										foundTarget.found = true;

								if (!found) {
										IntegrationTest.Fail (gameObject, "bad neighbor found at " + neighbor.ToString ());
								}
						}

						if (hits != targets.Length) {
								IntegrationTest.Fail (gameObject, "Not all targets found");
						}

				}
		}
}