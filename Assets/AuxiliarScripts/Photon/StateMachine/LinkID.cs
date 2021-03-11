using System.Reflection;

namespace StateMachine
{
	//Link value, it has a methodinfo with the link method and the node he is pointing at it
	public class LinkID
	{
		/// <summary>
		/// The method info with the link execution question.
		/// </summary>
		public MethodInfo link;

		/// <summary>
		/// The node pointed by this link.
		/// </summary>
		public NodeID target;

		/// <summary>
		/// node who owns this link
		/// </summary>
		public NodeID caster;

		/// <summary>
		/// Initializes a new instance of the <see cref="StateMachine.Engine2+LinkID"/> class. 
		/// </summary>
		public LinkID(MethodInfo link, NodeID target, NodeID caster)
		{
			this.link = link;
			this.target = target;
			this.caster = caster;
		}
	}
}
