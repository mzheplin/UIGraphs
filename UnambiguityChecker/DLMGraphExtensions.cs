using Categories;
namespace UnambiguityChecker
{
	public static class DLMGraphExtensions
	{
		public static bool IsDeterministic(this DLMGraph graph)
		{
            foreach (var vertex in graph.Vertices)
            {
                var localEdges = graph.Edges.Where(edge => edge.Tail == vertex).ToList();

                var edgeNum = localEdges.Count();
                if (edgeNum < 2) continue;

                for (int i = 0; i < edgeNum; i++)
                {
                    for (int j = i + 1; j < edgeNum; j++)
                    {
                        if (localEdges[i].Label == localEdges[j].Label
                           && localEdges[i].Head != localEdges[j].Head)
                            return false;
                    }
                }
            }
            return true;
		}
	}
}

