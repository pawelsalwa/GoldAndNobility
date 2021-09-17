using System;

namespace Common.Attributes
{
	/// <summary> Used to create persistent components before scene is loaded. </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class PersistentComponentAttribute : Attribute
	{
		
	}
}