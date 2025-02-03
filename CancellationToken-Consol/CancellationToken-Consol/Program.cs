Console.WriteLine("Press any key to cancel the operation");

//var longRunningTask = LongRunningTaskAsync();

//Console.ReadKey();
//Console.WriteLine("Key Press");

//await longRunningTask;


//async Task LongRunningTaskAsync()
//{
//    var i = 0;
//    while(i++ < 10)
//    {
//        Console.WriteLine($"Working {i}");
//        await Task.Delay( 1000 );
//        Console.WriteLine($"Completed {i}");
//    }
//}

//Eventhough we press the Key, Process continue until the process complete. In this case it will loop 10times and then ends.

var source = new CancellationTokenSource();

var longRunningTask = LongRunningTaskAsync(source.Token);

Console.ReadKey();
Console.WriteLine("Key Press");
source.Cancel();

await longRunningTask;


//async Task LongRunningTaskAsync(CancellationToken cancellation)
//{
//    try
//    {
//        var i = 0;
//        while (i++ < 10)
//        {
//            cancellation.ThrowIfCancellationRequested();
//            Console.WriteLine($"Working {i}");
//            await Task.Delay(1000);
//            Console.WriteLine($"Completed {i}");
//        }
//    }
//    catch (OperationCanceledException e)
//    {
//        Console.WriteLine("User Cancelled the operation.");
//    }
//}


// In the above method, even if yo press any key, it will complete the delay and then in the next iteration it will throw the exception.
//To fix that we can pass cancellation token to the Task.Delay operation.

async Task LongRunningTaskAsync(CancellationToken cancellationToken)
{
    try
    {
        var i = 0;
        while (i++ < 10)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Console.WriteLine($"Working {i}");
            await Task.Delay(1000, cancellationToken);
            Console.WriteLine($"Completed {i}");
        }
    }
    catch (OperationCanceledException e)
    {
        Console.WriteLine("User Cancelled the operation.");
    }

}