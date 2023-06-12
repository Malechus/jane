using System;
using System.Collections.Generic;

namespace jane.Models;

public partial class WeeklyChore
{
	public int Id { get; set; }

	public string ChoreName { get; set; } = null!;

	public string? WeekOne { get; set; }

	public string? WeekTwo { get; set; }

	public string? WeekThree { get; set; }

	public string? WeekFour { get; set; }

	public ulong Active { get; set; }

	public string? Notes { get; set; }
	
	public static string? GetOwnerByWeek(WeeklyChore weeklyChore, int week)
	{
		if(week == 1) return weeklyChore.WeekOne;
		else if(week == 2) return weeklyChore.WeekTwo;
		else if(week == 3) return weeklyChore.WeekThree;
		else if(week == 4) return weeklyChore.WeekFour;
		else throw new ArgumentException();
    }
}
