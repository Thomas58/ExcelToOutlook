using System.Collections.Generic;

namespace Oauth.Wrappers.Resources
{
    public class ContactFolder
    {
        public List<ContactFolder> ChildFolders { get; set; }
        public List<Contact> Contacts           { get; set; }
        public string DisplayName               { get; set; }
        public string Id                        { get; set; }
        public string ParentFolderId            { get; set; }
    }
}
