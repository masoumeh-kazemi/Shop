namespace Common.Domain.Exceptions;

public class NullOrEmptyDomainDataException : BaseDomainException
{
    public NullOrEmptyDomainDataException()
    {

    }

    public NullOrEmptyDomainDataException(string message) : base(message)
    {

    }

    public static void CheckString(string value, string nameOffField)
    {
        if (string.IsNullOrEmpty(value))
            throw new NullOrEmptyDomainDataException($"{nameOffField} is null or empty");
    }
}