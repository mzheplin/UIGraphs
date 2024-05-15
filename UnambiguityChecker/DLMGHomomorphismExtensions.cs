using Categories;

namespace UnambiguityChecker
{
	public static class DLMGHomomorphismExtensions
	{
		public static bool IsLocallyInjective(this DLMGHomomorphism hom)
		{
			var sourceGraph = hom.Source;

			foreach (var vertex in sourceGraph.Vertices)
			{
				var localEdges = sourceGraph.Edges.Where(edge => edge.Tail == vertex).ToList();

				var edgeNum = localEdges.Count();
				if (edgeNum < 2) continue;

				for (int i = 0; i < edgeNum; i++)
				{
					for (int j = i + 1; j < edgeNum; j++)
					{
						var edge1 = localEdges[i];
						var edge2 = localEdges[j];

						var t = hom.Vertex_Map[edge1.Head] == hom.Vertex_Map[edge2.Head];
						var l = edge1.Label == edge2.Label;
						var m = hom.Edge_Labeles_Map[edge1.Label] == hom.Edge_Labeles_Map[edge2.Label];

						if (t && !l && m) return false;
					}
				}
			}

			return true;
		}

		public static bool IsInjectiveOnVertices(this DLMGHomomorphism hom)
		{
			var unique = hom.Vertex_Map.Values.Distinct().Count();
			var all = hom.Vertex_Map.Values.Count();
			return unique == all;
		}
	}
}

