using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularPipelines.Build;
public class DotnetPipelinesSettings
{
	public List<string> Configure = new();
	public List<string> Modules = new();
}
