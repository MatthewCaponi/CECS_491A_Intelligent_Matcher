﻿using System;
using System.Collections.Generic;
using System.Text;
using UserManagement.Models;

namespace BusinessModels.ListingModels
{
    public class BusinessListingModel
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public String Details { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public int NumberOfParticipants { get; set; }
        public String InPersonOrRemote { get; set; }
        public WebUserAccountModel UserAccountId { get; set; }
        

    }
}