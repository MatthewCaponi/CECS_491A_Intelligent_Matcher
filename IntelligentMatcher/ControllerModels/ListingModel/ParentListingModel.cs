﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ControllerModels.ListingModel
{
    public class ParentListingModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int NumberOfParticipants { get; set; }
        public string InPersonOrRemote { get; set; }
        public int UserAccountId { get; set; }
    }
}
