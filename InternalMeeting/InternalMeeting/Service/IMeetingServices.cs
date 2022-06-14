using InternalMeeting.Models;

namespace InternalMeeting.Service
{
    public interface IMeetingServices
    {
        public List<Meeting> GetAllMeetings();
        public string AddNewMeeting(Meeting meeting);
        public string DeleteMeeting(Meeting meeting, Person person);
        public (string, Meeting) AddPersonToMeeting(Meeting meeting, string personName);
        public (string, Meeting) DeletePersonFromMeeting(Meeting meeting, string personName);
        public List<Meeting> GetMeetingsByDescription(string description);
        public List<Meeting> GetMeetingsByResponsiblePerson(string responsiblePerson);
        public List<Meeting> GetMeetingsByCategory(Category category);
        public List<Meeting> GetMeetingsByMeetType(MeetType type);
        public List<Meeting> GetMeetingsByDates(DateTime fromDate, DateTime toDate);
        public List<Meeting> GetMeetingByAttendeesNumber(int moreThan);
    }
}
