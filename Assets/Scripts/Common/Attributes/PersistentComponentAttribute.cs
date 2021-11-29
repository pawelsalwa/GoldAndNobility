using System;

namespace Common.Attributes
{
	/// <summary> Used to create persistent components before scene is loaded. </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class PersistentComponentAttribute : Attribute
	{

		/// <summary> if specified, this will be registered as service in ServiceLocator </summary>
		public readonly Type serviceType;

		public PersistentComponentAttribute(Type serviceType = null)
		{
			this.serviceType = serviceType;
		}
	}
}