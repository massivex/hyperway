using System;
using System.Collections.Generic;

namespace Mx.Certificates.Validator
{
    using System.IO;

    public class ValidatorLoader
    {

        private readonly Dictionary<String, Object> objectStorage = new Dictionary<string, object>();

        public static ValidatorLoader NewInstance()
        {
            return new ValidatorLoader();
        }

        private ValidatorLoader()
        {

        }

        public ValidatorLoader Put(String key, Object value)
        {
            this.objectStorage.Add(key, value);

            return this;
        }

        public ValidatorGroup Build(FileInfo path)
        {
            ValidatorGroup validatorGroup;
            using (Stream inputStream = File.OpenRead(path.FullName))
            {
                validatorGroup = this.Build(inputStream);
            }
            return validatorGroup;
        }

        public ValidatorGroup Build(Stream inputStream)
        {
            return ValidatorLoaderParser.Parse(inputStream, new Dictionary<string, object>(this.objectStorage));
        }
    }
}
