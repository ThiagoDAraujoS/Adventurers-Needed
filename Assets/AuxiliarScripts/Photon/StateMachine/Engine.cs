using System;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
	public class Engine
	{
		/// <summary>
		/// The name of this state machine.
		/// </summary>
		private string name;

		/// <summary>
		/// The current active node. 
		/// </summary>
		public Node activeNode;

		/// <summary>
		/// The last node before the active node becomes the active one.
		/// </summary>
		public Node lastNode;

		/// <summary>
		/// All nodes listed by their names.
		/// </summary>
		public Dictionary<string,Node> nodes;

		/// <summary>
		/// Builds a new instance of the <see cref="StateMachine.Engine2"/> together with the whole state machine structure class.
		/// </summary>
		/// <param name="name">State machine name.</param>
		/// <param name="caster">Who owns the state machine.</param>
		public Engine(string name, System.Object caster)
		{
			this.name = name;

			List<NodeID> blueprint = MagicDictionary.GetAPageInMagicDictionary(new MagicDictionary.MagicDictionaryKeyID(name,caster.GetType()));			//Pega a blueprint da maquina de estados no dicionario magico

			Dictionary<NodeID, Node> nodeReferenceTable = new  Dictionary<NodeID, Node>();								//Inicia uma lista com todos os nodes e suas respectivas build.

			nodes = new Dictionary<string, Node>();

			Node aux;																										//Inicia uma variavel de auxilio

			foreach (NodeID nodeInfo in blueprint) 																			//Inicia os nodes na lista de referencia de nodes.
			{
				aux = (Node)Activator.CreateInstance(nodeInfo.nodeType);														//Cria a instancia do nodo

				aux.m = caster;																									//Passa o script q esta construindo a maquina
				
				nodeReferenceTable.Add(nodeInfo, aux);																			//Adciona o nodo na tabua de referencia

				nodes.Add(nodeInfo.nodeType.Name, aux);																			//Salva a lista final de nodes para consulta
			}
	
			foreach (KeyValuePair<NodeID,Node> node in nodeReferenceTable)													//Para cada node na lista de referencia.
			{
				node.Value.links = new Dictionary<Link, Node>();															//Inicia a lista de links do nodo.

				foreach (LinkID link in node.Key.LinkList) 																	//para cada link dentro de cada build na lista de nodes.
					node.Value.links.Add((Link)Delegate.CreateDelegate(typeof(Link),nodeReferenceTable[link.caster],link.link),nodeReferenceTable[link.target]); //Cria uma pagina no dicionario do node contendo um delegate e uma referencia a um outro nodo.
			}

			activeNode = nodeReferenceTable[blueprint[0]];																	//Seta o nodo inicial como o primeiro nodo da blueprint
			lastNode = activeNode;

			foreach (KeyValuePair<NodeID,Node> node in nodeReferenceTable)													//Acorda todos os nodos
				node.Value.Awake();

			activeNode.Start();	 																							//Roda o start do nodo inicial da maquina
		}

		/// <summary>
		/// Sets the active node, running the end step from previous node and the start step from the new one.
		/// </summary>
		/// <param name="newNode">New node.</param>
		public void SetActiveNode(Node newNode)
		{
			string attName = ((NodeFromAttribute)Attribute.GetCustomAttribute(newNode.GetType(),typeof(NodeFromAttribute),false)).name;

			if (attName == name)
			{
				activeNode.End();
				lastNode = activeNode;
				activeNode = newNode;
				activeNode.Start();
			}
			else
			{
				throw new LinkNotBelongToStateMachineException("Nodo " + attName + " invalido para a maquina " + name);
			}
		}

		/// <summary>
		/// Run this active node update and check if its links for a swap
		/// </summary>
		public void Run()
		{
			activeNode.Update();
			CheckConditions();
		}

		/// <summary>
		/// Checks the active node conditions.
		/// </summary>
		private void CheckConditions()
		{
			foreach (var link in activeNode.links) 
				if (link.Key())
				{
					SetActiveNode(link.Value);
					break;
				}
		}
	}
}