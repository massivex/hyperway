    using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Certificates.Validator
{
    using System.IO;

    public class ValidatorLoader
    {

        private Dictionary<String, Object> objectStorage = new Dictionary<string, object>();

        public static ValidatorLoader newInstance()
        {
            return new ValidatorLoader();
        }

        private ValidatorLoader()
        {

        }

        public ValidatorLoader put(String key, Object value)
        {
            this.objectStorage.Add(key, value);

            return this;
        }

        public ValidatorGroup build(FileInfo path) // throws IOException, ValidatorParsingException
        {
            ValidatorGroup validatorGroup;
            using (Stream inputStream = File.OpenRead(path.FullName))
            {
                validatorGroup = this.build(inputStream);
            }
            return validatorGroup;
        }

        public ValidatorGroup build(Stream inputStream) // throws ValidatorParsingException
        {
            return ValidatorLoaderParser.parse(inputStream, new Dictionary<string, object>(this.objectStorage));
        }
    }
}
