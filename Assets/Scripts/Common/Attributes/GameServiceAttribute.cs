using System;

namespace Common.Attributes
{
	/// <summary> Used to create persistent components before scene is loaded. </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class GameServiceAttribute : Attribute
	{

		/// <summary> if specified, this will be registered as service in ServiceLocator </summary>
		public readonly Type[] serviceTypes;
		
		public GameServiceAttribute(params Type[] serviceTypes)
		{
			this.serviceTypes = serviceTypes;
		}
	}
}