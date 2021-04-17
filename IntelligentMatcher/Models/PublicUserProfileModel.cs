using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class PublicUserProfileModel
    {
		public int Id { set; get; }
		public int UserId { set; get; }
		public string Jobs { set; get; }
		public string Goals { set; get; }
		public int Age { set; get; }
		public string Gender  { set; get; }
		public string Ethnicity  { set; get; }
		public string SexualOrientation  { set; get; }
		public string Height { set; get; }
		public string Visibility  { set; get; }
		public string Status  { set; get; }
		public string Photo { set; get; }
		public string Intrests { set; get; }

		public string Description { set; get; }

		public string Hobbies { set; get; }


	}
}
