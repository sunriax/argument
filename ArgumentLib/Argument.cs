using RaGae.ArgumentLib.MarshalerLib;
using RaGae.ReflectionLib;
using RaGae.BootstrapLib.Loader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ErrorCode = RaGae.ArgumentLib.MarshalerLib.ErrorCode;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace RaGae.ArgumentLib
{
    public class Argument
    {
        private ArgumentConfig config;
        private Reflection reflection;
        private Dictionary<ArgumentSchema, Marshaler> marshalers;
        private Iterator<string> currentArgument;
        private List<string> argumentsFound;

        public Argument(string configFile, IEnumerable<string> args, IEnumerable<ArgumentSchema> schema = null)
        {
            LoadConfig(configFile, schema);
            LoadMarshaler(configFile);

            this.marshalers = new Dictionary<ArgumentSchema, Marshaler>();
            this.argumentsFound = new List<string>();

            ParseSchema();
            ParseArgumentStrings(new List<string>(args));
        }

        private void LoadConfig(string configFile, IEnumerable<ArgumentSchema> schema)
        {
            try
            {
                this.config = Loader.LoadConfigSection<ArgumentConfig>(configFile, nameof(ArgumentConfig));
            }
            catch (Exception ex)
            {
                if(ex.InnerException is ArgumentException)
                    throw new ArgumentException((ex.InnerException as ArgumentException).ErrorCode, (ex.InnerException as ArgumentException).ErrorParameter);

                throw new ArgumentException(ErrorCode.GLOBAL, string.Format(ArgumentResource.ConfigNotFound, configFile));
            }

            if (schema != null)
                this.config.Schema = schema;

            if (string.IsNullOrWhiteSpace(this.config.Delimiter))
                this.config.Delimiter = ArgumentResource.StandardDelimiter;
        }

        private void LoadMarshaler(string configFile)
        {
            try
            {
                this.reflection = new Reflection(configFile, 0);
            }
            catch (ReflectionException ex)
            {
                throw new ArgumentException(ErrorCode.REFLECTION, ex.ErrorMessage());
            }
        }

        private void ParseSchema()
        {
            if (this.config.Schema == null)
                throw new ArgumentException(ErrorCode.EMPTY, nameof(this.config.Schema));

            foreach (ArgumentSchema argument in this.config.Schema)
            {
                ParseSchemaArgument(argument);
            }
        }

        private void ParseSchemaArgument(ArgumentSchema argument)
        {
            try
            {
                Marshaler marshaler = reflection.GetInstanceByProperty<Marshaler>(nameof(marshaler.Schema), argument.Marshaler);
                this.marshalers.Add(argument, marshaler);
            }
            catch (ReflectionException ex)
            {
                throw new ArgumentException(ErrorCode.REFLECTION, ex.ErrorMessage());
            }
        }

        private void ParseArgumentStrings(List<string> argumentList)
        {
            for (this.currentArgument = new Iterator<string>(argumentList); this.currentArgument.HasNext;)
            {
                string argumentString = currentArgument.Next();

                if (argumentString.Length > 0 && argumentString.ElementAt(0)
                                                               .ToString()
                                                               .IndexOfAny(config.Delimiter.ToCharArray()) != -1)
                    ParseArgumentString(argumentString.Substring(1).ToLower());
                else
                    throw new ArgumentException(ErrorCode.INVALID_PARAMETER, argumentString);
            }
        }

        private void ParseArgumentString(string argumentString)
        {
            argumentString = argumentString.ToLower();

            if (!this.marshalers
                    .Any(x => x.Key.Argument
                    .Contains(argumentString)))
                throw new ArgumentException(ErrorCode.UNEXPECTED_ARGUMENT, argumentString, null);

            Marshaler m = this.marshalers
                              .Where(x => x.Key.Argument
                              .Any(k => k == argumentString))
                              .FirstOrDefault()
                              .Value;

            this.argumentsFound.Add(argumentString);

            try
            {
                m.Set(currentArgument);
            }
            catch (BaseArgumentException errorCode)
            {
                errorCode.ErrorArgumentId = argumentString;
                throw errorCode;
            }
        }

        public T GetValue<T>(string argumentString)
        {
            argumentString = argumentString.Trim().ToLower();

            ArgumentSchema s = this.marshalers.Keys
                                   .Where(a => a.Argument.Contains(argumentString))
                                   .FirstOrDefault() ?? throw new ArgumentException(ErrorCode.INVALID_PARAMETER, argumentString);

            Marshaler m = this.marshalers
                              .Where(a => a.Key.Argument.Contains(argumentString))
                              .FirstOrDefault().Value;

            try
            {
                if((T)(m.Value) == null)
                    throw new NullReferenceException();

                return (T)(m.Value);
            }
            catch (NullReferenceException)
            {
                if (s.Required == true)
                    throw new ArgumentException(ErrorCode.MISSING, string.Join(ArgumentResource.Separator, s.Argument), null);

                return default;
            }
        }

        public IEnumerable<string> ArgumentsFound => argumentsFound;

        public IEnumerable<IEnumerable<string>> Schema
        {
            get
            {
                List<IEnumerable<string>> schemaList = new List<IEnumerable<string>>();

                foreach (IEnumerable<string> marshalerSchema in this.marshalers.Keys.Select(x => x.Argument))
                {
                    schemaList.Add(marshalerSchema);
                }

                return schemaList;
            }
        }
    }
}
