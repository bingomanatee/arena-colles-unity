
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ArenaColles
{
		public class Rover : MonoBehaviour
		{

				// Use this for initialization
				void Start ()
				{
	
				}
	
				// Update is called once per frame
				void Update ()
				{
	
				}

		#region cell

				Cell cell_;

				public Cell Cell {
						get { return cell_; }
						set { 
								cell_ = value;
								cell_.revealed = true;
								gameObject.transform.position = cell_.position;
								
						}
				}

				bool hasCell { get { return Cell != null; } }

		#endregion
		
				struct RoveRating
				{
						public Cell cell;
						
						public RoveRating (Cell cell_)
						{
								cell = cell_;
						}
						
						public int rating {
								get {
										int r = 0;
			
										if (!cell.revealed)
												r += 2;
										foreach (Cell n in cell.Neighbors.Values) {
												if (n == null) 
														continue;
												if (!n.revealed)
														++r;
										}
										return r;
								}
						}
				}

				public void Rove ()
				{
						if (!hasCell)
								return;

						Dictionary<string, Cell> neighbors = Cell.Neighbors;
						
						List<RoveRating> ratings = new List<RoveRating> ();
						string[] keys = new string[neighbors.Keys.Count];
						neighbors.Keys.CopyTo (keys, 0);			
						string bestDirection = keys [0];
						
						RoveRating bestRating = new RoveRating (neighbors [bestDirection]);
						
						foreach (string direction in neighbors.Keys) {
								Cell n = neighbors [direction];
								RoveRating r = new RoveRating (n);
								ratings.Add (r);
								if (r.rating > bestRating.rating) {
										bestRating = r;
										bestDirection = direction;
								}
						}
						Rove (bestDirection);

				}
				
				public void Rove (string dir)
				{
						if (!hasCell)
								return;
						Cell = Cell.Neighbors [dir];
				}
		}
}