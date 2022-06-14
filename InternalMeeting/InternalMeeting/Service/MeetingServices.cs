using InternalMeeting.Models;

namespace InternalMeeting.Service
{
    
    public class MeetingServices : IMeetingServices
    {
        public FileService _fileService;

        public MeetingServices(string fileName)
        {
            _fileService = new FileService(fileName);
        }
        public List<Meeting> GetAllMeetings()
        {
            return _fileService.ReadMeetings();
        }
        public string AddNewMeeting(Meeting meeting)
        {
            var listMeetings = _fileService.ReadMeetings();
            if(!listMeetings.Exists(m => m.Name == meeting.Name &&
                                         m.Description == meeting.Description &&
                                         m.ResponsiblePerson.Name == meeting.ResponsiblePerson.Name &&
                                         m.Category == meeting.Category &&
                                         m.Type == meeting.Type &&
                                         m.StartDate.Equals(meeting.StartDate) &&
                                         m.EndDate.Equals(meeting.EndDate)))
            {
                listMeetings.Add(meeting);
                _fileService.WriteMeetings(listMeetings);
                return "Meeting was added";
            }
            return "Meeting exist in current meeting list";
        }
        public string DeleteMeeting(Meeting meeting, Person person)
        {
            var listMeetings = _fileService.ReadMeetings();
            var meetingIndex = listMeetings.FindIndex(m => m.Name == meeting.Name &&
                                                           m.Description == meeting.Description &&
                                                           m.ResponsiblePerson.Name == meeting.ResponsiblePerson.Name &&
                                                           m.Category == meeting.Category &&
                                                           m.Type == meeting.Type &&
                                                           m.StartDate.Equals(meeting.StartDate) &&
                                                           m.EndDate.Equals(meeting.EndDate));
            if (meetingIndex == -1) return "Meeting was not found";
            if (listMeetings.Exists(m => m.ResponsiblePerson.Name.ToLower() == person.Name.ToLower()))
            {
                listMeetings.RemoveAt(meetingIndex);
                _fileService.WriteMeetings(listMeetings);
                return "Meeting deleted";
            }
            return $"This user {person} is not resposible for this meeeting ";
        }
        public (string,Meeting) AddPersonToMeeting(Meeting meeting, string personName)
        {
            var listMeetings = _fileService.ReadMeetings();
            var foundedMeeting = listMeetings.SingleOrDefault(m => m.Name == meeting.Name &&
                                                         m.Description == meeting.Description &&
                                                         m.ResponsiblePerson.Name == meeting.ResponsiblePerson.Name &&
                                                         m.Category == meeting.Category &&
                                                         m.Type == meeting.Type &&
                                                         m.StartDate.Equals(meeting.StartDate) &&
                                                         m.EndDate.Equals(meeting.EndDate));
            if (!foundedMeeting.PersonList.Exists(p => p.Name.ToLower() == personName.ToLower()))
            {
                var filteredList = listMeetings.Where(m => m.StartDate < meeting.EndDate && m.EndDate > meeting.StartDate).ToList();
                if (filteredList.Exists(p => p.PersonList.Any(n => n.Name.ToLower() == personName.ToLower())))
                {
                    foundedMeeting.PersonList.Add(new Person(personName));
                    _fileService.WriteMeetings(listMeetings);
                    return ($"WARRNING: Person {personName} have an overlaping meeting" , foundedMeeting);
                }
                foundedMeeting.PersonList.Add(new Person(personName));
                _fileService.WriteMeetings(listMeetings);
                return ($"Person {personName} added to the meeting", foundedMeeting);
            }
            return ($"Person {personName} exist in this meeting", foundedMeeting);
        }
        public (string, Meeting) DeletePersonFromMeeting(Meeting meeting, string personName)
        {
            var listMeetings = _fileService.ReadMeetings();
            var foundedMeeting = listMeetings.SingleOrDefault(m => m.Name == meeting.Name &&
                                                         m.Description == meeting.Description &&
                                                         m.ResponsiblePerson.Name == meeting.ResponsiblePerson.Name &&
                                                         m.Category == meeting.Category &&
                                                         m.Type == meeting.Type &&
                                                         m.StartDate.Equals(meeting.StartDate) &&
                                                         m.EndDate.Equals(meeting.EndDate));

            if (foundedMeeting.PersonList.Exists(p => p.Name.ToLower() == personName.ToLower()))
            {
                if (foundedMeeting.ResponsiblePerson.Name.ToLower() == personName.ToLower())
                {
                    return ($"Resposible person {foundedMeeting.ResponsiblePerson.Name} can't be remuved from list", foundedMeeting);
                }
                var person = new Person(personName);
                foundedMeeting.PersonList.Remove(foundedMeeting.PersonList.Single(p => p.Name.ToLower() == personName.ToLower()));
                _fileService.WriteMeetings(listMeetings);
                return ($"Person {personName} remuved from the meeting", foundedMeeting);
            }
            return ($"Wrong Persons name {personName} don't exist in the meeting", foundedMeeting);
        }
        public List<Meeting> GetMeetingsByMeetType(MeetType type)
        {
            return _fileService.ReadMeetings().Where(t => t.Type == type).ToList();
        }
        public List<Meeting> GetMeetingByAttendeesNumber(int moreThan)
        {
            return _fileService.ReadMeetings().Where(t => t.PersonList.Count > moreThan).ToList();
        }
        public List<Meeting> GetMeetingsByCategory(Category category)
        {
            return _fileService.ReadMeetings().Where(t => t.Category == category).ToList();
        }
        public List<Meeting> GetMeetingsByDates(DateTime fromdDate, DateTime toDate)
        {
            return _fileService.ReadMeetings().Where(m => m.StartDate >= fromdDate).
                                               Where(m => m.EndDate <= toDate).
                                               ToList();
        }
        public List<Meeting> GetMeetingsByDescription(string description)
        {
            description = description.ToLower();
            return _fileService.ReadMeetings().Where(m => m.Description.ToLower().Contains(description)).ToList();
        }
        public List<Meeting> GetMeetingsByResponsiblePerson(string responsiblePerson)
        {
            return _fileService.ReadMeetings().Where(t => t.ResponsiblePerson.Name == responsiblePerson).ToList();
        }
        public void DeleteJsonFile()
        {
            _fileService.DeleteFile();
        }
    }
}
