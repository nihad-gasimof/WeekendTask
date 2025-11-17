using WeekEndTask;

Bank bank = new Bank();
AccountService accountService = new AccountService(Bank.Users);
var bankService = new BankService();
var menuService = new MenuService(accountService,bankService);
menuService.ShowMenu();
