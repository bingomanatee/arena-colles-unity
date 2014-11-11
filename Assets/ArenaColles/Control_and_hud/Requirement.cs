using UnityEngine;
using System.Collections;
using WLLForms;

namespace ArenaColles
{
		public class Requirement : MonoBehaviour
		{
				public string resourceName = "";
				public int quantityRequired = 1;
				public int quantityCommitted = 0;
				bool isSatisfied_ = false;

				public	bool IsSatisfied {
						get { return isSatisfied_; }
						set {
								if (value) {
										quantityCommitted = quantityRequired;
								} else {
										quantityCommitted = 0;
								}
								isSatisfied_ = value;
								if (Task)
										Task.CheckReqs ();
						}
				}

				public TaskPanel task_;

				public TaskPanel Task {
						get {
								return task_;
						}
						set { 
								task_ = value;
						}
				}

				public Checkbox commitCheckbox;

				// Use this for initialization
				void Start ()
				{
						if (!commitCheckbox)
								Debug.Log ("No checkbox for " + resourceName);
						commitCheckbox.CheckChangedEvent += CommitChanged;
				}
	
				// Update is called once per frame
				void Update ()
				{
						//Debug.Log (string.Format ("Resource {0} is {1}", resourceName, IsSatisfied));
				}
	
				//public Checkbox box;
	
				void CommitChanged (bool isChecked)
				{
						Debug.Log (string.Format ("Req checked: {0} = {1}", resourceName, isChecked));
						IsSatisfied = isChecked;
				}
		}
}