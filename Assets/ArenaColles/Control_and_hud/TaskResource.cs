using UnityEngine;
using System.Collections;

namespace ArenaColles
{
		public class TaskResource
		{
				public Task task;
				public string resourceName;
				public int quantity;

				public TaskResource (Requirement req, Task @task)
				{
						resourceName = req.resourceName;
						task = @task;
						quantity = req.quantityCommitted;
				}
		}
		
		
}