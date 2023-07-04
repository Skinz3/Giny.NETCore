using Giny.AS3;
using Giny.Core;
using Giny.Core.Extensions;
using Giny.ProtocolBuilder.Converters;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Formatting;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.VisualStudio.TextTemplating;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Giny.ProtocolBuilder.Profiles
{
    public abstract class Profile
    {
        public abstract string TemplateFileName
        {
            get;
        }
        public abstract string RelativeOutputPath
        {
            get;
        }
        public abstract string OutputDirectory
        {
            get;
        }

        public string TemplatePath
        {
            get
            {
                return Path.Combine(Path.Combine(Environment.CurrentDirectory, Constants.TEMPLATES_PATH), TemplateFileName);
            }
        }

        private string InputPath
        {
            get;
            set;
        }
        public virtual bool ParseMethods => true;

        public Profile(string inputPath)
        {
            this.InputPath = inputPath;
        }

        public abstract DofusConverter CreateDofusConverter(AS3File file);
        /// <summary>
        /// improve
        /// </summary>
        /// <returns></returns>
        public string GetRelativeOutputPath(AS3File file)
        {
            if (RelativeOutputPath == string.Empty)
                return string.Empty;

            var directory = Path.GetDirectoryName(file.FilePath);
            string dir = directory.Replace(RelativeOutputPath, string.Empty);
            dir = dir.UpperAfterChar('\\') + "\\";
            return dir;
        }

        public abstract bool Skip(AS3File file);

        public void Generate()
        {
            if (!Directory.Exists(InputPath))
            {
                Directory.CreateDirectory(InputPath);
            }

            Dictionary<string, AS3File> files = new Dictionary<string, AS3File>();

            foreach (var file in Directory.EnumerateFiles(InputPath, "*.*", SearchOption.AllDirectories).Select(x => new AS3File(x, ParseMethods)))
            {
                files.Add(file.ClassName, file);
            }

            var converters = files.Values.Where(x => !Skip(x)).Select(x => CreateDofusConverter(x)).ToArray();

            foreach (var converter in converters)
            {
                Logger.Write("Preparing " + converter.File.ClassName, Channels.Log);
                converter.Prepare(files);
            }

            foreach (var converter in converters)
            {
                Logger.Write("Post Preparing " + converter.File.ClassName, Channels.Log);
                converter.PostPrepare();
            }


            var text = File.ReadAllText(TemplatePath);

            T4Generator generator = new T4Generator(TemplatePath);

            generator.AddReference("Giny.AS3");
            generator.AddReference("Giny.ProtocolBuilder");

            generator.Compile();

            foreach (var converter in converters)
            {
                var as3File = converter.File;

                var directoryPath = Path.Combine(OutputDirectory, GetRelativeOutputPath(as3File));

                generator.Bind("Converter", converter);

                foreach (CompilerError error in generator.Host.Errors)
                {
                    Logger.Write("Compiler error :" + error.ErrorText + " line (" + error.Line + ")", Channels.Critical);
                }

                if (generator.Host.Errors.Count > 0)
                {
                    Console.Read();
                    Environment.Exit(0);
                }

                var output = generator.Generate();

             

                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);

                string filePath = directoryPath + as3File.ClassName + ".cs";

                File.WriteAllText(filePath, output);

                SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(output);
                SyntaxNode formattedNode = Formatter.Format(syntaxTree.GetRoot(), new AdhocWorkspace());
                File.WriteAllText(filePath, formattedNode.ToFullString());

                Logger.Write("Written : " + Path.GetFileNameWithoutExtension(as3File.FilePath), Channels.Log);

            }



        }
    }
}
