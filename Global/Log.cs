using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToolGame;
public static class Log
{
	public static void Info(string info)
	{
		GD.Print(info);
		Console.WriteLine(info);
	}
}
