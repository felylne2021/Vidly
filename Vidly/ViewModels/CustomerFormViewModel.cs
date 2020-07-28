using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class CustomerFormViewModel
    {
        public IEnumerable<MembershipType> MembershipTypes { get; set; }
        public Customer Customer { get; set; }
    }

    public class TestVM
    {
        public string FieldA { get; set; }
        public int FieldB { get; set; }
        public NestedCl NestedObj { get; set; }
    }

    public class NestedCl
    {
        public string NestedA { get; set; }
        public string NestedB { get; set; }
    }
}