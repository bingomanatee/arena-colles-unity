using UnityEngine;
using System.Collections;

namespace ArenaColles
{
		public class TaskResource
		{
				public Task task;
				public string resourceName;
				public int quantity;

				public TaskResource (Requirement req, Task task_)
				{
						resourceName = req.resourceName;
						task = task_;
						quantity = req.quantityCommitted;
				}
		}
		
}