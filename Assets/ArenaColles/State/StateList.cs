using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ArenaColles
{
		public class StateList
		{

#region properties
				static Dictionary<string, StateList> lists = new Dictionary<string, StateList> ();
				public string name;
				public StateListItem[] items;
				public Dictionary<StateListItem, StateListItem[]> controlledStateChanges = new Dictionary<StateListItem, StateListItem[]> ();
		
		#endregion

#region constructors
		
				public StateList (string n, params string[] i)
				{
						name = n;
						List<StateListItem> iList = new List<StateListItem> ();
						foreach (string ii in i) {
								iList.Add (new StateListItem (ii, this));
						}
						items = iList.ToArray ();
						lists.Add (n, this);
				}

				public static StateList Create (string n, params string[] i)
				{
						return new StateList (n, i);
				}

/**
 * this method replaces common boilerplate; it both ensures that a list exists and is defined,
 * and returns a new single state for the given name.
 */
				public static State Init (string n, params string[] i)
				{
						if (!HasList (n))
								Create (n, i);
						return new State (n);
				}

		#endregion

#region list management
		
				public static bool HasList (string name)
				{
						return lists.ContainsKey (name);
				}

				public static void RemoveState (string name)
				{
						if (lists.ContainsKey (name))
								lists.Remove (name);
				}
		
				public static StateList GetList (string name)
				{
						return lists [name];
				}

/// <summary>
/// Removes all predefined lists from the state registry; probably only need to do this in a test context.
/// </summary>
				public static void Clear ()
				{
						lists.Clear ();
				}
		#endregion

#region accessors
// returns a stored list of states

				public StateListItem Item (string name)
				{
						foreach (StateListItem item in items) {
								if (item.Equals (name))
										return item;
						}
						return null;
				}

				public bool Contains (string name)
				{
						return Item (name) != null;
				}

				public StateListItem First ()
				{
						return items [0];
				}

				public StateList Constrain (string toName, params string[] fromNames)
				{
						List<StateListItem> fromItems = new List<StateListItem> ();
						StateListItem toItem = null;
						foreach (StateListItem nameItem in items) {
								if (nameItem.Equals (toName)) {
										toItem = (nameItem);
								}
						}
						if (toItem == null)
								throw new UnityException ("Cannot find item " + toName);

						foreach (string fromName in fromNames) {
								foreach (StateListItem nameItem in items) {
										if (nameItem.Equals (fromName)) {
												fromItems.Add (nameItem);
										}
								}
						}
						controlledStateChanges.Add (toItem, fromItems.ToArray ());

						return this;
				}

		#endregion
		}

}