using InternalMeeting.Models;
using InternalMeeting.Service;
using System;
using Xunit;

namespace InternalMeeting.Tests
{
    public class MeetingServicesTest
    {
        
        [Fact]
        public void Test_If_MeetingService_Fuction_AddNewMeeting_Corectly_Add_Meeting_To_File_And_are_Equal()
        {
            MeetingServices meetingServices = new("Test1.json");
            var newMeetig = new Meeting("FirstTest", new Person("Audrius"), "Testing Add new meeting",
                                       (Category)1, (MeetType)1, DateTime.Parse("2022-06-06 12:30"), DateTime.Parse("2022-06-06 14:30"));
            meetingServices.AddNewMeeting(newMeetig);
            var readFromFile = meetingServices.GetAllMeetings();

            Assert.Equal(newMeetig.Description, readFromFile[0].Description);

            meetingServices.DeleteJsonFile();
        }
        [Fact]
        public void Test_If_MeetingService_Fuction_AddNewMeeting_Dont_add_the_Same_Meeting_Twice()
        {
            MeetingServices meetingServices = new("Test2.json");
            var newMeetig = new Meeting("Test", new Person("Audrius"), "Testing Add new meeting dont add the same",
                                       (Category)1, (MeetType)1, DateTime.Parse("2022-06-06 12:30"), DateTime.Parse("2022-06-06 14:30"));
            meetingServices.AddNewMeeting(newMeetig);
            var copyNewMeetig = new Meeting("Test", new Person("Audrius"), "Testing Add new meeting dont add the same",
                           (Category)1, (MeetType)1, DateTime.Parse("2022-06-06 12:30"), DateTime.Parse("2022-06-06 14:30"));
            meetingServices.AddNewMeeting(copyNewMeetig);
            var readFromFile = meetingServices.GetAllMeetings();
            var meetingNumberInFile = readFromFile.Count;
            Assert.Equal(1, meetingNumberInFile);

            meetingServices.DeleteJsonFile();
        }
        [Fact]
        public void Test_If_MeetingService_Fuction_DeleteMeeting_Delete_Meeting_Then_Resposible_Persons_Equal()
        {
            MeetingServices meetingServices = new("Test3.json");
            var newMeetig = new Meeting("Test", new Person("Audrius"), "Testing delete function",
                                       (Category)1, (MeetType)1, DateTime.Parse("2022-06-06 12:30"), DateTime.Parse("2022-06-06 14:30"));
            meetingServices.AddNewMeeting(newMeetig);
            var deletingMeetig = new Meeting("Test", new Person("Audrius"), "Testing delete function tray to delete this",
                           (Category)1, (MeetType)1, DateTime.Parse("2022-06-06 12:30"), DateTime.Parse("2022-06-06 14:30"));
            meetingServices.AddNewMeeting(deletingMeetig);
            meetingServices.DeleteMeeting(deletingMeetig, new Person("Audrius"));

            var readFromFile = meetingServices.GetAllMeetings();
            var meetingNumberInFile = readFromFile.Count;

            Assert.Equal(1, meetingNumberInFile);

            meetingServices.DeleteJsonFile();
        }
        [Fact]
        public void Test_If_MeetingService_Fuction_DeleteMeeting_Dont_Delete_Meeting_Then_Resposible_Person_differs()
        {
            MeetingServices meetingServices = new("Test4.json");
            var newMeetig = new Meeting("Test", new Person("Audrius"), "Testing delete function",
                                       (Category)1, (MeetType)1, DateTime.Parse("2022-06-06 12:30"), DateTime.Parse("2022-06-06 14:30"));
            meetingServices.AddNewMeeting(newMeetig);
            var deletingMeetig = new Meeting("Test", new Person("Audrius"), "Testing delete function tray to delete this",
                           (Category)1, (MeetType)1, DateTime.Parse("2022-06-06 12:30"), DateTime.Parse("2022-06-06 14:30"));
            meetingServices.AddNewMeeting(deletingMeetig);
            meetingServices.DeleteMeeting(deletingMeetig, new Person("Test"));

            var readFromFile = meetingServices.GetAllMeetings();
            var meetingNumberInFile = readFromFile.Count;

            Assert.Equal(2, meetingNumberInFile);

            meetingServices.DeleteJsonFile();
        }
        [Fact]
        public void Test_If_MeetingService_Fuction_AddPersonToMeeting_Adds_New_Person_To_The_Meeting()
        {
            MeetingServices meetingServices = new("Test5.json");
            var newMeetig = new Meeting("Test", new Person("Audrius"), "Adding person to the meeting",
                                       (Category)1, (MeetType)1, DateTime.Parse("2022-06-06 12:30"), DateTime.Parse("2022-06-06 14:30"));
            meetingServices.AddNewMeeting(newMeetig);

            meetingServices.AddPersonToMeeting(newMeetig, "Bukis");
            var readFromFile = meetingServices.GetAllMeetings();
            var newPersonExist = readFromFile[0].PersonList.Exists(p => p.Name == "Bukis");

            Assert.True(newPersonExist);

            meetingServices.DeleteJsonFile();
        }
        [Fact]
        public void Test_If_MeetingService_Fuction_AddPersonToMeeting_When_Adding_Existing_Person_It_Wont_Add()
        {
            MeetingServices meetingServices = new("Test6.json");
            var newMeetig = new Meeting("Test", new Person("Audrius"), "Adding person to the meeting which already exists",
                                       (Category)1, (MeetType)1, DateTime.Parse("2022-06-06 12:30"), DateTime.Parse("2022-06-06 14:30"));
            meetingServices.AddNewMeeting(newMeetig);

            meetingServices.AddPersonToMeeting(newMeetig, "Bukis");
            meetingServices.AddPersonToMeeting(newMeetig, "bukis");
            var readFromFile = meetingServices.GetAllMeetings();
            var personNumberInMeeting = readFromFile[0].PersonList.Count;

            Assert.Equal(2, personNumberInMeeting);

            meetingServices.DeleteJsonFile();
        }
        [Fact]
        public void Test_If_MeetingService_Fuction_AddPersonToMeeting_When_Adding_Person_Which_Have_An_Overlaping_Meeting_Gives_Warning_Masage()
        {
            MeetingServices meetingServices = new("Test7.json");
            var newMeetig = new Meeting("Test", new Person("Audrius"), "Adding person to the meeting have Overlaping meeting",
                                       (Category)1, (MeetType)1, DateTime.Parse("2022-06-06 12:30"), DateTime.Parse("2022-06-06 14:30"));
            meetingServices.AddNewMeeting(newMeetig);
            meetingServices.AddPersonToMeeting(newMeetig, "Bukis");
            var overlapingMeeting = new Meeting("Test2", new Person("Pranas"), "Adding person to the meeting have Overlaping meeting",
                                       (Category)1, (MeetType)1, DateTime.Parse("2022-06-06 13:30"), DateTime.Parse("2022-06-06 15:30"));
            meetingServices.AddNewMeeting(overlapingMeeting);
            Meeting updatedMeting;
            string warningMesage;
            (warningMesage, updatedMeting) = meetingServices.AddPersonToMeeting(overlapingMeeting, "Bukis");
            
            Assert.Equal("WARRNING: Person Bukis have an overlaping meeting", warningMesage);

            meetingServices.DeleteJsonFile();
        }
        [Fact]
        public void Test_If_MeetingService_Fuction_DeletePersonFromMeeting_When_deletind_Person_Which_Dont_Exists_Gives_Warning_Masage()
        {
            MeetingServices meetingServices = new("Test8.json");
            var newMeetig = new Meeting("Test", new Person("Audrius"), "Deleting person from the meeting which don't exists",
                                       (Category)1, (MeetType)1, DateTime.Parse("2022-06-06 12:30"), DateTime.Parse("2022-06-06 14:30"));
            meetingServices.AddNewMeeting(newMeetig);
            meetingServices.AddPersonToMeeting(newMeetig, "Bukis");
           
            Meeting updatedMeting;
            string warningMesage;
            (warningMesage, updatedMeting) = meetingServices.DeletePersonFromMeeting(newMeetig, "Pranas");

            Assert.Equal("Wrong Persons name Pranas don't exist in the meeting", warningMesage);

            meetingServices.DeleteJsonFile();
        }
        [Fact]
        public void Test_If_MeetingService_Fuction_DeletePersonFromMeeting_When_deletind_Responsible_Person_Gives_Warning_Masage()
        {
            MeetingServices meetingServices = new("Test9.json");
            var newMeetig = new Meeting("Test", new Person("Audrius"), "Deleting person from the meeting",
                                       (Category)1, (MeetType)1, DateTime.Parse("2022-06-06 12:30"), DateTime.Parse("2022-06-06 14:30"));
            meetingServices.AddNewMeeting(newMeetig);
            meetingServices.AddPersonToMeeting(newMeetig, "Bukis");
            meetingServices.AddPersonToMeeting(newMeetig, "Pranas");
            Meeting updatedMeting;
            string warningMesage;
            (warningMesage, updatedMeting) = meetingServices.DeletePersonFromMeeting(newMeetig, "Audrius");

            Assert.Equal("Resposible person Audrius can't be remuved from list", warningMesage);

            meetingServices.DeleteJsonFile();
        }
        [Fact]
        public void Test_If_MeetingService_Fuction_DeletePersonFromMeeting_Deletind_person()
        {
            MeetingServices meetingServices = new("Test10.json");
            var newMeetig = new Meeting("Test", new Person("Audrius"), "Deleting person from the meeting",
                                       (Category)1, (MeetType)1, DateTime.Parse("2022-06-06 12:30"), DateTime.Parse("2022-06-06 14:30"));
            meetingServices.AddNewMeeting(newMeetig);
            meetingServices.AddPersonToMeeting(newMeetig, "Bukis");
            meetingServices.AddPersonToMeeting(newMeetig, "Pranas");
            meetingServices.DeletePersonFromMeeting(newMeetig, "Bukis");
            var readFromFile = meetingServices.GetAllMeetings();
            var personNumberInMeeting = readFromFile[0].PersonList.Count;

            Assert.Equal(2, personNumberInMeeting);

            meetingServices.DeleteJsonFile();
        }
    }
}