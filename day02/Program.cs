string Project() => System.Reflection.Assembly.GetCallingAssembly().GetName().Name!;
string ProjectDir() => System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetCallingAssembly().Location)!;
string FilePath(string file) => Path.Join(ProjectDir(), "../../../", file);

void SolveOne(string file)
{
    Console.WriteLine($"{Project()}.1 Solve for: " + file);
    var sw = System.Diagnostics.Stopwatch.StartNew();

    var rotations = File.ReadAllLines(FilePath(file));

    sw.Stop();   
    //Console.WriteLine("Times at zero: " + timesAtZero);
    Console.WriteLine("Runtime: " + sw.ElapsedMilliseconds.ToString("0.000") + " ms");
    Console.WriteLine("");
}

SolveOne("ex.txt");
//SolveOne("in.txt");

void SolveTwo(string file)
{
    Console.WriteLine($"{Project()}.2 Solve for: " + file);
    var sw = System.Diagnostics.Stopwatch.StartNew();

    var rotations = File.ReadAllLines(FilePath(file));

    sw.Stop();
    //Console.WriteLine("Times at zero: " + timesAtZero);
    Console.WriteLine("Runtime: " + sw.ElapsedMilliseconds.ToString("0.000") + " ms");
    Console.WriteLine("");
}

//SolveTwo("ex.txt");
//SolveTwo("in.txt");
