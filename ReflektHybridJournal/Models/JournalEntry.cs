using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflektHybridJournal.Models;
public class JournalEntry
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public DateTime EntryDate { get; set; }
    public string Title { get; set; } = "";
    public string Content { get; set; } = "";
    public string? PrimaryMood { get; set; }
}
