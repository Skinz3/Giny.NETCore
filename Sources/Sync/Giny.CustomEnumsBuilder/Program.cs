using Giny.Core;
using Giny.EnumsBuilder.Generation;
using Giny.IO;
using Giny.IO.D2I;
using Giny.IO.D2O;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Formatting;

namespace Giny.EnumsBuilder
{
    class Program
    {
        private const string OutputPath = "Generated/";
        static void Main(string[] args)
        {
            Logger.DrawLogo();

            var clientPath = ClientConstants.ClientPath;

            D2IManager.Initialize(Path.Combine(clientPath, ClientConstants.i18nPath));

            if (!Directory.Exists(OutputPath))
            {
                Directory.CreateDirectory(OutputPath);
            }

            List<D2OReader> d2oReaders = new List<D2OReader>();

            string d2oDirectory = Path.Combine(clientPath, ClientConstants.D2oDirectory);

            foreach (var file in Directory.GetFiles(d2oDirectory))
            {
                if (Path.GetExtension(file) == ".d2o")
                    d2oReaders.Add(new D2OReader(file));
            }

            Logger.Write("Building enums ... ", Channels.Info);

            Build(d2oReaders);

            Logger.Write("Custom enums generated successfully.", Channels.Info);

            Console.ReadLine();
        }

        private static void Build(List<D2OReader> d2oReaders)
        {
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (type.IsSubclassOf(typeof(CustomEnum)))
                {
                    CustomEnum @enum = (CustomEnum)Activator.CreateInstance(type);
                    string filename = @enum.ClassName + ".cs";
                    string filepath = Path.Combine(OutputPath, filename);
                    string fileContent = @enum.Generate(d2oReaders);

                    SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(fileContent);
                    SyntaxNode formattedNode = Formatter.Format(syntaxTree.GetRoot(), new AdhocWorkspace());
                    File.WriteAllText(filepath, formattedNode.ToFullString());


                    Logger.Write(filename + " generated.");
                }
            }
        }
    }
}
