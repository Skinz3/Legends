using Legends.Core.DesignPattern;
using Legends.Core.Utils;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Core.CSharp
{
    public class InjectionManager : Singleton<InjectionManager>
    {
        Logger logger = new Logger();

        private string[] DEFAULT_REFERENCES_ASSEMBLIES = new string[]
        {
            "System.dll",
            "System.Linq.dll",
        };
        private Assembly[] ReferencedAssemblies
        {
            get;
            set;
        }
        public void Initialize(Assembly[] referencedAssemblies)
        {
            ReferencedAssemblies = referencedAssemblies;
        }
        public Type GetScript(string path)
        {
            return GetScripts(path).FirstOrDefault();
        }

        public Type[] GetScripts(string path)
        {
            string[] files = Directory.GetFiles(path);

            string language = CodeDomProvider.GetLanguageFromExtension(Path.GetExtension(files[0]));
            CodeDomProvider codeDomProvider = CodeDomProvider.CreateProvider(language);

            var compilerParams = GetCompilerParameters();

            CompilerResults results = codeDomProvider.CompileAssemblyFromFile(compilerParams,
                                                           files);

            if (NotifyErrors(results))
            {
                return new Type[0];
            }
            else
            {
                return results.CompiledAssembly.GetTypes();
            }
        }
        private CompilerParameters GetCompilerParameters()
        {
            CompilerParameters compilerParams = new CompilerParameters();
            compilerParams.GenerateExecutable = false;
            compilerParams.GenerateInMemory = true;
            compilerParams.IncludeDebugInformation = false;

            foreach (var assembly in DEFAULT_REFERENCES_ASSEMBLIES)
            {
                compilerParams.ReferencedAssemblies.Add(assembly);
            }

            foreach (var assembly in ReferencedAssemblies)
            {
                compilerParams.ReferencedAssemblies.Add(assembly.Location);
            }
            return compilerParams;
        }
        private bool NotifyErrors(CompilerResults results)
        {
            if (!results.Errors.HasErrors)
            {
                return false;
            }
            else
            {
                foreach (var error in results.Errors)
                {
                    logger.Write(error.ToString(), MessageState.WARNING);
                }
                return true;
            }
        }
    }
}
