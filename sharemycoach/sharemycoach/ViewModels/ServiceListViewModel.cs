using sharemycoach.Models;
using System.Collections.Generic;

namespace sharemycoach.ViewModels
{
    public class ServiceListViewModel
    {
        // equipment-type
        public List<EquipmentTypeModel> EquipmentTypeList { get; set; }
        // fee
        public List<FeeModel> FeeList { get; set; }
        // location-web-insurance-company
        public List<LocationInsuranceCompanyModel> LocationInsuranceCompanyList { get; set; }
        // lead-source
        public List<LeadSourceModel> LeadSourceList { get; set; }
    }
}