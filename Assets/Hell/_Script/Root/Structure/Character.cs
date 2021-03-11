using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Hell.Display;
using System.Collections;
using System;

namespace Hell
{
    /// <summary>
    /// This class describes a character
    /// </summary>
    public class Character : Pawn
    {
		public Renderer Renderer { get; private set; }

        [SerializeField]
        private Renderer[] coloredObjects;
        private Color color;
        public Color Color
        {
            get { return color; }
            set {
                color = value;
                foreach (Renderer renderer in coloredObjects)
                    renderer.material.color = color;
            }
        }

		protected override void Initialize (Color color, Transform parent)
		{
            base.Initialize (color, parent);

            Color = color;
            //Renderer = GetComponentInChildren<Renderer> ();
            //Renderer.material.color = color;

            transform.SetParent(parent, true);

			foreach (var rune in RunePrefab)
				Runes.Add (rune.Instantiate<PlayerAction> ());

			walk = walkPrefab.Instantiate<PlayerAction> ();
			walk.transform.SetParent(parent, true);

            wait = waitPrefab.Instantiate<PlayerAction> ();
			wait.transform.SetParent(parent, true);

        }


		public PlayerAction this [int index] {
			get {
				if (index < Runes.Count)
					return Runes [index];
				else if (index == Runes.Count)
					return walk;
				else
					return wait;
			}
		}

		[SerializeField]
		private GameObject[] RunePrefab;

		[SerializeField]
		private GameObject
			walkPrefab,
			waitPrefab;

		/// <summary>
		/// Runes equiped in the character
		/// </summary>
		[HideInInspector]
		public List<PlayerAction> Runes;

		[HideInInspector]
		public PlayerAction
			walk,
			wait;

	}

}