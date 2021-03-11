using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace StateMachine
{
	public static class MagicDictionary
	{
		///Dictionary id, it got a state machine name and a who built it
		public class MagicDictionaryKeyID
		{
			/// <summary>
			/// The name of the state machine.
			/// </summary>
			public string name;
			
			/// <summary>
			/// The class who built this state machine.
			/// </summary>
			public Type baseCasterType;
			
			/// <summary>verify if this object is equal to another.</summary>
			/// <returns><c>true</c>, true if it is equal, <c>false</c> otherwise.</returns>
			public bool isEqual (MagicDictionaryKeyID obj)
			{	
				return (name == obj.name && baseCasterType == obj.baseCasterType);
			}
			
			/// <summary>
			/// Initializes a new instance of the magic dictionary id <see cref="StateMachine.Engine2+MagicDictionaryKeyID"/> class.
			/// </summary>
			public MagicDictionaryKeyID(string name, Type casterType)
			{
				this.name = name;
				this.baseCasterType = casterType;
			}
		}
		
		/// <summary>
		/// The magic dictionary. with all state machies architectures blueprints
		/// </summary>
		private static Dictionary<MagicDictionaryKeyID, List<NodeID>> magicDictionary;
		
		/// <summary>Fills the magic dictionary link lists.</summary>
		/// <param name="id">Identifier in the magic dictionary to fill its nodes.</param>
		private static void FillMagicDictionaryLinkLists(MagicDictionaryKeyID pageId)
		{
			foreach (NodeID node in magicDictionary[pageId]) 										//Para cada node na pagina do dicionario magico
			{
				foreach (MethodInfo link in node.nodeType.GetMethods())									//Para cara metodo q o node tiver 									
				{																							//Salva a lista de strings do link
					LinkToAttribute linkTo = (LinkToAttribute)Attribute.GetCustomAttribute(link,typeof(LinkToAttribute));

					if (linkTo!=null)																		//Se o metodo usar a tag link
					{
						if (linkTo.name == "All")																//se o link usar o nome All 
						{
							if (link.GetCustomAttributes(typeof(ReverseAttribute),false).Length >= 1)				//se o link usar a tag Reverse
								throw new LinkWithAllAndReverseAttributesException("Links do tipo All n podem ter o atributo [Reverse]");					//acusa um erro

							Type[] mask = new Type[0];															//Inicia a mascara

							if (node.nodeType.GetCustomAttributes(typeof(MaskAttribute),false).Length>1)			//se estiver usando a mascara																		
								mask = ((MaskAttribute)Attribute.GetCustomAttribute(link,typeof(MaskAttribute))).mask;	//salva a lista de strings da mascara			
							
							foreach (NodeID omniReceiverNode in magicDictionary[pageId])   							//Para cada node na pagina, implementa um link inverso para o node atual da busca
								if  (!mask.Contains(omniReceiverNode.nodeType))									//Se a mascara nao conter o nome do nodo corrente 
									omniReceiverNode.LinkList.Add(new LinkID(link, node, node));				//Implementa um node inverso		
						}

						else if (link.GetCustomAttributes(typeof(ReverseAttribute),false).Length >= 1)			//ou se o link usar a tag Reverse
						{
							NodeID auxNodeId = SearchNodeWithName(pageId, linkTo.name);
							auxNodeId.LinkList.Add (new LinkID(link, node, node ));								//busca o node com o mesmo nome do link e implementa nesse node um link para o node atual da busca
						}
						else 																					//ou o link n usar nenhuma tag ou nome especial
						{
							NodeID auxNodeId = SearchNodeWithName(pageId,linkTo.name);
							node.LinkList.Add (new LinkID(link, auxNodeId, node));								//implementa no node atual da busta um link para um outro node com o mesmo nome
						}
					}
				}
			}
		}
		
		/// <summary>Searchs the node with the given name </summary>
		/// <returns>The node with name.</returns>
		/// <param name="id">Identifier in the magic dictionary.</param><param name="name">Name to be search.</param>
		private static NodeID SearchNodeWithName (MagicDictionaryKeyID pageId, string name)
		{
			foreach (NodeID node in magicDictionary[pageId])
				if (node.nodeType.Name == name)
					return node;
			
			throw new MissingNodeException("Node com nome inexistente... " + name);
		}
		
		/// <summary>Gets a page in the magic dictionary.</summary>
		/// <returns>The page in magic dictionary.</returns>
		/// <param name="id">Page identifier.</param>
		public static List<NodeID> GetAPageInMagicDictionary(MagicDictionaryKeyID pageId)
		{
			List<NodeID> nodeList;										//Referencia para a variavel de retorno
			
			if (magicDictionary == null)								//se o dicionario n existir
			{
				magicDictionary  =  new Dictionary<MagicDictionaryKeyID, List<NodeID>>();		//cria ele
				
				nodeList = BuildMagicDictionaryPageContent(pageId);					//pega a lista de nodes e salva na variavel de retorno
				
				AddAPageInMagicDictionary(pageId,nodeList);						//coloca ela no dicionario
			}
			else if (!MagicDictionaryContains(pageId))						//se ele existir e n estiver no dicionario
			{
				nodeList = BuildMagicDictionaryPageContent(pageId);					//pega a lista de nodes e salva na variavel de retorno
				
				AddAPageInMagicDictionary(pageId,nodeList);						//coloca ela no dicionario
			}
			else 														//se ele existir e existir no dicionario
			{
				nodeList = magicDictionary[pageId];								//salva a lista na variavel de retorno
			}
			return nodeList;											//retorna a variavel de retorno
		}
		
		/// <summary>Adds A page in the magic dictionary.</summary>
		/// <returns><c>true</c>, if A page in magic dictionary was added, <c>false</c> otherwise.</returns>
		/// <param name="id">Page identifier.</param> param name="nodeList">Page content.</param>
		private static void AddAPageInMagicDictionary (MagicDictionaryKeyID pageId, List<NodeID> nodeList)
		{
			if (nodeList.Count > 0)						//se a lista de nodos existir de verdade
			{
				magicDictionary.Add (pageId, nodeList);			//adiciona no dicionario
				
				FillMagicDictionaryLinkLists(pageId);			//preenche as listas de links nos nodes
			}
			else 										//se a lista de nodos n existir
				throw new NullStateMachineException("A maquina de estados de nome " + pageId.name + " Nao contem nenhum nodo... voce n errou o nome dela?"); 
		}
		
		///<summary> If the magics the dictionary contains certain . </summary>
		/// <returns><c>true</c>, if dictionary contains the page with the identifier, <c>false</c> otherwise.</returns>
		private static bool MagicDictionaryContains(MagicDictionaryKeyID soughtPage)
		{
			bool result = false;
			
			foreach (KeyValuePair<MagicDictionaryKeyID,List<NodeID>> page in magicDictionary) 
				if (page.Key.isEqual(soughtPage))
					result = true;
			
			return result;
		}
		
		/// <summary>Builds a page content getting a reflection from caster's type and build an avaliable node list </summary>
		/// <returns>The node list.</returns>
		/// <param name="name">State machine name.</param> <param name="caster">User's caster.</param> <param name="initialNode">Initial node.</param> <typeparam name="T">User's type.</typeparam>
		private static List<NodeID> BuildMagicDictionaryPageContent(MagicDictionaryKeyID pageId)
		{
			List<NodeID> nodeList = new List<NodeID>();				//Lista que vai ser retornada
			List<NodeID> overrideNodeList = new List<NodeID>();		//Lista com metodos q estao bloqueando outros
			bool initialNodeNotFound = true;						//variavel de controle q marca se o node inicial n foi encontrado
			bool wasNotOverriden = true;							//variavel de controle q marca se o node vai ou n ser sobrecarregado
			
			
			for (Type baseType = pageId.baseCasterType; baseType != null; baseType = baseType.BaseType)				//Para cada classe na hierarquia
			{
				foreach (Type node in baseType.GetNestedTypes(BindingFlags.Public))										//Para cada classe aninhada
				{
					if (node.IsSubclassOf(typeof(Node))) 																//verifica se a classe eh um node
					{																										//Salva o atributo NodeFrom para verificaçao
						NodeFromAttribute attribute = (NodeFromAttribute)Attribute.GetCustomAttribute(node,typeof(NodeFromAttribute),false);
						
						if (attribute == null) 																				//Verifica se ele NAO tem o atributo
						{
							throw new MissingNodeFromAttributeException("Node " + node.FullName + " sem tag de identificaçao [NodeFrom(string name)]!!!");	//Manda uma msg de erro
						}	
						else if (attribute.name == pageId.name)																	//verifica se o node tem a identificaçao da maquina de estados
						{
							wasNotOverriden = true;																				//Set a variavel de controle wasNotOverriden como true antes de começar a verificaçao
							
							foreach (NodeID overridenNode in overrideNodeList)													//Para cada nodo na lista de nodos que estao sobrecarregando outros
								if (overridenNode.nodeType.Name == node.Name)														//se o nodo tive o nome dele na lista negra
									wasNotOverriden = false;																			//seta esse nodo como Overriden (pulando os proximos passos)
							
							if (wasNotOverriden)																					//se n foi sobrecarregado
							{
								if (node.GetCustomAttributes(typeof(OverrideAttribute),false).Length >= 1)								//se ele tiver a tag override
								{
									overrideNodeList.Add (new NodeID(node, baseType));														//Entra na lista negra, marcando q ele vai sobrecarregar alguem
									nodeList.Add 		 (new NodeID(node, baseType));														//Entra na lista comum de nodes
								}
								else if (initialNodeNotFound && node.GetCustomAttributes(typeof(InitialAttribute),false).Length >= 1)	//Se o nodo inicial n foi achado, e esse nodo usar a tag [initial]
								{
									nodeList.Insert(0,new NodeID(node, baseType));															//salva o node inicial como o primeiro da lista
									initialNodeNotFound = false;																			//marca o nodo inicial como encontrado
								}
								else 																									//se ele n tiver nenhuma tag especial
								{
									nodeList.Add (new NodeID(node, baseType));																//entra na lista comum de nodes
								}	
							}
						}
					}
				}
			}
			if (initialNodeNotFound)																				//se o node inicial n foi encontrado... acusa um erro
				throw new MissingInitialNodeException("Maquina de estados de nome \""+pageId.name +"\" nao tem nenhum node com o atributo [Initial]");

			return nodeList;
		}
		
	}
}
