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
    var digits = new List<List<long>>();
    var ops = new List<string>();
    long sum = 0;

    foreach (var line in lines)
    {
        if (line.Length == 0) continue;
        if (line[0] == '+' || line[0] == '*')
        {
            ops = line.Split(" ").Select(l => l.Trim()).Where(l => l.Length > 0).ToList();
        } else
        {
            digits.Add(line.Split(" ").Select(l => l.Trim()).Where(l => l.Length > 0).Select(l => long.Parse(l)).ToList());
        }
    }

    for (int i = 0; i < ops.Count; i++)
    {
        long colSum = 0;
        switch (ops[i])
        {
            case "+":
                {
                    foreach (var dig in digits)
                    {
                        colSum += dig[i];
                    }
                    break;
                }
            case "*":
                {
                    colSum = 1;
                    foreach (var dig in digits)
                    {
                        colSum *= dig[i];
                    }
                    break;
                }
        }
        sum += colSum;
    }

    sw.Stop();
    Console.WriteLine("Sum: " + sum);
    Console.WriteLine("Runtime: " + (sw.ElapsedTicks / (Stopwatch.Frequency / 1_000.0)).ToString("0.000") + " ms");
    Console.WriteLine("");
}

SolveOne("ex.txt"); // Expect 4277556
SolveOne("in.txt");


void SolveTwo(string file)
{
    Console.WriteLine($"{Project()}.2 Solve for: " + file);
    var sw = System.Diagnostics.Stopwatch.StartNew();

    var lines = File.ReadAllLines(FilePath(file))!;
    var digits = lines.Take(lines.Length - 1).ToArray();
    var ops = lines.Skip(lines.Length - 1).Single();
    long sum = 0;

    char currentOp = '+';
    var operands = new List<long>();
    bool inOp = false;
    for (int i = 0; i < ops.Length; i++)
    {
        if (digits.All(d => d[i] == ' ')) // All blank, all terms in place
        {
            inOp = false;
            if (currentOp == '+') sum += operands.Sum();
            else {
                long mulSum = 1;
                foreach (var operand in operands) mulSum *= operand;
                sum += mulSum;
            }
            operands.Clear();
            continue;
        }
        if (ops[i] != ' ') {
            currentOp = ops[i];
            inOp = true;
        }
        string column = "";
        for (int j = 0; j < digits.Length; j++)
        {
            column += digits[j][i];
        }
        if (column.Trim() != "") {
            operands.Add(long.Parse(column.Trim()));
            // Console.WriteLine($"{column} op {currentOp}");
        }
    }

    if (inOp)
    {
        if (currentOp == '+') sum += operands.Sum();
        else {
            long mulSum = 1;
            foreach (var operand in operands) mulSum *= operand;
            sum += mulSum;
        }        
    }

    sw.Stop();
    Console.WriteLine("Sum: " + sum);
    Console.WriteLine("Runtime: " + (sw.ElapsedTicks / (Stopwatch.Frequency / 1_000.0)).ToString("0.000") + " ms");
    Console.WriteLine("");
}

SolveTwo("ex.txt"); // Expect 3263827
SolveTwo("in.txt");
