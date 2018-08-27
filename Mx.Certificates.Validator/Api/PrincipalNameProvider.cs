namespace Mx.Certificates.Validator.Api
{

    /// <summary>
    /// Used by PrincipalNameValidator to implement validation logic.
    /// </summary>
    /// <typeparam name="T">Principal type</typeparam>
    public interface IPrincipalNameProvider<T>
    {
        bool Validate(T value);
    }

}
