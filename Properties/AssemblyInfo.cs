using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


[assembly: AssemblyProduct("Polyriser")]
[assembly: AssemblyTitle("Polyriser")]
[assembly: AssemblyDescription("An alarm clock for polyphasic sleepers.")]
[assembly: AssemblyCopyright("Copyright © 2010 by John Simon")]
[assembly: AssemblyVersion("0.17.0045")]

#if DEBUG
	[assembly: AssemblyConfiguration("Debug")]
#else
	[assembly: AssemblyConfiguration("Release")]
#endif

[assembly: ComVisible(false)]