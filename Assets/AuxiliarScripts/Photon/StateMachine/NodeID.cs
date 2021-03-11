using System;
using System.Collections.Generic;

namespace StateMachine
{
	//Dictionary value, it got a nodetype and who has his nested class
	public class NodeID
	{
		/// <summary>
		/// The type of the node.
		/// </summary>
		public Type nodeType;
		
		/// <summary>
		/// Who has the nodeType nested in its architecture
		/// </summary>
		public Type nestedClassHolder;
		
		/// <summary>
		/// The link with other nodes's list.
		/// </summary>
		public List<LinkID> LinkList;
		
		/// <summary>
		///verify if this object is equal to another.
		///</summary>
		/// <returns><c>true</c>, true if it is equal, <c>false</c> otherwise.</returns>
		public bool isEqual (NodeID obj)
		{
			return (nodeType == obj.nodeType && nestedClassHolder == obj.nestedClassHolder);
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="StateMachine.Engine2+NodeID"/> class.
		/// </summary>
		public NodeID(Type nodeType, Type nestedClassHolder)
		{
			this.nodeType = nodeType;
			this.nestedClassHolder = nestedClassHolder;
			this.LinkList = new List<LinkID>();
		}
	}	
}
