using RaGae.ArgumentLib.MarshalerLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RaGae.ArgumentLib
{
    public class ArgumentConfig
    {
        private IEnumerable<ArgumentSchema> schema;

        public IEnumerable<ArgumentSchema> Schema
        {
            get => schema;
            set
            {
                if (value != null && value.Count() > 0)
                {
                    value.ToList().ForEach(e =>
                    {
                        if (e.Error != ErrorCode.OK)
                            throw new ArgumentException(e.Error, nameof(e.Argument));
                    });
                }

                this.schema = value;
            }
        }
        public string Delimiter { get; set; }
    }

    public class ArgumentSchema
    {
        private IEnumerable<string> argument;

        public IEnumerable<string> Argument
        {
            get => this.argument;
            set
            {
                this.Error = ErrorCode.OK;

                if (value == null || value.Count() == 0)
                {
                    this.Error = ErrorCode.EMPTY;
                    return;
                }

                value.ToList().ForEach(s =>
                {
                    if(s.Select(c => char.IsLetterOrDigit(c)).Any(b => b == false))
                        this.Error = ErrorCode.INVALID;
                });

                this.argument = value.Select(s => s.ToLower());
            }
        }

        public string Marshaler { get; set; }

        public bool Required { get; set; }

        public ErrorCode Error { get; private set; }
    }
}
