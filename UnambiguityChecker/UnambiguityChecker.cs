using Categories;

namespace UnambiguityChecker
{
    public static class UnambiguityChecker
    {

        public static bool IsPullbackAmbiguous(Pullback<DLMGraph> pullback)
        {
            //condition 1
            var vertices = pullback.pullback.Vertices;
            var vCount = vertices.Count;

            for(int i = 0; i < vCount; i++)
            {
                for(int j = i + 1; j < vCount; j++)
                {
                    if (vertices[i].Left == vertices[j].Left
                        && vertices[i].Right != vertices[j].Right)
                        return true;
                }
            }

            //condition 2
            foreach(var vertex in vertices)
            {
                var localEdges = pullback.pullback.Edges.Where(edge => edge.Tail == vertex).ToList();
                var eCount = localEdges.Count;

                for (int i = 0; i < eCount; i++)
                {
                    for (int j = i + 1; j < eCount; j++)
                    {
                        //condition 2a
                        if (localEdges[i].Right == vertices[j].Right
                            && localEdges[i].Left != localEdges[j].Left)
                            return true;

                        //condition 2b
                        if (localEdges[i].Left == vertices[j].Left
                            && localEdges[i].Head.Left != localEdges[j].Head.Left)
                            return true;
                    }
                }

            }
            return false;
        }

        public static bool DoConditionsHold(DLMGHomomorphism hom1, DLMGHomomorphism hom2)
        {
            var c1 = hom1.IsLocallyInjective();
            var c2 = hom2.IsInjectiveOnVertices();
            var c3 = hom1.Target.IsDeterministic();
            var c4 = hom2.Target.IsDeterministic();
            return c1 && c2 && c3 && c4;
        }
    }
}