using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net.Sockets;

string Project() => System.Reflection.Assembly.GetCallingAssembly().GetName().Name!;
string ProjectDir() => System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetCallingAssembly().Location)!;
string FilePath(string file) => Path.Join(ProjectDir(), "../../../", file);


void SolveOne(string file)
{
    Console.WriteLine($"{Project()}.1 Solve for: " + file);
    var sw = System.Diagnostics.Stopwatch.StartNew();

    var lines = File.ReadAllLines(FilePath(file))!;    
    var beams = new List<int>();
    beams.Add(lines[0].IndexOf('S'));
    long sum = 0;

    for (int row = 1; row < lines.Length; row++)
    {
        var newBeams = new List<int>();
        foreach (var beam in beams)
        {
            if (lines[row][beam] == '.') newBeams.Add(beam);
            else // splitter
            {
                if (beam > 0)
                {
                    newBeams.Add(beam - 1);
                }
                if (beam < lines[0].Length - 1)
                {
                    newBeams.Add(beam + 1);
                }
                sum++;
                //Console.WriteLine("splitter " + row + " " + beam);
            }
        }
        beams = newBeams.Distinct().ToList();
    }

    sw.Stop();
    Console.WriteLine("Sum: " + sum);
    Console.WriteLine("Runtime: " + (sw.ElapsedTicks / (Stopwatch.Frequency / 1_000.0)).ToString("0.000") + " ms");
    Console.WriteLine("");
}

SolveOne("ex.txt"); // Expect 21
SolveOne("in.txt");


void SolveTwo(string file)
{
    Console.WriteLine($"{Project()}.2 Solve for: " + file);
    var sw = System.Diagnostics.Stopwatch.StartNew();

    var lines = File.ReadAllLines(FilePath(file))!;    
    var beams = new Dictionary<int, long>();
    beams.Add(lines[0].IndexOf('S'), 1);

    for (int row = 1; row < lines.Length; row++)
    {
        var newBeams = new Dictionary<int, long>();
        foreach (var beam in beams)
        {
            if (lines[row][beam.Key] == '.') {
                if (newBeams.ContainsKey(beam.Key))
                {
                    newBeams[beam.Key] = newBeams[beam.Key] + beam.Value;
                } else
                {
                    newBeams[beam.Key] = beam.Value;
                }
            }
            else // splitter
            {
                if (beam.Key > 0)
                {
                    if (newBeams.ContainsKey(beam.Key - 1))
                    {
                        newBeams[beam.Key - 1] = newBeams[beam.Key - 1] + beam.Value;
                    } else
                    {
                        newBeams[beam.Key - 1] = beam.Value;
                    }
                }
                if (beam.Key < lines[0].Length - 1)
                {
                    if (newBeams.ContainsKey(beam.Key + 1))
                    {
                        newBeams[beam.Key + 1] = newBeams[beam.Key + 1] + beam.Value;
                    } else
                    {
                        newBeams[beam.Key + 1] = beam.Value;
                    }
                }
                //Console.WriteLine("splitter " + row + " " + beam);
            }
        }
        beams = newBeams;
    }

    sw.Stop();
    Console.WriteLine("Timelines: " + beams.Values.Sum());
    Console.WriteLine("Runtime: " + (sw.ElapsedTicks / (Stopwatch.Frequency / 1_000.0)).ToString("0.000") + " ms");
    Console.WriteLine("");
}

SolveTwo("ex.txt"); // Expect 40
SolveTwo("in.txt");
