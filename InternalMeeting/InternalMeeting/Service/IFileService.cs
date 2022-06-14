using InternalMeeting.Models;

namespace InternalMeeting.Service
{
    public interface IFileService
    {
        public List<Meeting> ReadMeetings();
        public void WriteMeetings(List<Meeting> meetingsList);
        public void DeleteFile();
    }
}
