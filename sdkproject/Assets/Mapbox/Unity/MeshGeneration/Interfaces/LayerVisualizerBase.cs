namespace Mapbox.Unity.MeshGeneration.Interfaces
{
    using Mapbox.VectorTile;
    using UnityEngine;
    using Mapbox.Unity.MeshGeneration.Data;
	using Mapbox.Unity.Map;
	using System;

	/// <summary>
	/// Layer visualizers contains sytling logic and processes features
	/// </summary>
	public abstract class LayerVisualizerBase : ScriptableObject
    {
        public bool Active = true;
        public abstract string Key { get; set; }
        public abstract void Create(VectorTileLayer layer, UnityTile tile);

		public ModuleState State { get; private set; }
		private int _progress;
		protected int Progress
		{
			get
			{
				return _progress;
			}
			set
			{
				if (_progress == 0 && value > 0)
				{
					State = ModuleState.Working;
					OnVisualizerStateChanged(this);
				}
				if (_progress > 0 && value == 0)
				{
					State = ModuleState.Finished;
					OnVisualizerStateChanged(this);
				}
				_progress = value;
			}
		}
		public event Action<LayerVisualizerBase> OnVisualizerStateChanged = delegate { };
	}
}
