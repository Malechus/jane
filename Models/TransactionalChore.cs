using System;
using System.Collections.Generic;

namespace jane.Models;

public partial class TransactionalChore
{
	public int Id { get; set; }

	public int WeekOf { get; set; }
	
	public DateOnly Date { get; set; }

	public int ChoreId { get; set; }
	
	public string ChoreName { get; set; }

	public ulong Completed { get; set; }

	public DateTime? CompletedDatetime { get; set; }
	
	public string Owner { get; set; }
}
