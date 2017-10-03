namespace Mapbox.Unity.Map
{
	using Mapbox.Unity.Utilities;
	using Utils;
	using Mapbox.Map;
	using UnityEngine;
	using System.Diagnostics;

	public class BasicMap : AbstractMap
	{
		private Stopwatch sw;

		public override void Initialize(Vector2d latLon, int zoom)
		{
			_worldHeightFixed = false;
			_centerLatitudeLongitude = latLon;
			_zoom = zoom;

			var referenceTileRect = Conversions.TileBounds(TileCover.CoordinateToTileId(_centerLatitudeLongitude, _zoom));
			_centerMercator = referenceTileRect.Center;

			sw = new Stopwatch();
			sw.Reset();
			sw.Start();

			_worldRelativeScale = (float)(_unityTileSize / referenceTileRect.Size.x);
			_mapVisualizer.Initialize(this, _fileSouce);
			_tileProvider.Initialize(this);

			SendInitialized();

			MapVisualizer.OnMapVisualizerStateChanged += (s) =>
			{
				if (s == ModuleState.Finished)
				{
					sw.Stop();
					UnityEngine.Debug.Log(sw.ElapsedMilliseconds);
				}
			};
		}
	}
}