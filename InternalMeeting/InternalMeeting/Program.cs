using InternalMeeting.Models;
using InternalMeeting.Repository;

namespace InternalMeeting;

public static class Program
{
    
    public static MeetingRepository _meetingRepository = new();
    public static void Main()
    {
        while(true)
        {
            Console.Clear();
            Console.WriteLine("Welcom to meeting app");
            Console.WriteLine("Enter the name to login as resposible person");
            var userInput = Console.ReadLine().TrimStart().TrimEnd();
            if (userInput.Length == 0)
            {
                ConsoleMsg.EmptyInputMsg();
                continue;
            }
            var resposiblePerson = new Person(userInput);
            _meetingRepository.StartProgram(resposiblePerson);
        }

    }
}



