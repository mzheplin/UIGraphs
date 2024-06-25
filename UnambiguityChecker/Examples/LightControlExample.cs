using System;
using Categories;

namespace UnambiguityChecker.Examples
{
	public static class LightControlExample
	{
		public static void ShowExample()
		{
            var dark_room = new Vertex("00");
            var purple = new Vertex("10");
            var purple_orange = new Vertex("11");
            var orange = new Vertex("01");

            var actionEdges = new List<DEdge>
            {
                new DEdge(dark_room, purple, "3 on"),
                new DEdge(purple, purple, "3 on"),
                new DEdge(purple, purple_orange, "4 on"),
                new DEdge(orange, purple_orange, "3 on"),
                new DEdge(dark_room, orange, "4 on"),
                new DEdge(orange, orange, "4 on"),
                new DEdge(purple_orange, purple_orange, "4 on"),
                new DEdge(purple_orange, purple_orange, "3 on"),

                new DEdge(purple, dark_room, "3 off"),
                new DEdge(orange, dark_room, "4 off"),
                new DEdge(purple_orange, purple, "4 off"),
                new DEdge(purple_orange, orange, "3 off"),
                new DEdge(dark_room, dark_room, "3 off"),
                new DEdge(dark_room, dark_room, "4 off"),
                new DEdge(orange, orange, "3 off"),
                new DEdge(purple, purple, "4 off"),

                new DEdge(purple, purple, ""),
                new DEdge(dark_room, dark_room, ""),
                new DEdge(purple_orange, purple_orange, ""),
                new DEdge(orange, orange, ""),
            };

            DLMGraph actionGraph = new DLMGraph(actionEdges);
            Console.WriteLine(actionGraph);

            Console.WriteLine($"Number of vertices in action graph: {actionGraph.Vertices.Count}");
            Console.WriteLine($"Number of edges in action graph: {actionGraph.Edges.Count}");

            var c1 = new Vertex("00");
            var c2 = new Vertex("10");
            var c3 = new Vertex("01");
            var c4 = new Vertex("11");

            var affordanceEdges = new List<DEdge>
            {
                new DEdge(c1, c2, "1t"),
                new DEdge(c1, c3, "2t"),
                new DEdge(c1, c1, "3t"),
                new DEdge(c1, c1, "4t"),
                new DEdge(c1, c1, "1b"),
                new DEdge(c1, c1, "2b"),
                new DEdge(c1, c1, "3b"),
                new DEdge(c1, c1, "4b"),

                new DEdge(c2, c2, "1t"),
                new DEdge(c2, c4, "2t"),
                new DEdge(c2, c2, "3t"),
                new DEdge(c2, c2, "4t"),
                new DEdge(c2, c1, "1b"),
                new DEdge(c2, c2, "2b"),
                new DEdge(c2, c2, "3b"),
                new DEdge(c2, c2, "4b"),

                new DEdge(c3, c4, "1t"),
                new DEdge(c3, c3, "2t"),
                new DEdge(c3, c3, "3t"),
                new DEdge(c3, c3, "4t"),
                new DEdge(c3, c3, "1b"),
                new DEdge(c3, c1, "2b"),
                new DEdge(c3, c3, "3b"),
                new DEdge(c3, c3, "4b"),

                new DEdge(c4, c4, "1t"),
                new DEdge(c4, c4, "2t"),
                new DEdge(c4, c4, "3t"),
                new DEdge(c4, c4, "4t"),
                new DEdge(c4, c3, "1b"),
                new DEdge(c4, c2, "2b"),
                new DEdge(c4, c4, "3b"),
                new DEdge(c4, c4, "4b"),
            };
            DLMGraph affordanceGraph = new DLMGraph(affordanceEdges);
            Console.WriteLine(affordanceGraph);

            Console.WriteLine($"Number of vertices in affordance graph: {affordanceGraph.Vertices.Count}");
            Console.WriteLine($"Number of edges in affordance graph: {affordanceGraph.Edges.Count}");


            var m = new Vertex(".");

            var abstractEdges = new List<DEdge>
            {
                new DEdge(m, m, "3t"),
                new DEdge(m, m, "4t"),
                new DEdge(m, m, ""),
                new DEdge(m, m, "3b"),
                new DEdge(m, m, "4b"),

            };
            DLMGraph abstractGraph = new DLMGraph(abstractEdges);
            Console.WriteLine(abstractGraph);

            Console.WriteLine($"Number of vertices in abstract graph: {abstractGraph.Vertices.Count}");
            Console.WriteLine($"Number of edges in abstract graph: {abstractGraph.Edges.Count}");

            Dictionary<Vertex, Vertex> acab_v = new Dictionary<Vertex, Vertex>()
            {   {dark_room,m },
                {purple,m},
                {orange,m},
                {purple_orange,m },
            };

            Dictionary<string, string> acab_e = new Dictionary<string, string>()
            {
                {"3 on","3t"},
                {"4 on","4t"},
                {"3 off","3b"},
                {"4 off", "4b"},
                {"", "" }
            };
            DLMGHomomorphism ac_ab = new DLMGHomomorphism(actionGraph, abstractGraph, acab_v, acab_e);

            //affordance to abstract
            Dictionary<Vertex, Vertex> afab_v = new Dictionary<Vertex, Vertex>()
                { {c1,m },{c2,m},{c3,m}, {c4, m} };

            Dictionary<string, string> afab_e = new Dictionary<string, string>()
             {
                {"1t",""},
                {"2t",""},
                {"3t","3t"},
                {"4t","4t"},
                {"1b",""},
                {"2b",""},
                {"3b","3b"},
                {"4b","4b"},
            };

            DLMGHomomorphism af_ab = new DLMGHomomorphism(affordanceGraph, abstractGraph, afab_v, afab_e);

            DLMGCategory category = new DLMGCategory();

            var pullback = category.GetPullback(ac_ab, af_ab);
            Console.WriteLine(pullback.pullback);
            Console.WriteLine(UnambiguityChecker.DoConditionsHold(ac_ab, af_ab));
            Console.WriteLine(UnambiguityChecker.IsPullbackUnambiguous(pullback));

        }


