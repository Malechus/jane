using System;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration.Assemblies;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jane.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace jane.Services
{
    public class DbService
    {
        private IConfigurationRoot Config;

        private Settings Settings;

        private string ConnStr;

        public DbService(IConfigurationRoot config)
        {
            Config = config;
            Settings = config.GetRequiredSection("Settings").Get<Settings>();
            ConnStr = Settings.HouseholdConnection;
        }

        private int setWeek(DateOnly date)
        {
            int week;

            if (date.DayNumber <= 6) week = 1;
            else if (date.DayNumber <= 13) week = 2;
            else if (date.DayNumber <= 20) week = 3;
            else if (date.DayNumber <= 27) week = 4;
            else week = 1;

            return week;
        }

        public void FindOrCreateChoreList(DateTime choreDate)
        {
            HouseholdContext context = new HouseholdContext(ConnStr);

            DateOnly date = DateOnly.FromDateTime(choreDate);
            int week = setWeek(date);

            TransactionalChore? checker = context.TransactionalChores
                .Where(c => c.Date == date)
                .FirstOrDefault();

            TransactionalChore? wChecker = context.TransactionalChores
                .Where(c => c.WeekOf == week && c.Completed == 0)
                .FirstOrDefault();

            if (checker is not null && wChecker is not null)
            {
                return;
            }
            else if (checker is null && wChecker is not null)
            {
                createDailyChoreList(date);
                return;
            }
            else
            {
                createDailyChoreList(date);
                createWeeklyChoreList(date);
                return;
            }
        }

        private void createDailyChoreList(DateOnly date)
        {
            HouseholdContext context = new HouseholdContext(ConnStr);
            string dayOfWeek = date.DayOfWeek.ToString();
            int week = setWeek(date);

            //This table represents one day's worth of chores, so should always be relatively small. We can handle listing the entire table to make manipulating it easier.
            List<DailyChore> dailyChores = context.DailyChores.ToList();

            List<TransactionalChore> tChores = new List<TransactionalChore>();

            foreach (DailyChore d in dailyChores)
            {
                TransactionalChore tchore = new TransactionalChore
                {
                    WeekOf = week,
                    Date = date,
                    ChoreId = d.Id,
                    ChoreName = d.ChoreName,
                    Completed = 0,
                    Owner = d.GetOwnerByDay(dayOfWeek).ToString()
                };

                tChores.Add(tchore);
            }

            context.TransactionalChores.AddRange(tChores);
            context.SaveChanges();
        }

        private void createWeeklyChoreList(DateOnly date)
        {
            HouseholdContext context = new HouseholdContext(ConnStr);
            int week = setWeek(date);

            List<WeeklyChore> weeklyChores = context.WeeklyChores.ToList();

            List<TransactionalChore> tChores = new List<TransactionalChore>();

            foreach (WeeklyChore w in weeklyChores)
            {
                TransactionalChore tchore = new TransactionalChore
                {
                    WeekOf = week,
                    Date = date,
                    ChoreId = w.Id,
                    ChoreName = w.ChoreName,
                    Completed = 0,
                    Owner = WeeklyChore.GetOwnerByWeek(w, week)
                };

                tChores.Add(tchore);
            }

            context.TransactionalChores.AddRange(tChores);
            context.SaveChanges();
        }

        public string GetIncompleteChores()
        {
            HouseholdContext context = new HouseholdContext(ConnStr);

            List<User> users = context.Users.ToList();

            List<TransactionalChore> incompleteChores = context.TransactionalChores
                .Where(c => c.Completed == 0)
                .ToList();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("```");

            foreach (User u in users)
            {
                sb.AppendLine(u.FirstName);

                foreach (TransactionalChore tc in incompleteChores.Where(tc => tc.Owner == u.FirstName))
                {
                    sb.AppendLine(tc.ChoreName);
                }

                sb.AppendLine(".....");
            }

            sb.AppendLine("```");

            return sb.ToString();
        }
    }
}