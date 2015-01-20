﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace AC
{
		public class EventArg<T> : EventArgs
		{
				private T m_currentValue;

				public T CurrentValue{ get { return m_currentValue; } }

				public EventArg (T currentValue)
				{
						m_currentValue = currentValue;
				}
    
		}
}
