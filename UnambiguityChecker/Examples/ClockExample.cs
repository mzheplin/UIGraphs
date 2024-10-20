namespace UnambiguityChecker.Examples;
using Categories;

public static class ClockExample
{
    public static void ShowExample()
    {
        var time_no_alarm = new Vertex("t - []");
        var time_with_alarm = new Vertex("t - [a]");
        var time_active_alarm = new Vertex("t - [a!]");


        var actionEdges = new List<DEdge>
        {
            new DEdge(time_no_alarm, time_with_alarm, "add alarm a+"),
            new DEdge(time_with_alarm, time_active_alarm, "activate alarm t = a, a -> a!"),
            new DEdge(time_active_alarm, time_no_alarm, "stop alarm a!-"),
            new DEdge(time_active_alarm, time_with_alarm, "postpone alarm a! -> a"),
            new DEdge(time_no_alarm, time_no_alarm, "time passes t+"),
            new DEdge(time_with_alarm, time_with_alarm, "time passes t+"),
            new DEdge(time_active_alarm, time_active_alarm, "time passes t+"),
        };

        DLMGraph actionGraph = new DLMGraph(actionEdges);
        Console.WriteLine(actionGraph);

        var quite_context = new Vertex("quiet context - {add alarm menu}");
        var ringing_context = new Vertex("ringing context = {stop alarm button}");


        var affordanceEdges = new List<DEdge>
        {
            new DEdge(quite_context, quite_context, "add alarm in a menu"),
            new DEdge(quite_context, ringing_context, "start ringing"),
            new DEdge(ringing_context, quite_context, "click stop button"),
            new DEdge(ringing_context, quite_context, "auto-stop ringing"),
            new DEdge(quite_context, quite_context, "time ticks"),
            new DEdge(ringing_context, ringing_context, "time ticks"),
        };

        DLMGraph affordanceGraph = new DLMGraph(affordanceEdges);
        Console.WriteLine(affordanceGraph);


        var c1 = new Vertex("quiet");
        var c2 = new Vertex("ringing");

        var abstractEdges = new List<DEdge>
        {
            new DEdge(c1, c1, "time +"),
            new DEdge(c1, c1, "alarm +"),
            new DEdge(c2, c2, "time +"),
            new DEdge(c1, c2, "ringing +"),
            new DEdge(c2, c1, "ringing -"),
            new DEdge(c2, c1, "ringing on hold"),
        };

        DLMGraph abstractGraph = new DLMGraph(abstractEdges);


        var ac_ab_v = new Dictionary<Vertex, Vertex>()
        {
            {time_no_alarm,c1}, {time_with_alarm, c1 }, { time_active_alarm, c2}
        };

        var ac_ab_e = new Dictionary<string, string>()
        {
            {"add alarm a+", "alarm +" },
            {"activate alarm t = a, a -> a!",  "ringing +"},
            {"stop alarm a!-",  "ringing -"},
            {"postpone alarm a! -> a",  "ringing on hold"},
            {"time passes t+",  "time +"}
        };
        Console.WriteLine(abstractGraph);

        DLMGHomomorphism ac_ab = new DLMGHomomorphism(actionGraph, abstractGraph, ac_ab_v, ac_ab_e);
        Console.WriteLine(ac_ab);

        var af_ab_v = new Dictionary<Vertex, Vertex>()
        {
            {quite_context,c1}, {ringing_context, c2 }
        };

        var af_ab_e = new Dictionary<string, string>()
        {
            {"add alarm in a menu", "alarm +" },
            {"start ringing",  "ringing +"},
            {"click stop button",  "ringing -"},
            {"auto-stop ringing",  "ringing on hold"},
            {"time ticks",  "time +"}
        };

        DLMGHomomorphism af_ab = new DLMGHomomorphism(affordanceGraph, abstractGraph, af_ab_v, af_ab_e);
        Console.WriteLine(af_ab);
        DLMGCategory category = new DLMGCategory();

        var pullback = category.GetPullback(ac_ab, af_ab);
        Console.WriteLine(pullback);

        Console.Read();
    }
}