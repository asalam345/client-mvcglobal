using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvcglobal.Models
{
    public class ModelUser
    {

        public string GroupId { get; set; }
        public string GroupName { get; set; }
        public string GroupShortName { get; set; }
        public string GroupCode { get; set; }

        public string BranchId { get; set; }
        public string BranchTitle { get; set; }

        public string DesignationId { get; set; }
        public string DesignationName { get; set; }

        public string UserId { get; set; }
        public string UserFullName { get; set; }
        public string UserLoginName { get; set; }
        public string CreationBy { get; set; }
        public string ContactId { get; set; }
        public string Phone { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }



        public List<ModelUser> GroupInfo { get; set; }
        public List<ModelUser> BranchInfo { get; set; }
        public List<ModelUser> DropDownInfo { get; set; }

    }

    public class ListComboDataViewModel
    {
    public string GroupId { get; set; }
    public string GroupName { get; set; }
    public List<ModelUser> GroupInfo { get; set; }
    //    public List<ModelUser> BranchInfo { get; set; }

    }
}