namespace TgBot_librarian
{
    public class DebtService
    {
        private readonly DebtDbContext _context;
        public DebtService(DebtDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }
        public string GetDebtText()
        {
            var debts = _context.Debts.ToList();
            var text = debts.Select((d, i) => $"{i + 1}. {d.Name} - {d.Book}").ToList();
            return text.Any() ? string.Join(Environment.NewLine, text) : "Боржників немає.";
        }
        public void UpdateDebtText(string newText)
        {
            var debts = newText
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line => line.Split('-', StringSplitOptions.TrimEntries))
                .Where(parts => parts.Length == 2)
                .Select(parts => new DebtDb { Name = parts[0], Book = parts[1] })
                .ToList();

            _context.Debts.RemoveRange(_context.Debts);
            _context.Debts.AddRange(debts);
            _context.SaveChanges();
        }
    }
}