        public static void ShowExampleV2()
        {
            var dark_room = new Vertex("0000");
            var orange = new Vertex("0001");
            var purple = new Vertex("0010");
            var purple_orange = new Vertex("0011");
            var purple_blue = new Vertex("0110");
            var purple_blue_orange = new Vertex("0111");
            var purple_red = new Vertex("1010");
            var purple_red_orange = new Vertex("1011");
            var purple_red_blue = new Vertex("1110");
            var purple_red_blue_orange = new Vertex("1111");

            var actionEdges = new List<DEdge>
            {
                new DEdge(dark_room, orange, "4+"),
                new DEdge(orange, dark_room, "4-"),
                new DEdge(orange, orange, "4+"),
                new DEdge(dark_room, dark_room, "4-"),

                new DEdge(purple, purple_orange, "4+"),
                new DEdge(purple_orange, purple, "4-"),
                new DEdge(purple_orange, purple_orange, "4+"),
                new DEdge(purple, purple, "4-"),

                new DEdge(purple_blue, purple_blue_orange, "4+"),
                new DEdge(purple_blue_orange, purple_blue, "4-"),
                new DEdge(purple_blue_orange, purple_blue_orange, "4+"),
                new DEdge(purple_blue, purple_blue, "4-"),

                new DEdge(purple_red, purple_red_orange, "4+"),
                new DEdge(purple_red_orange, purple_red, "4-"),
                new DEdge(purple_red_orange, purple_red_orange, "4+"),
                new DEdge(purple_red, purple_red, "4-"),

                new DEdge(purple_red_blue, purple_red_blue_orange, "4+"),
                new DEdge(purple_red_blue_orange, purple_red_blue, "4-"),
                new DEdge(purple_red_blue_orange, purple_red_blue_orange, "4+"),
                new DEdge(purple_red_blue, purple_red_blue, "4-"),

                new DEdge(dark_room, dark_room, "1+"),
                new DEdge(dark_room, dark_room, "1-"),
                new DEdge(dark_room, dark_room, "2+"),
                new DEdge(dark_room, dark_room, "2-"),

                new DEdge(orange, orange, "1+"),
                new DEdge(orange, orange, "1-"),
                new DEdge(orange, orange, "2+"),
                new DEdge(orange, orange, "2-"),

                new DEdge(dark_room, purple, "3+"),
                new DEdge(purple, dark_room, "3-"),
                new DEdge(dark_room, purple_blue, "3+"),
                new DEdge(purple_blue, dark_room, "3-"),
                new DEdge(dark_room, purple_red, "3+"),
                new DEdge(purple_red, dark_room, "3-"),
                new DEdge(dark_room, purple_red_blue, "3+"),
                new DEdge(purple_red_blue, dark_room, "3-"),

                new DEdge(purple, purple, "3+"),
                new DEdge(dark_room, dark_room, "3-"),
                new DEdge(purple_blue, purple_blue, "3+"),
                new DEdge(purple_red, purple_red, "3+"),
                new DEdge(purple_red_blue, purple_red_blue, "3+"),

                new DEdge(orange, purple_orange, "3+"),
                new DEdge(purple_orange, orange, "3-"),
                new DEdge(orange, purple_blue_orange, "3+"),
                new DEdge(purple_blue_orange, orange, "3-"),
                new DEdge(orange, purple_red_orange, "3+"),
                new DEdge(purple_red_orange, orange, "3-"),
                new DEdge(orange, purple_red_blue_orange, "3+"),
                new DEdge(purple_red_blue_orange, orange, "3-"),

                new DEdge(purple_orange, purple_orange, "3+"),
                new DEdge(orange, orange, "3-"),
                new DEdge(purple_blue_orange, purple_blue_orange, "3+"),
                new DEdge(purple_red_orange, purple_red_orange, "3+"),
                new DEdge(purple_red_blue_orange, purple_red_blue_orange, "3+"),

                new DEdge(purple, purple_blue, "2+"),
                new DEdge(purple_blue, purple, "2-"),

                new DEdge(purple_orange, purple_blue_orange, "2+"),
                new DEdge(purple_blue_orange, purple_orange, "2-"),

                new DEdge(purple_red, purple_red_blue, "2+"),
                new DEdge(purple_red_blue, purple_red, "2-"),

                new DEdge(purple_red_orange, purple_red_blue_orange, "2+"),
                new DEdge(purple_red_blue_orange, purple_red_orange, "2-"),

                new DEdge(purple_blue, purple_blue, "2+"),
                new DEdge(purple, purple, "2-"),

                new DEdge(purple_blue_orange, purple_blue_orange, "2+"),
                new DEdge(purple_orange, purple_orange, "2-"),

                new DEdge(purple_red_blue, purple_red_blue, "2+"),
                new DEdge(purple_red, purple_red, "2-"),

                new DEdge(purple_red_blue_orange, purple_red_blue_orange, "2+"),
                new DEdge(purple_red_orange, purple_red_orange, "2-"),

                new DEdge(purple, purple_red, "1+"),
                new DEdge(purple_red, purple, "1-"),

                new DEdge(purple_orange, purple_red_orange, "1+"),
                new DEdge(purple_red_orange, purple_orange, "1-"),

                new DEdge(purple_blue, purple_red_blue, "1+"),
                new DEdge(purple_red_blue, purple_blue, "1-"),

                new DEdge(purple_blue_orange, purple_red_blue_orange, "1+"),
                new DEdge(purple_red_blue_orange, purple_blue_orange, "1-"),

                new DEdge(purple_red, purple_red, "1+"),
                new DEdge(purple, purple, "1-"),

                new DEdge(purple_red_orange, purple_red_orange, "1+"),
                new DEdge(purple_orange, purple_orange, "1-"),

                new DEdge(purple_red_blue, purple_red_blue, "1+"),
                new DEdge(purple_blue, purple_blue, "1-"),

                new DEdge(purple_red_blue_orange, purple_red_blue_orange, "1+"),
                new DEdge(purple_blue_orange, purple_blue_orange, "1-"),
            };

            DLMGraph actionGraph = new DLMGraph(actionEdges);
            Console.WriteLine(actionGraph);

            Console.WriteLine($"Number of vertices in action graph: {actionGraph.Vertices.Count}");
            Console.WriteLine($"Number of edges in action graph: {actionGraph.Edges.Count}");

            var c1 = new Vertex("0_00");
            var c2 = new Vertex("0_10");
            var c3 = new Vertex("0_01");
            var c4 = new Vertex("0_11");
            var c5 = new Vertex("1_00");
            var c6 = new Vertex("1_10");
            var c7 = new Vertex("1_01");
            var c8 = new Vertex("1_11");

            var affordanceEdges = new List<DEdge>
            {
                new DEdge(c1, c2, "1t"),
                new DEdge(c2, c2, "1t"),

                new DEdge(c1, c3, "2t"),
                new DEdge(c3, c3, "2t"),

                new DEdge(c5, c6, "1t"),
                new DEdge(c6, c6, "1t"),

                new DEdge(c5, c7, "2t"),
                new DEdge(c7, c7, "2t"),

                new DEdge(c1, c5, "3t"),
                new DEdge(c5, c1, "3b"),
                new DEdge(c5, c5, "3t"),
                new DEdge(c1, c1, "3b"),

                new DEdge(c1, c1, "4t"),
                new DEdge(c1, c1, "4b"),

                new DEdge(c5, c5, "4t"),
                new DEdge(c5, c5, "4b"),

                new DEdge(c2, c1, "1b"),
                new DEdge(c2, c4, "2t"),
                new DEdge(c1, c1, "1b"),
                new DEdge(c4, c4, "2t"),

                new DEdge(c6, c5, "1b"),
                new DEdge(c6, c8, "2t"),
                new DEdge(c5, c5, "1b"),
                new DEdge(c8, c8, "2t"),

                new DEdge(c2, c6, "3t"),
                new DEdge(c6, c2, "3b"),
                new DEdge(c6, c6, "3t"),
                new DEdge(c2, c2, "3b"),

                new DEdge(c2, c2, "4t"),
                new DEdge(c2, c2, "4b"),

                new DEdge(c6, c6, "4t"),
                new DEdge(c6, c6, "4b"),

                new DEdge(c3, c4, "1t"),
                new DEdge(c3, c1, "2b"),

                new DEdge(c7, c8, "1t"),
                new DEdge(c7, c5, "2b"),

                new DEdge(c3, c7, "3t"),
                new DEdge(c7, c3, "3b"),

                new DEdge(c4, c4, "1t"),
                new DEdge(c1, c1, "2b"),

                new DEdge(c8, c8, "1t"),
                new DEdge(c5, c5, "2b"),

                new DEdge(c7, c7, "3t"),
                new DEdge(c3, c3, "3b"),

                new DEdge(c3, c3, "4t"),
                new DEdge(c3, c3, "4b"),

                new DEdge(c7, c7, "4t"),
                new DEdge(c7, c7, "4b"),

                new DEdge(c4, c3, "1b"),
                new DEdge(c4, c2, "2b"),

                new DEdge(c8, c7, "1b"),
                new DEdge(c8, c6, "2b"),

                new DEdge(c4, c8, "3t"),
                new DEdge(c8, c4, "3b"),

                new DEdge(c3, c3, "1b"),
                new DEdge(c2, c2, "2b"),

                new DEdge(c7, c7, "1b"),
                new DEdge(c6, c6, "2b"),

                new DEdge(c8, c8, "3t"),
                new DEdge(c4, c4, "3b"),

                new DEdge(c4, c4, "4t"),
                new DEdge(c4, c4, "4b"),

                new DEdge(c8, c8, "4t"),
                new DEdge(c8, c8, "4b"),
            };
            DLMGraph affordanceGraph = new DLMGraph(affordanceEdges);
            Console.WriteLine(affordanceGraph);

            Console.WriteLine($"Number of vertices in affordance graph: {affordanceGraph.Vertices.Count}");
            Console.WriteLine($"Number of edges in affordance graph: {affordanceGraph.Edges.Count}");


            var m0 = new Vertex("0");
            var m1 = new Vertex("1");
            var m2 = new Vertex("2");
            var m3 = new Vertex("3");
            var m4 = new Vertex("4");

            var abstractEdges = new List<DEdge>
            {
                new DEdge(m0, m0, "1+"),
                new DEdge(m0, m0, "2+"),
                new DEdge(m0, m0, "1-"),
                new DEdge(m0, m0, "2-"),

                new DEdge(m1, m2, "1+"),
                new DEdge(m3, m4, "1+"),
                new DEdge(m2, m1, "1-"),
                new DEdge(m4, m3, "1-"),

                new DEdge(m1, m3, "2+"),
                new DEdge(m2, m4, "2+"),
                new DEdge(m3, m1, "2-"),
                new DEdge(m4, m2, "2-"),


                new DEdge(m2, m2, "1+"),
                new DEdge(m4, m4, "1+"),
                new DEdge(m1, m1, "1-"),
                new DEdge(m3, m3, "1-"),

                new DEdge(m3, m3, "2+"),
                new DEdge(m4, m4, "2+"),
                new DEdge(m1, m1, "2-"),
                new DEdge(m2, m2, "2-"),


                new DEdge(m0, m1, "3+"),
                new DEdge(m0, m2, "3+"),
                new DEdge(m0, m3, "3+"),
                new DEdge(m0, m4, "3+"),

                new DEdge(m1, m0, "3-"),
                new DEdge(m2, m0, "3-"),
                new DEdge(m3, m0, "3-"),
                new DEdge(m4, m0, "3-"),


                new DEdge(m1, m1, "3+"),
                new DEdge(m2, m2, "3+"),
                new DEdge(m3, m3, "3+"),
                new DEdge(m4, m4, "3+"),

                new DEdge(m0, m0, "3-"),

                new DEdge(m0, m0, "4+"),
                new DEdge(m0, m0, "4-"),
                new DEdge(m1, m1, "4+"),
                new DEdge(m1, m1, "4-"),
                new DEdge(m2, m2, "4+"),
                new DEdge(m2, m2, "4-"),
                new DEdge(m3, m3, "4+"),
                new DEdge(m3, m3, "4-"),
                new DEdge(m4, m4, "4+"),
                new DEdge(m4, m4, "4-"),
            };
            DLMGraph abstractGraph = new DLMGraph(abstractEdges);
            Console.WriteLine(abstractGraph);

            Console.WriteLine($"Number of vertices in abstract graph: {abstractGraph.Vertices.Count}");
            Console.WriteLine($"Number of edges in abstract graph: {abstractGraph.Edges.Count}");

            Dictionary<Vertex, Vertex> acab_v = new Dictionary<Vertex, Vertex>()
            {
                {dark_room, m0},
                {orange, m0},
                {purple,m1},
                {purple_red, m2},
                {purple_blue,m3},
                {purple_red_blue,m4},
                {purple_orange,m1},
                {purple_red_orange, m2},
                {purple_blue_orange,m3},
                {purple_red_blue_orange,m4},
            };

            Dictionary<string, string> acab_e = new Dictionary<string, string>()
            {
                {"1+","1+"},
                {"1-","1-"},
                {"2+","2+"},
                {"2-","2-"},
                {"3+","3+"},
                {"3-","3-"},
                {"4+","4+"},
                {"4-","4-"},
            };
            DLMGHomomorphism ac_ab = new DLMGHomomorphism(actionGraph, abstractGraph, acab_v, acab_e);

            //affordance to abstract
            Dictionary<Vertex, Vertex> afab_v = new Dictionary<Vertex, Vertex>()
            {
                {c1,m0},
                {c2,m0},
                {c3,m0},
                {c4,m0},
                {c5,m1},
                {c6,m2},
                {c7,m3},
                {c8,m4}
            };

            Dictionary<string, string> afab_e = new Dictionary<string, string>()
            {
                {"1t","1+"},
                {"1b","1-"},
                {"2t","2+"},
                {"2b","2-"},
                {"3t","3+"},
                {"3b","3-"},
                {"4t","4+"},
                {"4b","4-"},
            };


            DLMGHomomorphism af_ab = new DLMGHomomorphism(affordanceGraph, abstractGraph, afab_v, afab_e);

            DLMGCategory category = new DLMGCategory();

            var pullback = category.GetPullback(ac_ab, af_ab);

            Console.WriteLine(pullback.pullback);
            Console.WriteLine(UnambiguityChecker.DoConditionsHold(ac_ab, af_ab));
            Console.WriteLine(UnambiguityChecker.IsPullbackUnambiguous(pullback));
        }



