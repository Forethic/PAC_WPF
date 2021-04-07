using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace MarkPad
{
    internal class Loader
    {
        const string LIBSFOLDER = "Libs";

        static readonly Dictionary<string, Assembly> _Libraries = new Dictionary<string, Assembly>();
        static readonly Dictionary<string, Assembly> _ReflectionOnlyLibraries = new Dictionary<string, Assembly>();

        [STAThread]
        public static void Main(string[] args)
        {
            AppDomain.CurrentDomain.AssemblyResolve += FindAssembly;

            PreloadUnmanagedLibraries();

            var app = new App();
            app.Run();
        }

        private static void PreloadUnmanagedLibraries()
        {
            var bittyness = "x86";
            if (IntPtr.Size == 8) bittyness = "x64";

            var assemblyName = Assembly.GetExecutingAssembly().GetName();

            var libraries = Assembly.GetExecutingAssembly().GetManifestResourceNames()
                .Where(s => s.StartsWith($"{assemblyName.Name}.{bittyness}.{assemblyName.Version}")).ToArray();

            var dirName = Path.Combine(Path.GetTempPath(), $"{assemblyName.Name}.{bittyness}.{assemblyName.Version}");

            if (!Directory.Exists(dirName))
                Directory.CreateDirectory(dirName);

            foreach (var lib in libraries)
            {
                var dllPath = Path.Combine(dirName, string.Join(".", lib.Split('.').Skip(3)));

                if (!File.Exists(dllPath))
                {
                    using (var stm = Assembly.GetExecutingAssembly().GetManifestResourceStream(lib))
                    {
                        // Copy the assembly to the temporary file
                        try
                        {
                            using (var outFile = File.Create(dllPath))
                            {
                                stm.CopyTo(outFile);
                            }
                        }
                        catch
                        {
                        }
                    }
                }

                // 我们必须直接加载 dll，因为临时文件夹不在程序的运行 PATH 中
                // 一旦加载成功，程序将会使用这个dll
                LoadLibrary(dllPath);
            }
        }

        private static Assembly FindAssembly(object sender, ResolveEventArgs args) => LoadAssembly(args.Name);

        internal static Assembly LoadAssembly(string fullName)
        {
            Assembly asm;

            var executingAssembly = Assembly.GetExecutingAssembly();

            var assemblyName = executingAssembly.GetName();

            var shortName = new AssemblyName(fullName).Name;
            if (_Libraries.ContainsKey(shortName))
                return _Libraries[shortName];

            var resourceName = $"{assemblyName.Name}.{LIBSFOLDER}.{shortName}.dll";
            var actualName = executingAssembly.GetManifestResourceNames().Where(n => string.Equals(n, resourceName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

            if (string.IsNullOrEmpty(actualName))
            {
                // 这个程序集一定与平台有关的
                var bittyness = "x86";
                if (IntPtr.Size == 0) bittyness = "x64";

                resourceName = $"{assemblyName.Name}.{LIBSFOLDER}.{bittyness}.{shortName}.dll";
                actualName = executingAssembly.GetManifestResourceNames().Where(n => string.Equals(n, resourceName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

                if (string.IsNullOrEmpty(actualName))
                {
                    _Libraries[shortName] = null;
                    return null;
                }

                var dirName = Path.Combine(Path.GetTempPath(), $"{assemblyName.Name}.{bittyness}.{assemblyName.Version}");
                string dllPath = Path.Combine(dirName, string.Join(".", actualName.Split('.').Skip(3)));

                if (!File.Exists(dllPath))
                {
                    _Libraries[shortName] = null;
                    return null;
                }

                asm = Assembly.LoadFile(dllPath);
                _Libraries[shortName] = asm;
                return asm;
            }

            using (var stream = executingAssembly.GetManifestResourceStream(actualName))
            {
                var data = new BinaryReader(stream).ReadBytes((int)stream.Length);

                byte[] debugData = null;
                if (executingAssembly.GetManifestResourceNames().Contains($"{assemblyName.Name}.{LIBSFOLDER}.{shortName}.pdb"))
                {
                    using (var debugStream = executingAssembly.GetManifestResourceStream($"{assemblyName.Name}.{LIBSFOLDER}.{shortName}.pdb"))
                    {
                        debugData = new BinaryReader(debugStream).ReadBytes((int)debugStream.Length);
                    }
                }

                if (debugData != null)
                {
                    asm = Assembly.Load(data, debugData);
                    _Libraries[shortName] = asm;
                    return asm;
                }
                asm = Assembly.Load(data);
                _Libraries[shortName] = asm;
                return asm;
            }
        }

        internal static Assembly FindReflectionOnlyAssembly(object sender, ResolveEventArgs args) => ReflectionOnlyLoadAssembly(args.Name);

        internal static Assembly ReflectionOnlyLoadAssembly(string fullName)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            var assemblyName = Assembly.GetExecutingAssembly().GetName();

            var shortName = new AssemblyName(fullName).Name;
            if (_ReflectionOnlyLibraries.ContainsKey(shortName))
                return _ReflectionOnlyLibraries[shortName];

            var resourceName = $"{assemblyName.Name}.{LIBSFOLDER}.{shortName}.dll";

            if (!executingAssembly.GetManifestResourceNames().Contains(resourceName))
            {
                _ReflectionOnlyLibraries[shortName] = null;
                return null;
            }

            using (var stream = executingAssembly.GetManifestResourceStream(resourceName))
            {
                var data = new BinaryReader(stream).ReadBytes((int)stream.Length);

                var asm = Assembly.ReflectionOnlyLoad(data);
                _ReflectionOnlyLibraries[shortName] = asm;
                return asm;
            }
        }


        [DllImport("kernel32.dll")]
        private static extern IntPtr LoadLibrary(string dllToLoad);
    }
}