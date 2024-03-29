﻿using System;
using System.Collections.Generic;

namespace Oauth.Wrappers.Resources
{
    public class Contact
    {
        public string AssistantName { get; set; }
        public DateTimeOffset Birthday { get; set; }
        public PhysicalAddress BusinessAddress { get; set; }
        public string BusinessHomePage { get; set; }
        public List<string> BusinessPhones { get; set; }
        public List<string> Categories { get; set; }
        public string ChangeKey { get; set; }
        public List<string> Children { get; set; }
        public string CompanyName { get; set; }
        public string Department { get; set; }
        public DateTimeOffset CreatedDateTime { get; set; }
        public DateTimeOffset LastModifiedDateTime { get; set; }
        public string DisplayName { get; set; }
        public List<EmailAddress> EmailAddresses { get; set; }
        //public List<Extension> Extensions { get; set; }
        public string FileAs { get; set; }
        public string Generation { get; set; }
        public string GivenName { get; set; }
        public PhysicalAddress HomeAddress { get; set; }
        public List<string> HomePhones { get; set; }
        public string Id { get; set; }
        public List<string> ImAddresses { get; set; }
        public string Initials { get; set; }
        public string JobTitle { get; set; }
        public string Manager { get; set; }
        public string MiddleName { get; set; }
        public string MobilePhone1 { get; set; }
        public string NickName { get; set; }
        public string OfficeLocation { get; set; }
        public PhysicalAddress OtherAddress { get; set; }
        public string ParentFolderId { get; set; }
        public string PersonalNotes { get; set; }
        public string Profession { get; set; }
        public string SpouseName { get; set; }
        public string Surname { get; set; }
        public string Title { get; set; }
    }
}
