using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ArenaColles
{
		public class Colony : MonoBehaviour
		{
				TerraGen.Cell cell_;
				public Colonist colonistTemplate;
				public List<Colonist> ColonistsInColony = new List<Colonist> ();
				public GameObject focus;
				public bool showColonists;

				public TerraGen.Cell cell {
						set {
								cell_ = value;
								transform.parent = cell.g.transform;
								transform.localScale = Vector3.one;
								transform.localPosition = Vector3.zero;
						}
						get { return cell_;}
				}

				public int Colonists {
						get { return ColonistsInColony.Count;}
						set {
								value = Mathf.Max (0, value);
								while (ColonistsInColony.Count > value)
										ColonistsInColony.RemoveAt (0);
								if (colonistTemplate)
										while (ColonistsInColony.Count < value) {
										
												GameObject newCol = (GameObject)Instantiate (colonistTemplate.gameObject);
												Colonist c = newCol.GetComponent<Colonist> ();
												newCol.transform.parent = transform;
												newCol.transform.localPosition = Vector3.zero;
												newCol.transform.localScale = Vector3.one;
												ColonistsInColony.Add (c);
										}
								RedistributeColonists ();
						}
				}

#region loops
		
				// Use this for initialization
				void Start ()
				{
						++colCount;
						colonyID = colCount;
				}
		
				// Update is called once per frame
				void Update ()
				{
			
				}

#endregion

				public void SetCell (TerraGen.Cell cell__)
				{
						cell = cell__;
				}

				void OnMouseDown ()
				{
						if (Game.game.ActiveColony != this)
								Game.game.ActiveColony = this;
				}

		#region identity 
		
				static int colCount = 0;
				int colonyID = 0;

				public string ColonyName { get { return "Colony " + colonyID; } }

				void RedistributeColonists ()
				{
						if (ColonistsInColony.Count < 1)
								return;
						int angle = 0;
						int inc = 360 / ColonistsInColony.Count;

						foreach (Colonist c in ColonistsInColony) {
								GameObject col = c.gameObject;
								col.transform.localRotation = Quaternion.identity;
								col.transform.Rotate (new Vector3 (0, angle, 0));
								angle += inc;
						}
				}

		#endregion

		}
}
