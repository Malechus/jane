using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jane.Models
{
	public class User
	{
		public int Id { get; set; }
		
		public string Name { get; set; }
		
		public string Group { get; set; }
		
		public string DayOff { get; set; }
    }
}