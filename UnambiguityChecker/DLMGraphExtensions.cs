using Categories;
namespace UnambiguityChecker
{
	public static class DLMGraphExtensions
	{
		public static bool IsDeterministic(this DLMGraph graph)
		{
            var duplicateEdges = graph.Edges
                                .GroupBy(edge => new { edge.Tail, edge.Label }) 
                                .Where(group => group.Count() > 1) 
                                .SelectMany(group => group); 

            return !duplicateEdges.Any();
		}


		public static DLMGHomomorphism GenerateIsomorphic(this DLMGraph graph)
		{
			throw new NotImplementedException();
		}
	}
}