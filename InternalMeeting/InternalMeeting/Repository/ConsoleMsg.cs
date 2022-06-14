namespace InternalMeeting.Repository
{
    public static class ConsoleMsg
    {
        public static void WrongInputMsg()
        {
            Console.WriteLine("Wrong input!!! press anny key to continue");
            Console.ReadKey();
        }
        public static void EmptyInputMsg()
        {
            Console.WriteLine("Input must bee not empty!!! press anny key to continue");
            Console.ReadKey();
        }
        public static void ContinueMsg()
        {
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Press anny key to continue");
            Console.ReadKey();
        }
        public static void BackToPreviouslyWindowMsg(int commandNumber)
        {
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine($"{commandNumber} - Go back to previously window");
            Console.WriteLine("---------------------------------------------------");
        }
        public static void FilterSelectionMsg()
        {
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("1 - Filter by description");
            Console.WriteLine("2 - Filter by responsible person");
            Console.WriteLine("3 - Filter by category");
            Console.WriteLine("4 - Filter by type");
            Console.WriteLine("5 - Filter by dates");
            Console.WriteLine("6 - Filter by the number of attendees");
            Console.WriteLine("7 - Return to previously menu");
            Console.WriteLine("---------------------------------------------------");
        }
        public static void FirstSelection(string Name)
        {
            Console.Clear();
            Console.WriteLine($"Welcome {Name}");
            Console.WriteLine("Select command number what you want to do ");
            Console.WriteLine("1 - Create meeting");
            Console.WriteLine("2 - Delete meeting");
            Console.WriteLine("3 - Add Or Delete person to the meeting");
            Console.WriteLine("4 - List all the meetings");
            Console.WriteLine("5 - Logoff");
        }
    }
}
