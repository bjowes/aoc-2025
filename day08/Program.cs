using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO.Compression;
using System.Net.Sockets;

string Project() => System.Reflection.Assembly.GetCallingAssembly().GetName().Name!;
string ProjectDir() => System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetCallingAssembly().Location)!;
string FilePath(string file) => Path.Join(ProjectDir(), "../../../", file);

void SolveOne(string file, int connectionCount)
{
    Console.WriteLine($"{Project()}.1 Solve for: " + file);
    var sw = System.Diagnostics.Stopwatch.StartNew();

    var lines = File.ReadAllLines(FilePath(file))!;    

    var boxes = lines.Select(l => new Box { X = long.Parse(l.Split(",")[0]), Y = long.Parse(l.Split(",")[1]), Z = long.Parse(l.Split(",")[2])}).ToList();
    var connections = boxes.ToDictionary(b => b.Key(), b => new List<Box> { b });
    var allPossible = new List<PossibleConnection>();
    var direct = new Dictionary<long, HashSet<long>>();
    long sum = 0;

    int possibleScan = 1000;
    int madeConnections = 0;
        for (int i = 0; i < boxes.Count; i++)
        {
            var possible = new List<PossibleConnection>();
            long distance = long.MaxValue;
            for (int j = 0; j < boxes.Count; j++)
            {
                if (i == j) continue;
                var dist = boxes[i].Distance(boxes[j]);
                if (possible.Count < possibleScan || dist < distance)
                {
                    if (possible.Count >= possibleScan)
                    {
                        possible = possible.Take(possibleScan - 1).ToList();
                    }
                    possible.Add(new PossibleConnection { B1 = boxes[i], B2 = boxes[j], Distance = dist });
                    distance = possible.Max(d => d.Distance);
                }
            }
            allPossible.AddRange(possible);
        }

        
    //Console.WriteLine("all : " + allPossible.Count);
    var connectOrder = allPossible.OrderBy(b => b.Distance).ToList();

/*
    foreach (var co in connectOrder.Take(20))
    {
        Console.WriteLine($"co: {co.B1} - {co.B2} ({co.Distance})");
    }
*/
    while (madeConnections < connectionCount)
    {
        var co = connectOrder.First();        
        var b1 = co.B1;
        var b2 = co.B2;
        connectOrder.Remove(co); 

        if (direct.ContainsKey(b2.Key()))
        {
            if (direct[b2.Key()].Contains(b1.Key())) // inverse exists            
            {
                //Console.WriteLine($"dup {b1} {b2}");
                    //madeConnections++;
                continue;
            }
        }
        if (direct.ContainsKey(b1.Key()))
        {
            direct[b1.Key()].Add(b2.Key());            
        }
        else
        {
            var set = new HashSet<long>();
            set.Add(b2.Key());
            direct.Add(b1.Key(), set);            
        }
        
        madeConnections++;
        //Console.WriteLine($"connect {b1} {b2} ({b1.Distance(b2)})");

        var merged = connections[b1.Key()].Concat(connections[b2.Key()]).Distinct().ToList();
        foreach (var b in merged)
        {
            connections[b.Key()] = merged;
        }
    }

    var top3 = connections.Values.OrderByDescending(v => v.Count).Distinct().Take(3).ToList();
    Console.WriteLine(string.Join("; ", top3.Select(t => t.Count)));

    sw.Stop();
    Console.WriteLine("Sum: " + top3[0].Count * top3[1].Count * top3[2].Count);
    Console.WriteLine("Runtime: " + (sw.ElapsedTicks / (Stopwatch.Frequency / 1_000.0)).ToString("0.000") + " ms");
    Console.WriteLine("");
}

SolveOne("ex.txt", 10); // Expect 40
SolveOne("in.txt", 1000);


void SolveTwo(string file)
{
    Console.WriteLine($"{Project()}.2 Solve for: " + file);
    var sw = System.Diagnostics.Stopwatch.StartNew();

    var lines = File.ReadAllLines(FilePath(file))!;    

    var boxes = lines.Select(l => new Box { X = long.Parse(l.Split(",")[0]), Y = long.Parse(l.Split(",")[1]), Z = long.Parse(l.Split(",")[2])}).ToList();
    var connections = boxes.ToDictionary(b => b.Key(), b => new List<Box> { b });
    var allPossible = new List<PossibleConnection>();
    var direct = new Dictionary<long, HashSet<long>>();
    long sum = 0;

    int possibleScan = 1000;
    int madeConnections = 0;
        for (int i = 0; i < boxes.Count; i++)
        {
            var possible = new List<PossibleConnection>();
            long distance = long.MaxValue;
            for (int j = 0; j < boxes.Count; j++)
            {
                if (i == j) continue;
                var dist = boxes[i].Distance(boxes[j]);
                if (possible.Count < possibleScan || dist < distance)
                {
                    if (possible.Count >= possibleScan)
                    {
                        possible = possible.Take(possibleScan - 1).ToList();
                    }
                    possible.Add(new PossibleConnection { B1 = boxes[i], B2 = boxes[j], Distance = dist });
                    distance = possible.Max(d => d.Distance);
                }
            }
            allPossible.AddRange(possible);
        }

        
    //Console.WriteLine("all : " + allPossible.Count);
    var connectOrder = allPossible.OrderBy(b => b.Distance).ToList();

/*
    foreach (var co in connectOrder.Take(20))
    {
        Console.WriteLine($"co: {co.B1} - {co.B2} ({co.Distance})");
    }
*/
    PossibleConnection last = new PossibleConnection();
    while (!connections.Any() || connections.First().Value.Count < boxes.Count)
    {
        var co = connectOrder.First();    
        last = co;    
        var b1 = co.B1;
        var b2 = co.B2;
        connectOrder.Remove(co); 

        if (direct.ContainsKey(b2.Key()))
        {
            if (direct[b2.Key()].Contains(b1.Key())) // inverse exists            
            {
                //Console.WriteLine($"dup {b1} {b2}");
                    //madeConnections++;
                continue;
            }
        }
        if (direct.ContainsKey(b1.Key()))
        {
            direct[b1.Key()].Add(b2.Key());            
        }
        else
        {
            var set = new HashSet<long>();
            set.Add(b2.Key());
            direct.Add(b1.Key(), set);            
        }
        
        madeConnections++;
        //Console.WriteLine($"connect {b1} {b2} ({b1.Distance(b2)})");

        var merged = connections[b1.Key()].Concat(connections[b2.Key()]).Distinct().ToList();
        foreach (var b in merged)
        {
            connections[b.Key()] = merged;
        }
    }

    sw.Stop();
    Console.WriteLine("From wall: " + last.B1.X * last.B2.X);
    Console.WriteLine("Runtime: " + (sw.ElapsedTicks / (Stopwatch.Frequency / 1_000.0)).ToString("0.000") + " ms");
    Console.WriteLine("");
}

SolveTwo("ex.txt"); // Expect 25272
SolveTwo("in.txt");

public class PossibleConnection
{
    public Box B1 {get;set;}
    public Box B2 {get;set;}
    public long Distance {get;set;}
}

public class Box
{
    public long X {get;set;}
    public long Y {get;set;}
    public long Z {get;set;}

    public Box? Shortest {get;set;}
    public double ShortestDistance {get;set;}

    public long Key() => (X << 40) + (Y << 20) + Z;

    public override string ToString()
    {
        return $"({X}, {Y}, {Z})";
    }

    public long Distance(Box b)
    {
        return ((X - b.X) * (X - b.X)) + ((Y - b.Y) * (Y - b.Y)) + ((Z - b.Z) * (Z - b.Z));
    }
}

