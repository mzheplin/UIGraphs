using Categories;

namespace UnambiguityChecker
{
	public static class DLMGHomomorphismExtensions
	{
		public static bool IsLocallyInjectiveOnLabeles(this DLMGHomomorphism hom)
		{
            var sourceGraph = hom.Source;

            return sourceGraph.Vertices.All(vertex =>
            {
                var localEdges = sourceGraph.Edges.Where(edge => edge.Tail == vertex).ToList();
                var edgeCount = localEdges.Count;

                return edgeCount < 2 ||
                    !localEdges.SelectMany((edge1, i) =>
                        localEdges.Skip(i + 1).Select(edge2 =>
                            new { Edge1 = edge1, Edge2 = edge2 }))
                        .Any(pair =>
                            //hom.Vertex_Map[pair.Edge1.Head] == hom.Vertex_Map[pair.Edge2.Head] &&
                            pair.Edge1.Label != pair.Edge2.Label &&
                            hom.Edge_Labeles_Map[pair.Edge1.Label] == hom.Edge_Labeles_Map[pair.Edge2.Label]);
            });
        }

		public static bool IsInjectiveOnVertices(this DLMGHomomorphism hom)
		{
			var unique = hom.Vertex_Map.Values.Distinct().Count();
			var all = hom.Vertex_Map.Values.Count();
			return unique == all;
		}
	}
}

