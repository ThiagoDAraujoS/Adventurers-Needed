using System;

namespace StateMachine
{
	/// <summary>Custon link delegate.</summary>
	public delegate bool Link();

	/// <summary>  Um dos nodos com esse atributo vira o nodo inicial da maquina de estados. </summary>
	[AttributeUsage (AttributeTargets.Class, AllowMultiple = false)]
	public class InitialAttribute : Attribute{}

	/// <summary> O Link Marcado com esse atributo eh implementado na clase q ele aponta para a clase q ele esta </summary>
	[AttributeUsage (AttributeTargets.Method, AllowMultiple = false)]
	public class ReverseAttribute : Attribute{}

	/// <summary> O nodo marcado com esse atributo sobrepoe um nodo ja previamente passado na lista (por herança proavelmente) </summary>
	[AttributeUsage (AttributeTargets.Class, AllowMultiple = false)]
	public class OverrideAttribute : Attribute{}	

	/// <summary> Demarca um link. </summary>
	[AttributeUsage (AttributeTargets.Method, AllowMultiple = false)]
	public class LinkToAttribute : Attribute
	{
		public string name; 
		public LinkToAttribute(string name)	{	this.name = name;}
		public LinkToAttribute(Type name)	{	this.name = name.Name;}
	}
	
	/// <summary> Demarca links para serem bloqueados no omni cast. </summary>
	[AttributeUsage (AttributeTargets.Method, AllowMultiple = false)]
	public class MaskAttribute : Attribute
	{
		public Type[] mask;
		public MaskAttribute(params Type[] mask)	{	this.mask = mask;}
	}

	/// <summary> Demarca que qual maquina de estados esse nodo faz parte. </summary>
	[AttributeUsage (AttributeTargets.Class, AllowMultiple = false)]
	public class NodeFromAttribute : Attribute
	{
		public string name;
		public NodeFromAttribute(string name)	{	this.name = name;}
	}

	/// <summary>
	/// Link does not belong to current state machine exception.
	/// </summary>
	public class LinkNotBelongToStateMachineException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:LinkNotBelongToStateMachineException"/> class
		/// </summary>
		public LinkNotBelongToStateMachineException (string message) : base(message){}
	}

	/// <summary>
	/// Link with all and reverse attributes exception.
	/// </summary>
	public class LinkWithAllAndReverseAttributesException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:LinkWithAllAndReverseAttributesException"/> class
		/// </summary>
		public LinkWithAllAndReverseAttributesException (string message) : base(message){}
	}

	/// <summary>
	/// Missing initial node exception.
	/// </summary>
	public class MissingInitialNodeException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MissingInitialNodeException"/> class
		/// </summary>
		public MissingInitialNodeException (string message) : base(message){}
	}

	/// <summary>
	/// Missing [node from] attribute exception.
	/// </summary>
	public class MissingNodeFromAttributeException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MissingNodeFromAttributeException"/> class
		/// </summary>
		public MissingNodeFromAttributeException (string message) : base(message){}
	}

	/// <summary>
	/// Null state machine exception.
	/// </summary>
	public class NullStateMachineException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:NullStateMachineException"/> class
		/// </summary>
		public NullStateMachineException (string message) : base(message){}
	}

	/// <summary>
	/// Missing node exception.
	/// </summary>
	public class MissingNodeException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MissingNodeException"/> class
		/// </summary>
		public MissingNodeException (string message) : base(message){}
	}

	/// <summary>
	/// Duplicated node name exception.
	/// </summary>
	public class DuplicatedNodeNameException : Exception
	{		
		/// <summary>
		/// Initializes a new instance of the <see cref="T:DuplicatedNodeNameException"/> class
		/// </summary>
		/// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
		public DuplicatedNodeNameException (string message) : base (message){}
	}
}