using System;

public class ParamsToTypeConverter<T> where T: class
{
    private T _boxedType;

    public ParamsToTypeConverter(object[] boxedType)
    {
        if (boxedType == null || boxedType.Length == 0)
            throw new ArgumentException($"At least one argument is required", $"{nameof(boxedType)}");
        string typeDebug = typeof(T).Name;
        T recivedInstance = boxedType[0] as T;
        if (recivedInstance == null)
            throw new ArgumentException($"Unexpected object, expected {typeof(T).Name} object", $"{nameof(boxedType)}");

        _boxedType = recivedInstance;
    }

    public T Get() => _boxedType;


}

