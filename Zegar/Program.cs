using System;




public class Zegar {
    protected int time_hours;
    protected int time_minutes;
    protected int time_seconds;
    public Zegar(int time_hours, int time_minutes, int time_seconds)
    {
        this.time_hours = time_hours;
        this.time_minutes = time_minutes;
        this.time_seconds = time_seconds;
    }
    public virtual string DisplayTime()
    {
        string time = string.Empty;

        if (time_hours < 10)
        {
            time += $"0{time_hours}:";
        }
        else
        {
            time += $"{time_hours}:";
        }
        if (time_minutes < 10)
        {
            time += $"0{time_minutes}:";
        }
        else
        {
            time += $"{time_minutes}:";
        }
        if (time_seconds < 10)
        {
            time += $"0{time_seconds}";
        }
        else
        {
            time += $"{time_seconds}";
        }
        return time;
    }
    public virtual void PassTime()
    {
        if (this.time_seconds == 59)
        {
            this.time_seconds = 0;

            if(this.time_minutes == 59)
            {
                this.time_minutes = 0;
                if (this.time_hours == 23)
                {
                    this.time_hours = 0;
                } else
                {
                    this.time_hours++;
                }
            }
            else
            {
                this.time_minutes++;
            }
        } else
        {
            this.time_seconds++;
        }
    }
}
public class RandomZegar : Zegar
{
    private int random_range;
    public RandomZegar(int time_hours, int time_minutes, int time_seconds, int random_range) : base(time_hours, time_minutes, time_seconds)
    {
        this.random_range = random_range;
    }
    public override void PassTime() { 
        Random rand = new Random();
        for(int i = 0; i < rand.Next(1,random_range+1); i++)
        {
            base.PassTime();
        }
    }

}
public class AlarmZegar : Zegar
{
    private int alarm_hours;
    private int alarm_minutes;
    private int alarm_seconds;
    public AlarmZegar(int time_hours, int time_minutes, int time_seconds, int alarm_hours, int alarm_minutes, int alarm_seconds) : base(time_hours, time_minutes, time_seconds)
    {
        this.alarm_hours = alarm_hours;
        this.alarm_minutes = alarm_minutes;
        this.alarm_seconds = alarm_seconds;
    }
    public override void PassTime()
    {
        base.PassTime();

        if (this.time_seconds == this.alarm_seconds && this.time_minutes == alarm_minutes && this.time_hours == this.alarm_hours)
        {
            Console.WriteLine("ALARM ALARM ALARM ALARM");
        }
    }
}

public class CountdownZegar : Zegar
{
    public CountdownZegar(int time_hours, int time_minutes, int time_seconds) : base(time_hours,time_minutes,time_seconds) { 
    }
    public override void PassTime()
    {
        if (this.time_seconds == 0)
        {
            this.time_seconds = 59;

            if (this.time_minutes == 0)
            {
                this.time_minutes = 59;
                if (this.time_hours == 0)
                {
                    this.time_hours = 23;
                    Console.WriteLine("ZERO, RESET TIMERA");
                }
                else
                {
                    this.time_hours--;
                }
            }
            else
            {
                this.time_minutes--;
            }
        }
        else
        {
            this.time_seconds--;
        }
    }
}

class Program
{
    public static void Main(string[] args)
    {
        Zegar z1 = new Zegar(1, 31, 12);
        RandomZegar z2 = new RandomZegar(12, 58, 31,5);
        AlarmZegar z3 = new AlarmZegar(15, 21, 59, 15, 22, 11);
        CountdownZegar z4 = new CountdownZegar(17, 32, 0);

        List<Zegar> zegars = new List<Zegar>();
        zegars.Add(z1);
        zegars.Add(z2);
        zegars.Add(z3);
        zegars.Add(z4);
        for(int i = 0; i < 60; i++)
        {
            foreach (Zegar z in zegars)
            {
                z.PassTime();
                Console.WriteLine(z.GetType().Name);
                Console.WriteLine(z.DisplayTime());
                Console.WriteLine("===================");
            }
            Thread.Sleep(1000);
            Console.Clear();
        }

    }
}