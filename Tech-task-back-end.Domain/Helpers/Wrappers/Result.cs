namespace Tech_task_back_end.Domain.Helpers.Wrappers;

public class Result<T>
{
    private Result(T? value, bool isSuccess, string message)
    {
        Value = value;
        IsSuccess = isSuccess;
        Message = message;
    }

    public bool IsSuccess { get; }
    public string Message { get; }
    public T? Value { get; private set; }

    public static Result<T> Ok(T value)
    {
        return new Result<T>(value, true, string.Empty);
    }

    public static Result<T> Err(string message)
    {
        return new Result<T>(default, false, message);
    }
}

public class Result
{
    private Result(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }

    public bool IsSuccess { get; private set; }
    public string Message { get; private set; }

    public static Result Ok()
    {
        return new Result(true, string.Empty);
    }

    public static Result Err(string message)
    {
        return new Result(false, message);
    }
}