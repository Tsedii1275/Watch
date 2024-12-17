using System;
using System.Timers;

public delegate void StopwatchEventHandler(string message);

public class Stopwatch
{
    private System.Timers.Timer timer; 
    private TimeSpan timeElapsed;
    public bool IsRunning { get; private set; }

    public event StopwatchEventHandler OnStarted;
    public event StopwatchEventHandler OnStopped;
    public event StopwatchEventHandler OnReset;

    public Stopwatch()
    {
        timer = new System.Timers.Timer(1000); 
        timer.Elapsed += Tick;
        timeElapsed = TimeSpan.Zero;
        IsRunning = false;
    }

    public void Start()
    {
        if (!IsRunning)
        {
            timer.Start();
            IsRunning = true;
            OnStarted?.Invoke("Stopwatch Started!");
        }
    }

    public void Stop()
    {
        if (IsRunning)
        {
            timer.Stop();
            IsRunning = false;
            OnStopped?.Invoke("Stopwatch Stopped!");
        }
    }

    public void Reset()
    {
        timer.Stop();
        timeElapsed = TimeSpan.Zero;
        IsRunning = false;
        OnReset?.Invoke("Stopwatch Reset!");
    }

    private void Tick(object sender, ElapsedEventArgs e)
    {
        timeElapsed = timeElapsed.Add(TimeSpan.FromSeconds(1));
        Console.Clear();
        Console.WriteLine($"Time Elapsed: {timeElapsed}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Stopwatch stopwatch = new Stopwatch();
        
        stopwatch.OnStarted += MessageHandler;
        stopwatch.OnStopped += MessageHandler;
        stopwatch.OnReset += MessageHandler;

        Console.WriteLine("Press S to start, T to stop, R to reset the stopwatch.");

        while (true)
        {
            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.S:
                    stopwatch.Start();
                    break;
                case ConsoleKey.T:
                    stopwatch.Stop();
                    break;
                case ConsoleKey.R:
                    stopwatch.Reset();
                    break;
                default:
                    Console.WriteLine("Invalid key. Use S, T, or R.");
                    break;
            }
        }
    }

    private static void MessageHandler(string message)
    {
        Console.WriteLine(message);
    }
}
