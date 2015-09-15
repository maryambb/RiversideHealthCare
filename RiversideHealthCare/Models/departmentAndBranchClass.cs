using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RiversideHealthCare.Models
{
    public class departmentAndBranchClass
    {

        //Returning all departments 
        riversideLinqDataContext objLinq = new riversideLinqDataContext();
        public List<string> getDepartmentList()
        {
            List<string> deptList = new List<string>();
            foreach (var dept in objLinq.Departments.Select(x => x.name))
            {
                deptList.Add(dept);
            }
            return deptList;
        }

        //Returning all branches
        public IQueryable<Branch> getAllBranches()
        {
            var allBranches = objLinq.Branches.Select(x => x);
            return allBranches;
        }

    }
}