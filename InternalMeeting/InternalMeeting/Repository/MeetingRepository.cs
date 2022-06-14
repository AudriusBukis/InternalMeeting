using InternalMeeting.Models;
using InternalMeeting.Service;

namespace InternalMeeting.Repository
{
    public class MeetingRepository 
    {
        private readonly MeetingServices _meetingServices = new("Meetings.json");
        public void StartProgram(Person person)
        {
            var logoff = false;
            while(!logoff)
            {
                ConsoleMsg.FirstSelection(person.Name);
                if (Int32.TryParse(Console.ReadLine(), out int command))
                {
                    switch (command)
                    {
                        case 1:
                            CreateNewMeeting(person);
                            break;
                        case 2:
                            DeleteSelectedMeeting(person);
                            break;
                        case 3:
                            AddOrDeletePerson(person);
                            break;
                        case 4:
                            ListAllMetings();
                            break;
                        case 5:
                            logoff = true;
                            break;
                        default:
                            ConsoleMsg.WrongInputMsg();
                            break;
                    }
                }else ConsoleMsg.WrongInputMsg();
            }
        }
        public void CreateNewMeeting(Person person)
        {
            Console.Clear();
            Console.WriteLine("Enter meeting's name:");
            var name = InputForString();
            Console.Clear();
            Console.WriteLine("Enter meeting's description");
            var description = InputForString(); ;
            Console.Clear();
            var type = InputForMeetType();
            Console.Clear();
            var category = InputForCategoty();
            Console.Clear();
            Console.WriteLine("Enter start date of the meeting in format yyyy/MM/dd HH:mm");
            var  startDate = InputForDateTime();
            Console.Clear();
            Console.WriteLine("Enter end date of the meeting in format yyyy/MM/dd HH:mm");
            var endDate = InputForDateTime();
            Console.WriteLine(_meetingServices.AddNewMeeting(new Meeting(name, person, description, category, type, startDate, endDate)));
            ConsoleMsg.ContinueMsg();
        }
        public void DeleteSelectedMeeting(Person person)
        {
            Console.Clear();
            var meeting = GetMeetingFromList();
           
            if (meeting != null)
            {
                Console.WriteLine(_meetingServices.DeleteMeeting(meeting, person));
                ConsoleMsg.ContinueMsg();
            }
        }
        public Meeting GetMeetingFromList()
        {
            var listMeetings = _meetingServices.GetAllMeetings();
            foreach (var meeting in listMeetings)
            {
                Console.WriteLine($"{listMeetings.IndexOf(meeting) + 1} -    {meeting}");
            }
            ConsoleMsg.BackToPreviouslyWindowMsg(listMeetings.Count + 1);
            while (true)
            {
                Console.WriteLine("Enter the number of the meeting in which you want to add or delete person");
                if (Int32.TryParse(Console.ReadLine(), out int command))
                {
                    if (command == listMeetings.Count + 1) break;
                    if ((command > 0) && (command <= listMeetings.Count))
                    {
                        return listMeetings.ElementAt(command - 1);
                    }
                    else ConsoleMsg.WrongInputMsg();
                }
                else ConsoleMsg.WrongInputMsg();
            }
            return default;
        }
        public void AddOrDeletePerson(Person person)
        {
            var meeting = GetMeetingFromList();
            if (meeting != null)
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine(meeting);
                    Console.WriteLine("Select number what you want to do \n1 - Add person to the meeting, \n2 - Delete person from the meeting, \n3 - Back to previously menu");
                    if (Int32.TryParse(Console.ReadLine(), out int selection))
                    {
                        string answer;
                        if (selection == 1)
                        {
                            Console.WriteLine("Enter the persons name you want to add");
                            var name = InputForString();
                            (answer, meeting) = _meetingServices.AddPersonToMeeting(meeting, name);
                            Console.WriteLine(answer);
                            ConsoleMsg.ContinueMsg();
                        } else if (selection == 2)
                        {
                            Console.WriteLine("Enter the persons name you want to delete");
                            var name = InputForString();
                            (answer, meeting) = _meetingServices.DeletePersonFromMeeting(meeting, name);
                            Console.WriteLine(answer);
                            ConsoleMsg.ContinueMsg();
                        }
                        else if (selection == 3) break;
                        else ConsoleMsg.WrongInputMsg();
                    }
                    else ConsoleMsg.WrongInputMsg();
                }

            }
        }
        public void ListAllMetings()
        {
            while (true)
            {
               Console.Clear();
               ConsoleMsg.FilterSelectionMsg();
               if (Int32.TryParse(Console.ReadLine(), out int selection))
               {
                    if (selection == 1)
                    {
                        Console.WriteLine("Filter by description");
                        Console.WriteLine("Enter meeting's description");
                        var description = InputForString();
                        var filteredMeetings = _meetingServices.GetMeetingsByDescription(description);
                        foreach (var meeting in filteredMeetings) Console.WriteLine(meeting);
                        ConsoleMsg.ContinueMsg();

                    }
                    else if (selection == 2)
                    {
                        Console.WriteLine("Filter by responsible person");
                        Console.WriteLine("Enter name:");
                        var name = InputForString();
                        var filteredMeetings = _meetingServices.GetMeetingsByResponsiblePerson(name);
                        foreach (var meeting in filteredMeetings) Console.WriteLine(meeting);
                        ConsoleMsg.ContinueMsg();
                    }
                    else if (selection == 3)
                    {
                        Console.WriteLine("Filter by category");
                        var category = InputForCategoty();
                        var filteredMeetings = _meetingServices.GetMeetingsByCategory(category);
                        foreach (var meeting in filteredMeetings) Console.WriteLine(meeting);
                        ConsoleMsg.ContinueMsg();
                    }
                    else if (selection == 4)
                    {
                        var type = InputForMeetType();
                        var filteredMeetings = _meetingServices.GetMeetingsByMeetType(type);
                        foreach (var meeting in filteredMeetings) Console.WriteLine(meeting);
                        ConsoleMsg.ContinueMsg();
                    }
                    else if (selection == 5)
                    {
                        Console.WriteLine("Filter by dates");
                        Console.WriteLine("Enter start date of the meeting in format yyyy/MM/dd HH:mm");
                        var startDate = InputForDateTime();
                        Console.WriteLine("Enter end date of the meeting in format yyyy/MM/dd HH:mm");
                        var endDate = InputForDateTime();
                        var filteredMeetings = _meetingServices.GetMeetingsByDates(startDate, endDate);
                        foreach (var meeting in filteredMeetings) Console.WriteLine(meeting);
                        ConsoleMsg.ContinueMsg();
                    }
                    else if (selection == 6)
                    {
                        Console.WriteLine("Filter by the number of attendees");
                        Console.WriteLine("enter the number");
                        if (Int32.TryParse(Console.ReadLine(), out int number))
                        {
                            var filteredMeetings = _meetingServices.GetMeetingByAttendeesNumber(number);
                            foreach (var meeting in filteredMeetings) Console.WriteLine(meeting);
                            ConsoleMsg.ContinueMsg();
                        }
                        else ConsoleMsg.WrongInputMsg();
                    }
                    else if (selection == 7) break;
                    else ConsoleMsg.WrongInputMsg();
                }
                else ConsoleMsg.WrongInputMsg();
            }
        }
        internal Category InputForCategoty()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Select number of the meeting's category\n1 - CodeMonkey, \n2 - Hub, \n3 - Short, \n4 - TeamBuilding");
                if (Int32.TryParse(Console.ReadLine(), out int selection))
                {
                    if ((selection > 0) && (selection < 5))
                    {
                        return (Category)selection;
                    }
                    else ConsoleMsg.WrongInputMsg();
                }
                else ConsoleMsg.WrongInputMsg();

            }
        }
        internal string InputForString()
        {
            string stringInput;
            while (true)
            {
                stringInput = Console.ReadLine().TrimStart().TrimEnd();
                if (stringInput.Length == 0)
                {
                    ConsoleMsg.EmptyInputMsg();
                    continue;
                }
                break;
            }
            return stringInput;
        }
        internal MeetType InputForMeetType()
        {
            while (true)
            {
                Console.WriteLine("Select number of the meeting's type \n1 - Live, \n2 - InPerson");
                if (Int32.TryParse(Console.ReadLine(), out int selection))
                {
                    if ((selection > 0) && (selection < 3))
                    {
                        return (MeetType)selection;
                    }
                    else ConsoleMsg.WrongInputMsg();
                }
                else ConsoleMsg.WrongInputMsg();
            }
        }
        internal DateTime InputForDateTime()
        {
            while (true)
            {
                string userInput = Console.ReadLine();
                if (DateTime.TryParse(userInput, out DateTime startDateInput))
                {
                   return startDateInput;
                }
                else ConsoleMsg.WrongInputMsg();
            }
        }
    }
}