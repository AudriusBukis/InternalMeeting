using InternalMeeting.Models;
using System.Text.Json;

namespace InternalMeeting.Service
{
    public class FileService : IFileService
    {
        private string _path;
        public FileService(string fileName)
        {
            _path = Path.GetFullPath(@"..\..\..\" + fileName);
        }
        public List<Meeting> ReadMeetings()
        {
            if (!File.Exists(_path))
                return new List<Meeting>();
            var readedMeetings = File.ReadAllText(_path); 
            List<Meeting>? listMeetings = JsonSerializer.Deserialize<List<Meeting>>(readedMeetings);
            return listMeetings == null ? new List<Meeting>() : listMeetings;
        }

        public void WriteMeetings(List<Meeting> meetingsList)
        {
            string json = JsonSerializer.Serialize(meetingsList);
            File.WriteAllText(_path, json);
        }
        public void DeleteFile()
        {
            File.Delete(_path);
        }
    }
}
