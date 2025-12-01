string Project() => System.Reflection.Assembly.GetCallingAssembly().GetName().Name!;
string ProjectDir() => System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetCallingAssembly().Location)!;
string FilePath(string file) => Path.Join(ProjectDir(), "../../../", file);

void SolveOne(string file)
{
    Console.WriteLine($"{Project()}.1 Solve for: " + file);
    var sw = System.Diagnostics.Stopwatch.StartNew();

    var rotations = File.ReadAllLines(FilePath(file));

    long position = 50;
    long timesAtZero = 0;

    foreach (var r in rotations)
    {
        if (string.IsNullOrWhiteSpace(r)) continue;

        long clicks = long.Parse(r.Substring(1));
        if (r[0] == 'L')
        {
            position -= clicks;
        } else
        {
            position += clicks;        
        }
        position %= 100;
        if (position == 0) timesAtZero++;
    } 

    sw.Stop();   
    Console.WriteLine("Times at zero: " + timesAtZero);
    Console.WriteLine("Runtime: " + sw.ElapsedMilliseconds.ToString("0.000") + " ms");
    Console.WriteLine("");
}

SolveOne("ex.txt");
SolveOne("in.txt");

void SolveTwo(string file)
{
    Console.WriteLine($"{Project()}.2 Solve for: " + file);
    var sw = System.Diagnostics.Stopwatch.StartNew();

    var rotations = File.ReadAllLines(FilePath(file));

    long position = 50;
    long timesAtZero = 0;

    foreach (var r in rotations)
    {
        if (string.IsNullOrWhiteSpace(r)) continue;

        long clicks = long.Parse(r.Substring(1));        
        if (r[0] == 'L')
        {
            if (position == 0) position = 100;

            while (clicks > 0)
            {
                if (clicks < position)
                {
                    position -= clicks;
                    clicks = 0;                     
                } else
                {
                    clicks -= position;
                    position = 100;   
                    timesAtZero++;     
                }
            }
        } else
        {
            while (clicks > 0)
            {
                if (position == 100) position = 0;

                if (clicks + position < 100)
                {
                    position += clicks;
                    clicks = 0;                     
                } else
                {
                    clicks -= 100 - position;
                    position = 0;   
                    timesAtZero++;                  
                }
            }
        }
        position %= 100;
    }    

    sw.Stop();
    Console.WriteLine("Times at zero: " + timesAtZero);
    Console.WriteLine("Runtime: " + sw.ElapsedMilliseconds.ToString("0.000") + " ms");
    Console.WriteLine("");
}

SolveTwo("ex.txt");
SolveTwo("in.txt");
