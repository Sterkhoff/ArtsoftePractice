namespace Core.HttpLogic.Services.Polly;
/// <summary>
/// Данные для конфигурации polly
/// </summary>
/// <param name="SleepDurationInSeconds">Кол-во секунд, между повторением запроса</param>
/// <param name="OnRetryFunc">Функция, выполняющаяся при каждом повторении запроса</param>
public record PollyData(int SleepDurationInSeconds,
    Func<Exception, int, TimeSpan, Task> OnRetryFunc);