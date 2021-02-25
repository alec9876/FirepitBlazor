using AutoMapper;
using FirepitAPI.DTO;
using FirepitAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirepitAPI.Mapping
{
    public class Maps : Profile
    {
        public Maps()
        {
            //Person 
            CreateMap<Person, PersonDTO>().ReverseMap();
            CreateMap<Person, PersonUpdateDTO>().ReverseMap();

            //Beverages
            CreateMap<Beverages, BeverageDTO>().ReverseMap();
            CreateMap<Beverages, BeverageCreateDTO>().ReverseMap();
            CreateMap<Beverages, BeverageUpdateDTO>().ReverseMap();

            //Meetings
            CreateMap<Meeting, MeetingDTO>().ReverseMap();
            CreateMap<Meeting, MeetingCreateDTO>().ReverseMap();
            CreateMap<Meeting, MeetingUpdateDTO>().ReverseMap();

            //Quotes
            CreateMap<Quotes, QuoteDTO>().ReverseMap();
            CreateMap<Quotes, QuoteCreateDTO>().ReverseMap();
            CreateMap<Quotes, QuoteUpdateDTO>().ReverseMap();

            CreateMap<UserMeeting, UserMeetingDTO>().ReverseMap();
            CreateMap<UserMeeting, UserMeetingUpdateDTO>().ReverseMap();
        }
    }
}
