﻿using System;
using System.IO;
using System.Reflection;

namespace Avalonia.Designer.HostApp.NetFX
{
    class Program
    {
#if NET461
        private static string s_appDir;
        
        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string assemblyPath = Path.Combine(s_appDir, new AssemblyName(args.Name).Name + ".dll");
            if (File.Exists(assemblyPath) == false) return null;
            Assembly assembly = Assembly.LoadFrom(assemblyPath);
            return assembly;
        }
        public static void Main(string[] args)
        {
            s_appDir = Directory.GetCurrentDirectory();
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            foreach (var dll in Directory.GetFiles(s_appDir, "*.dll"))
            {
                try
                {
                    Console.WriteLine("Loading " + dll);
                    Assembly.LoadFile(dll);
                }
                catch
                {
                    
                }
            }
            Exec(args);
        }

        static void Exec(string[] args)
#elif NETCOREAPP2_0
        public static void Main(string[] args)
#endif
        {
            Avalonia.DesignerSupport.Remote.RemoteDesignerEntryPoint.Main(args);
        }

    }
}
