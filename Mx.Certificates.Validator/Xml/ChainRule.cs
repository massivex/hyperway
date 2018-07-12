namespace Mx.Certificates.Validator.Xml
{
    public class ChainRule : ValidationRule
    {
        public string RootBucketReference { get; set; }
        public string IntermediateBucketReference { get; set; }
    }
}