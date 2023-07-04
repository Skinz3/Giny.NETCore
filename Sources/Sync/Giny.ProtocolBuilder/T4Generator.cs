using Microsoft.VisualStudio.TextTemplating;
using Mono.TextTemplating;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.ProtocolBuilder
{
    /// <summary>
    /// Classe encapsulant des procédés de génération de code utilisant Mono.TextTemplating.
    /// https://docs.microsoft.com/fr-fr/visualstudio/modeling/code-generation-and-t4-text-templates?view=vs-2022
    /// </summary>
    public class T4Generator
    {
        /// <summary>
        /// Emplacement du template T4.
        /// </summary>
        public string TemplatePath
        {
            get;
            private set;
        }
        /// <summary>
        /// Règles de génération d'assembly au moment de la compilation.
        /// </summary>
        public TemplateGenerator Host
        {
            get;
            private set;
        }
        /// <summary>
        /// Permet de passer des paramètres au moment de la génération.
        /// </summary>
        private ITextTemplatingSession Session
        {
            get;
            set;
        }
        private TemplatingEngine Engine
        {
            get;
            set;
        }
        private CompiledTemplate CompiledTemplate
        {
            get;
            set;
        }
        public T4Generator(string templatePath)
        {
            this.TemplatePath = templatePath;
            this.Host = new TemplateGenerator();
            this.Session = Host.GetOrCreateSession();

            Engine = new TemplatingEngine();




        }
        /// <summary>
        /// Ajoute une référence au moment de la génération
        /// </summary>
        /// <param name="import">namespace</param>
        public void AddReference(string import)
        {
            this.Host.Refs.Add(import);
        }

        public void Compile()
        {
            CompiledTemplate = Engine.CompileTemplate(File.ReadAllText(TemplatePath), Host);
        }
        /// <summary>
        /// Affecte un paramètre de template T4.
        /// Exemple dans un fichier T4 : <#@ parameter name="helper" type="ByTel.Tools.CodeGen.Helpers.CSharpHelper" #>
        /// </summary>
        /// <param name="parameterName">Nom du paramètre</param>
        /// <param name="value">Instance du paramètre</param>
        public void Bind(string parameterName, object value)
        {
            Session[parameterName] = value;
        }
        /// <summary>
        /// Débute une génération de code source C# en fonction des paramètres de session.
        /// Une fois généré, le fichier sera compilé.
        /// </summary>
        /// <returns>Resultat de la compilation du fichier généré.</returns>
        public string Generate()
        {
            return CompiledTemplate.Process();
        }
    }

}
