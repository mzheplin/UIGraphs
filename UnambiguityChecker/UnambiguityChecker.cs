using System.Linq;
using Categories;

namespace UnambiguityChecker
{
    public static class UnambiguityChecker
    {

        public static bool IsPullbackUnambiguous(Pullback<DLMGraph> pullback)
        {

            var vertices = pullback.pullback.Vertices;
            var edges = pullback.pullback.Edges;
            var c1 = true;
            var c2 = true;

            // Condition 1
            if (vertices.SelectMany((v, i) => vertices.Skip(i + 1).Select(v2 => (v, v2)))
                .Any(pair => pair.v.Left == pair.v2.Left && pair.v.Right != pair.v2.Right))
            {
                Console.WriteLine("condition 1 fails");
                c1 = false;
            }


            if (vertices.Any(vertex =>
            {
                var localEdges = edges.Where(edge => edge.Tail == vertex).ToList();
                return localEdges.SelectMany((e, i) => localEdges.Skip(i + 1).Select(e2 => (e, e2)))
                .Any(pair =>
                    pair.e.Right == pair.e2.Right && pair.e.Head != pair.e2.Head
                );
            }))
            {
                Console.WriteLine("condition 2 fails");
                c2 = false;
            }

            return c1 & c2;
        }

        public static bool DoConditionsHold(DLMGHomomorphism hom1, DLMGHomomorphism hom2)
        {
            if (hom1.Target != hom2.Target)
                throw new ArgumentException("targets of homomorphisms must be the same");


            var c1 = hom2.IsInjectiveOnVertices();
            Console.WriteLine($"is injective in vertices: {c1}");

            var actionGraph = hom1.Source;
            var affordnceGraph = hom2.Source;
            var abstractGraph = hom2.Target;

            var c2 = true;

            foreach (var actVertex in actionGraph.Vertices)
            {
                var affVertex = affordnceGraph.Vertices.FirstOrDefault(
                    v => hom2.Vertex_Map[v] == hom1.Vertex_Map[actVertex]
                    );

                if (affVertex is null)
                {
                    Console.WriteLine("pullback will not be complete");
                    continue;
                }

                var actEdges = actionGraph.Edges.Where(e => e.Tail == actVertex).ToList();
                var affEdges = affordnceGraph.Edges.Where(e => e.Tail == affVertex).ToList();


                for (int i = 0; i < actEdges.Count; i++)
                {
                    if (!c2) break;

                    var act1 = actEdges[i];
                    var aff1Edges = affEdges.Where(e => hom2.Edge_Map[e] == hom1.Edge_Map[act1]);

                    if (aff1Edges.Count() == 0) continue;

                    for (int j = i + 1; j < actEdges.Count; j++)
                    {
                        if (!c2) break;
                         
                        var act2 = actEdges[j];

                        if (act1.Head == act2.Head) continue;

                        if (hom1.Edge_Map[act1] == hom1.Edge_Map[act2])
                        {
                            Console.WriteLine("condition 2 fails");
                            c2 = false;
                            break;
                        }

                        var aff2Edges = affEdges.Where(e => hom2.Edge_Map[e] == hom1.Edge_Map[act2]);

                        if (aff2Edges.Count() == 0) continue;

                        foreach (var aff1 in aff1Edges)
                        {
                            foreach (var aff2 in aff2Edges)
                            {
                                if (aff1.Label == aff2.Label)
                                {
                                    Console.WriteLine("condition 2 fails");
                                    c2 = false;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            return c1 && c2;
        }
    }
}