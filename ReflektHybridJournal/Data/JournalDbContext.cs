using ReflektHybridJournal.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflektHybridJournal.Data;
public class JournalDbContext
{
    readonly SQLiteAsyncConnection db;

    public JournalDbContext(string dbPath)
    {
        db = new SQLiteAsyncConnection(dbPath);
        db.CreateTableAsync<JournalEntry>().Wait();
    }

    public Task<List<JournalEntry>> GetEntriesAsync() =>
        db.Table<JournalEntry>()
          .OrderByDescending(e => e.EntryDate)
          .ToListAsync();

    public Task<JournalEntry?> GetEntryByDateAsync(DateTime date)
    {
        var start = date.Date;
        var end = start.AddDays(1);

#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
        return db.Table<JournalEntry>()
                 .Where(e => e.EntryDate >= start && e.EntryDate < end)
                 .FirstOrDefaultAsync();
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
    }



    public Task<int> SaveEntryAsync(JournalEntry entry) =>
        entry.Id == 0 ? db.InsertAsync(entry) : db.UpdateAsync(entry);

    public Task<int> DeleteEntryAsync(JournalEntry entry) =>
        db.DeleteAsync(entry);
}