        public static void ShowExampleV3()
        {
            var dark_room = new Vertex("0000");
            var orange = new Vertex("0001");
            var purple = new Vertex("0010");
            var purple_orange = new Vertex("0011");
            var purple_blue = new Vertex("0110");
            var purple_blue_orange = new Vertex("0111");
            var purple_red = new Vertex("1010");
            var purple_red_orange = new Vertex("1011");
            var purple_red_blue = new Vertex("1110");
            var purple_red_blue_orange = new Vertex("1111");

            var actionEdges = new List<DEdge>
            {
                new DEdge(dark_room, orange, "4+"),
                new DEdge(orange, dark_room, "4-"),
                new DEdge(orange, orange, "4+"),
                new DEdge(dark_room, dark_room, "4-"),

                new DEdge(purple, purple_orange, "4+"),
                new DEdge(purple_orange, purple, "4-"),
                new DEdge(purple_orange, purple_orange, "4+"),
                new DEdge(purple, purple, "4-"),

                new DEdge(purple_blue, purple_blue_orange, "4+"),
                new DEdge(purple_blue_orange, purple_blue, "4-"),
                new DEdge(purple_blue_orange, purple_blue_orange, "4+"),
                new DEdge(purple_blue, purple_blue, "4-"),

                new DEdge(purple_red, purple_red_orange, "4+"),
                new DEdge(purple_red_orange, purple_red, "4-"),
                new DEdge(purple_red_orange, purple_red_orange, "4+"),
                new DEdge(purple_red, purple_red, "4-"),

                new DEdge(purple_red_blue, purple_red_blue_orange, "4+"),
                new DEdge(purple_red_blue_orange, purple_red_blue, "4-"),
                new DEdge(purple_red_blue_orange, purple_red_blue_orange, "4+"),
                new DEdge(purple_red_blue, purple_red_blue, "4-"),

                new DEdge(dark_room, dark_room, "1+"),
                new DEdge(dark_room, dark_room, "1-"),
                new DEdge(dark_room, dark_room, "2+"),
                new DEdge(dark_room, dark_room, "2-"),

                new DEdge(orange, orange, "1+"),
                new DEdge(orange, orange, "1-"),
                new DEdge(orange, orange, "2+"),
                new DEdge(orange, orange, "2-"),

                new DEdge(dark_room, purple, "3+"),
                new DEdge(purple, dark_room, "3-"),
                new DEdge(dark_room, purple_blue, "3+"),
                new DEdge(purple_blue, dark_room, "3-"),
                new DEdge(dark_room, purple_red, "3+"),
                new DEdge(purple_red, dark_room, "3-"),
                new DEdge(dark_room, purple_red_blue, "3+"),
                new DEdge(purple_red_blue, dark_room, "3-"),

                new DEdge(purple, purple, "3+"),
                new DEdge(dark_room, dark_room, "3-"),
                new DEdge(purple_blue, purple_blue, "3+"),
                new DEdge(purple_red, purple_red, "3+"),
                new DEdge(purple_red_blue, purple_red_blue, "3+"),

                new DEdge(orange, purple_orange, "3+"),
                new DEdge(purple_orange, orange, "3-"),
                new DEdge(orange, purple_blue_orange, "3+"),
                new DEdge(purple_blue_orange, orange, "3-"),
                new DEdge(orange, purple_red_orange, "3+"),
                new DEdge(purple_red_orange, orange, "3-"),
                new DEdge(orange, purple_red_blue_orange, "3+"),
                new DEdge(purple_red_blue_orange, orange, "3-"),

                new DEdge(purple_orange, purple_orange, "3+"),
                new DEdge(orange, orange, "3-"),
                new DEdge(purple_blue_orange, purple_blue_orange, "3+"),
                new DEdge(purple_red_orange, purple_red_orange, "3+"),
                new DEdge(purple_red_blue_orange, purple_red_blue_orange, "3+"),

                new DEdge(purple, purple_blue, "2+"),
                new DEdge(purple_blue, purple, "2-"),

                new DEdge(purple_orange, purple_blue_orange, "2+"),
                new DEdge(purple_blue_orange, purple_orange, "2-"),

                new DEdge(purple_red, purple_red_blue, "2+"),
                new DEdge(purple_red_blue, purple_red, "2-"),

                new DEdge(purple_red_orange, purple_red_blue_orange, "2+"),
                new DEdge(purple_red_blue_orange, purple_red_orange, "2-"),

                new DEdge(purple_blue, purple_blue, "2+"),
                new DEdge(purple, purple, "2-"),

                new DEdge(purple_blue_orange, purple_blue_orange, "2+"),
                new DEdge(purple_orange, purple_orange, "2-"),

                new DEdge(purple_red_blue, purple_red_blue, "2+"),
                new DEdge(purple_red, purple_red, "2-"),

                new DEdge(purple_red_blue_orange, purple_red_blue_orange, "2+"),
                new DEdge(purple_red_orange, purple_red_orange, "2-"),

                new DEdge(purple, purple_red, "1+"),
                new DEdge(purple_red, purple, "1-"),

                new DEdge(purple_orange, purple_red_orange, "1+"),
                new DEdge(purple_red_orange, purple_orange, "1-"),

                new DEdge(purple_blue, purple_red_blue, "1+"),
                new DEdge(purple_red_blue, purple_blue, "1-"),

                new DEdge(purple_blue_orange, purple_red_blue_orange, "1+"),
                new DEdge(purple_red_blue_orange, purple_blue_orange, "1-"),

                new DEdge(purple_red, purple_red, "1+"),
                new DEdge(purple, purple, "1-"),

                new DEdge(purple_red_orange, purple_red_orange, "1+"),
                new DEdge(purple_orange, purple_orange, "1-"),

                new DEdge(purple_red_blue, purple_red_blue, "1+"),
                new DEdge(purple_blue, purple_blue, "1-"),

                new DEdge(purple_red_blue_orange, purple_red_blue_orange, "1+"),
                new DEdge(purple_blue_orange, purple_blue_orange, "1-"),
            };

            DLMGraph actionGraph = new DLMGraph(actionEdges);
            Console.WriteLine(actionGraph);

            Console.WriteLine($"Number of vertices in action graph: {actionGraph.Vertices.Count}");
            Console.WriteLine($"Number of edges in action graph: {actionGraph.Edges.Count}");

            var c1 = new Vertex("0");

            var affordanceEdges = new List<DEdge>
            {
                new DEdge(c1, c1, "1t"),
                new DEdge(c1, c1, "2t"),
                new DEdge(c1, c1, "3t"),
                new DEdge(c1, c1, "3b"),
                new DEdge(c1, c1, "4t"),
                new DEdge(c1, c1, "4b"),
                new DEdge(c1, c1, "1b"),
                new DEdge(c1, c1, "2b"),
            };
            DLMGraph affordanceGraph = new DLMGraph(affordanceEdges);
            Console.WriteLine(affordanceGraph);

            Console.WriteLine($"Number of vertices in affordance graph: {affordanceGraph.Vertices.Count}");
            Console.WriteLine($"Number of edges in affordance graph: {affordanceGraph.Edges.Count}");


            var m0 = new Vertex("0");

            var abstractEdges = new List<DEdge>
            {
                new DEdge(m0, m0, "1+"),
                new DEdge(m0, m0, "2+"),
                new DEdge(m0, m0, "1-"),
                new DEdge(m0, m0, "2-"),
                new DEdge(m0, m0, "3+"),
                new DEdge(m0, m0, "3-"),
                new DEdge(m0, m0, "4+"),
                new DEdge(m0, m0, "4-"),
            };
            DLMGraph abstractGraph = new DLMGraph(abstractEdges);
            Console.WriteLine(abstractGraph);

            Console.WriteLine($"Number of vertices in abstract graph: {abstractGraph.Vertices.Count}");
            Console.WriteLine($"Number of edges in abstract graph: {abstractGraph.Edges.Count}");

            Dictionary<Vertex, Vertex> acab_v = new Dictionary<Vertex, Vertex>()
            {
                {dark_room, m0},
                {orange, m0},
                {purple,m0},
                {purple_red, m0},
                {purple_blue,m0},
                {purple_red_blue,m0},
                {purple_orange,m0},
                {purple_red_orange, m0},
                {purple_blue_orange,m0},
                {purple_red_blue_orange,m0},
            };

            Dictionary<string, string> acab_e = new Dictionary<string, string>()
            {
                {"1+","1+"},
                {"1-","1-"},
                {"2+","2+"},
                {"2-","2-"},
                {"3+","3+"},
                {"3-","3-"},
                {"4+","4+"},
                {"4-","4-"},
            };
            DLMGHomomorphism ac_ab = new DLMGHomomorphism(actionGraph, abstractGraph, acab_v, acab_e);

            //affordance to abstract
            Dictionary<Vertex, Vertex> afab_v = new Dictionary<Vertex, Vertex>()
            {
                {c1,m0},
            };

            Dictionary<string, string> afab_e = new Dictionary<string, string>()
            {
                {"1t","1+"},
                {"1b","1-"},
                {"2t","2+"},
                {"2b","2-"},
                {"3t","3+"},
                {"3b","3-"},
                {"4t","4+"},
                {"4b","4-"},
            };


            DLMGHomomorphism af_ab = new DLMGHomomorphism(affordanceGraph, abstractGraph, afab_v, afab_e);

            DLMGCategory category = new DLMGCategory();

            var pullback = category.GetPullback(ac_ab, af_ab);

            Console.WriteLine(pullback.pullback);
            Console.WriteLine(UnambiguityChecker.DoConditionsHold(ac_ab, af_ab));
            Console.WriteLine(UnambiguityChecker.IsPullbackUnambiguous(pullback));
        }
    }
}



